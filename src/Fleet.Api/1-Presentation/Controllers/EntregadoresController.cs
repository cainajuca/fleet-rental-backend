using Fleet.Api._1_Presentation.ViewModels;
using Fleet.Api._2_Application.Services;
using Fleet.Api._2_Application.UseCases;
using Fleet.Api._2_Application.UseCases.UserUseCases.Register;
using Fleet.Api._2_Application.UseCases.UserUseCases.Remove;
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

        var output = BaseOutput<IEnumerable<DeliverymanVM>>.Success(users);

        return Ok(output);
    }

    [HttpGet("{identificador}")]
    public async Task<IActionResult> GetDeliverymanByUsername(string identificador)
    {
        var user = await _userRepo.GetUserByUsernameAsync(identificador);

        if (user == null)
            return NotFound(BaseOutput<string>.Failure("User not found."));

        var output = BaseOutput<DeliverymanVM>.Success(new DeliverymanVM
        {
            Identificador = user.Username,
            Nome = user.Name,
            Cnpj = user.Deliveryman != null ? user.Deliveryman!.Cnpj : null,
            DataNascimento = user.BirthDate,
            Papel = user.Role!.Value.ToString(),
        });

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

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userRepo.GetByUsernameAsync(dto.Username);

        if (user == null)
            return Unauthorized(BaseOutput<string>.Failure("User not found"));

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return Unauthorized(BaseOutput<string>.Failure("Incorrect password"));

        var token = _authService.CreateToken(user);
        var output = BaseOutput<LoginResponseDto>.Success(new LoginResponseDto { Token = token });

        return Ok(output);
    }

    [HttpDelete("{identificador}")]
    public async Task<IActionResult> Delete(string identificador)
    {
        RemoveUserInput input = new(identificador);

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}

public class LoginDto
{
    [JsonPropertyName("identificador")]
    public string Username { get; set; } = null!;

    [JsonPropertyName("senha")]
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; init; } = null!;
}
