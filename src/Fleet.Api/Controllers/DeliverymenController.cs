using Fleet.Api.ViewModels;
using Fleet.Application.Services;
using Fleet.Application.UseCases.UserUseCases.Register;
using Fleet.Application.UseCases.UserUseCases.Remove;
using Fleet.Application.UseCases.UserUseCases.UpdateCnhImage;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Fleet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliverymenController : ControllerBase
{
    private readonly ILogger<DeliverymenController> _logger;
    private readonly IPasswordHasher<AppUser> _hasher;
    private readonly IUserRepository _userRepo;
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public DeliverymenController(
        ILogger<DeliverymenController> logger, // TODO: use logger in methods
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

    [HttpGet]
    public async Task<IActionResult> GetAllDeliverymen()
    {
        Expression<Func<AppUser, bool>> predicate = u => u.Role == UserRole.Deliveryman;
        Expression<Func<AppUser, DeliverymanVM>> selector = u => new DeliverymanVM
        {
            Identifier = u.Username,
            Name = u.Name,
            Cnpj = u.Deliveryman!.Cnpj,
            BirthDate = u.BirthDate,
            Role = u.Role!.Value.ToString(),
        };

        var users = await _userRepo.GetAllAsync(predicate, selector);

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeliverymanByUsername(string id)
    {
        var user = await _userRepo.GetUserByUsernameAsync(id);

        if (user == null)
            return NotFound("Deliveryman was not found");

        var output = new DeliverymanVM
        {
            Identifier = user.Username,
            Name = user.Name,
            Cnpj = user.Deliveryman != null ? user.Deliveryman!.Cnpj : null,
            CnhNumber = user.Deliveryman != null ? user.Deliveryman!.CnhNumber : null,
            BirthDate = user.BirthDate,
            Role = user.Role!.Value.ToString(),
        };

        return Ok(output);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("{id}/cnh")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateCnhImage(string id, [FromBody] UpdateCnhImageInput input)
    {
        input.DeliverymanUsername = id;

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userRepo.GetByUsernameAsync(dto.Username);

        if (user == null)
            return Unauthorized("Deliveryman was not found");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Username or password is incorrect");

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
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; init; } = null!;
}
