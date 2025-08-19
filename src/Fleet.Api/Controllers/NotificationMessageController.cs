using Fleet.Application.UseCases.NotificationMessageUseCases.Register;
using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Fleet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationMessageController : ControllerBase
{
    private readonly ILogger<NotificationMessageController> _logger;
    private readonly INotificationMessageRepository _messageRepo;
    private readonly IMediator _mediator;

    public NotificationMessageController(
        ILogger<NotificationMessageController> logger, // TODO: use logger in methods
        INotificationMessageRepository rentalRepo,
        IMediator mediator)
    {
        _logger = logger;
        _messageRepo = rentalRepo;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Expression<Func<NotificationMessage, object>> selector = u => new
        {
            u.Id,
            u.Message,
            u.ReceivedAt
        };

        var users = await _messageRepo.GetAllAsync(x => true, selector);

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Expression<Func<NotificationMessage, object>> selector = u => new
        {
            u.Id,
            u.Message,
            u.ReceivedAt
        };

        var user = await _messageRepo.GetByIdAsync(id, selector);
        if (user == null)
            return NotFound("Message was not found");

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterNotificationMessageInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return StatusCode(StatusCodes.Status201Created);
    }
}
