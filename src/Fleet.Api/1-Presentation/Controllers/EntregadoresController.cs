using Fleet.Api._1_Presentation.ViewModels;
using Fleet.Api._2_Application.Services;
using Fleet.Api._2_Application.UseCases.UserUseCases.Register;
using Fleet.Api._2_Application.UseCases.UserUseCases.Remove;
using Fleet.Api._2_Application.UseCases.UserUseCases.UpdateCnhImage;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Fleet.Api._1_Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class EntregadoresController : ControllerBase
{
    private readonly ILogger<EntregadoresController> _logger;
    private readonly IPasswordHasher<AppUser> _hasher;
    private readonly IUserRepository _userRepo;
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public EntregadoresController(
        ILogger<EntregadoresController> logger, // TODO: use logger in methods
        IPasswordHasher<AppUser> hasher,
        IUserRepository userRepo,
        IMediator mediator,
        IAuthService authService)
    {
        _logger = logger;
        _hasher = hasher;
        _userRepo = userRepo;
        _mediator = mediator;
        _authService = authService;
    }

    // TODO: implement authorization for these endpoints

    [HttpGet]
    public async Task<IActionResult> GetAllDeliverymen()
    {
        Expression<Func<AppUser, bool>> predicate = u => u.Role == UserRole.Deliveryman;
        Expression<Func<AppUser, DeliverymanVM>> selector = u => new DeliverymanVM
        {
            Identificador = u.Username,
            Nome = u.Name,
            Cnpj = u.Deliveryman!.Cnpj,
            DataNascimento = u.BirthDate,
            Papel = u.Role!.Value.ToString(),
        };

        var users = await _userRepo.GetAllAsync(predicate, selector);

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeliverymanByUsername(string id)
    {
        var user = await _userRepo.GetUserByUsernameAsync(id);

        if (user == null)
            return NotFound("Entregador não encontrado");

        var output = new DeliverymanVM
        {
            Identificador = user.Username,
            Nome = user.Name,
            Cnpj =      user.Deliveryman != null ? user.Deliveryman!.Cnpj : null,
            CnhNumber = user.Deliveryman != null ? user.Deliveryman!.CnhNumber : null,
            DataNascimento = user.BirthDate,
            Papel = user.Role!.Value.ToString(),
        };

        return Ok(output);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Dados inválidos");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("{id}/cnh")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateCnhImage(string id, [FromBody] UpdateCnhImageInput input)
    {
        input.DeliverymanUsername = id;

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Dados inválidos");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userRepo.GetByUsernameAsync(dto.Username);

        if (user == null)
            return Unauthorized("Entregador não encontrado");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Usuário ou Senha incorreto");

        var token = _authService.CreateToken(user);
        var output = new LoginResponseDto { Token = token };

        return Ok(output);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        RemoveUserInput input = new(id);

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}

public class LoginDto
{
    [JsonPropertyName("id")]
    public string Username { get; set; } = null!;

    [JsonPropertyName("senha")]
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; init; } = null!;
}
