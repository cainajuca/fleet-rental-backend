using Fleet.Application.Services;
using Fleet.Application.UseCases.UserUseCases.Register;
using Fleet.Domain.Interfaces.Repositories;
using Fleet.Api.Controllers;
using Fleet.Api.UnitTests.Fakers.Dtos;
using Fleet.Api.UnitTests.Fakers.Entities;
using Fleet.Api.ViewModels;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fleet.Api.UnitTests.Controllers;
public class UserControllerTests
{
    private readonly Mock<ILogger<EntregadoresController>> _logger = new();
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IMediator> _mediator = new();
    private readonly Mock<IAuthService> _authSvc = new();
    private readonly Mock<IPasswordHasher<AppUser>> _hasher = new();
    private readonly EntregadoresController _controller;

    public UserControllerTests()
    {
        _controller = new EntregadoresController(
            _logger.Object,
            _hasher.Object,
            _userRepo.Object,
            _mediator.Object,
            _authSvc.Object
        );
    }

    [Fact]
    public async Task GetDeliverymanByUsername_WhenUserExists_Returns200Ok()
    {
        // Arrange
        var user = new AppUserFaker()
            .RuleFor(u => u.Role, _ => UserRole.Admin)
            .UseSeed(234).Generate();

        _userRepo
            .Setup(r => r.GetUserByUsernameAsync(user.Username))
            .ReturnsAsync(user);

        // Act
        var action = await _controller.GetDeliverymanByUsername(user.Username);

        // Assert
        var ok = action as OkObjectResult;
        Assert.NotNull(ok);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        var output = ok.Value as DeliverymanVM;
        Assert.NotNull(output);
    }

    [Fact]
    public async Task GetDeliverymanByUsername_WhenUserNotFound_Returns404()
    {
        // Arrange
        _userRepo
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((AppUser?)null);

        // Act
        var action = await _controller.GetDeliverymanByUsername("test123");

        // Assert
        var nf = action as NotFoundObjectResult;
        Assert.NotNull(nf);
        Assert.Equal(StatusCodes.Status404NotFound, nf.StatusCode);
    }

    [Fact]
    public async Task Register_WhenUseCaseSucceeds_Returns201Created()
    {
        // Arrange
        var input = new RegisterUserInputFaker().UseSeed(234).Generate();

        input.CnhImage = "base64-text";

        var output = new RegisterUserOutput(true);

        _mediator
            .Setup(m => m.Send(input, It.IsAny<CancellationToken>()))
            .ReturnsAsync(output);

        // Act
        var action = await _controller.Register(input);

        // Assert
        var created = action as StatusCodeResult;
        Assert.NotNull(created);
        Assert.Equal(StatusCodes.Status201Created, created.StatusCode);
    }

    [Fact]
    public async Task Register_WhenUseCaseFails_Returns400BadRequest()
    {
        // Arrange
        var input = new RegisterUserInputFaker().UseSeed(234).Generate();
        var output = new RegisterUserOutput(false);

        _mediator
            .Setup(m => m.Send(It.IsAny<RegisterUserInput>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(output);

        // Act
        var action = await _controller.Register(input);

        // Assert
        var bad = action as BadRequestObjectResult;
        Assert.NotNull(bad);
        Assert.Equal(StatusCodes.Status400BadRequest, bad.StatusCode);
    }

    [Fact]
    public async Task Login_WhenUserNotFound_Returns401()
    {
        // Arrange
        var dto = new LoginDto { Username = "x", Password = "y" };
        _userRepo
            .Setup(r => r.GetByUsernameAsync(dto.Username))
            .ReturnsAsync((AppUser?)null);

        // Act
        var action = await _controller.Login(dto);

        // Assert
        var unAuth = action as UnauthorizedObjectResult;
        Assert.NotNull(unAuth);
        Assert.Equal(StatusCodes.Status401Unauthorized, unAuth.StatusCode);
    }

    [Fact]
    public async Task Login_WhenPasswordIncorrect_Returns401()
    {
        // Arrange
        var dto = new LoginDto { Username = "x", Password = "wrong" };
        var user = new AppUserFaker()
            .RuleFor(u => u.Username, dto.Username)
            .RuleFor(u => u.PasswordHash, "hashed-password")
            .UseSeed(234).Generate();

        _userRepo
            .Setup(r => r.GetByUsernameAsync(dto.Username))
            .ReturnsAsync(user);

        // Act
        var action = await _controller.Login(dto);

        // Assert
        var unAuth = action as UnauthorizedObjectResult;
        Assert.NotNull(unAuth);
        Assert.Equal(StatusCodes.Status401Unauthorized, unAuth.StatusCode);
    }

    [Fact]
    public async Task Login_WhenCredentialsValid_Returns200WithToken()
    {
        // Arrange
        var dto = new LoginDto { Username = "u", Password = "not used" };

        var user = new AppUserFaker().UseSeed(234).Generate();

        _hasher
            .Setup(h => h.VerifyHashedPassword(user, user.PasswordHash, dto.Password))
            .Returns(PasswordVerificationResult.Success);

        _userRepo
            .Setup(r => r.GetByUsernameAsync(dto.Username))
            .ReturnsAsync(user);

        _authSvc
            .Setup(a => a.CreateToken(user))
            .Returns("jwt-token");

        // Act
        var action = await _controller.Login(dto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(action);
        Assert.NotNull(ok);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        var output = Assert.IsType<LoginResponseDto>(ok.Value);
        Assert.Equal("jwt-token", output.Token);
    }
}
