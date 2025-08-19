using Fleet.Api.ViewModels;
using Fleet.Application.UseCases.RentalUseCases.InformReturnDate;
using Fleet.Application.UseCases.RentalUseCases.Register;
using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Fleet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RentalController : ControllerBase
{
    private readonly ILogger<RentalController> _logger;
    private readonly IRentalRepository _rentalRepo;
    private readonly IMediator _mediator;

    public RentalController(
        ILogger<RentalController> logger, // TODO: use logger in methods
        IRentalRepository rentalRepo,
        IMediator mediator)
    {
        _logger = logger;
        _rentalRepo = rentalRepo;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Expression<Func<Rental, RentalListlVM>> selector = u => new RentalListlVM
        {
            Id = u.Id,
            DeliverymanIdentifier = u.Deliveryman!.AppUser.Username,
            VehicleIdentifier = u.Vehicle!.Identifier,
            StartDate = u.StartDate,
            EndDate = u.EndDate,
        };

        var users = await _rentalRepo.GetAllAsync(x => true, selector);

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Expression<Func<Rental, RentalVM>> selector = u => new RentalVM
        {
            Id = u.Id,
            DailyRate = u.DailyRate / 100.0, // convert cents to currency units
            DeliverymanIdentifier = u.Deliveryman!.AppUser.Username,
            VehicleIdentifier = u.Vehicle!.Identifier,
            StartDate = u.StartDate,
            EndDate = u.EndDate,
            ExpectedEndDate = u.ExpectedEndDate,
            ReturnedAt = u.ReturnedAt,

            TotalCost = u.TotalCost / 100.0,
            Penalty = u.Penalty / 100.0,
        };

        var user = await _rentalRepo.GetByIdAsync(id, selector);
        if (user == null)
            return NotFound("Deliveryman was not found");

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRentalInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}/return")]
    public async Task<IActionResult> InformReturnDate(Guid id, [FromBody] InformReturnDateInput input)
    {
        input.Id = id;

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return Ok("Return date was successfully updated");
    }
}
