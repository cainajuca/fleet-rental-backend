using Fleet.Api._2_Application.Services;
using Fleet.Api._2_Application.UseCases;
using Fleet.Api._2_Application.UseCases.UserUseCases.Register;
using Fleet.Api._3_Domain.Repositories;
using Fleet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Api._1_Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IPasswordHasher<AppUser> _hasher;
    private readonly IUserRepository _userRepo;
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public UserController(
        ILogger<UserController> logger, // TODO: use logger in methods
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

    [HttpGet("{id}", Name = nameof(GetUserById))]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);

        if (user == null)
            return NotFound(BaseOutput<string>.Failure("User not found."));

        var output = BaseOutput<object>.Success(new
        {
            user.Id,
            user.Username,
            user.Email,
            user.Role,
        });

        return Ok(output);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register(RegisterUserInput input)
    {

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
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

/*
Valid fields for testing:
  "cnpj": "62173620000180",
  "birthDate": "1997-03-04T00:00:00",
  "cnhNumber": "06601432083"
*/
