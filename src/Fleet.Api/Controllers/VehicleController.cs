using Fleet.Api.ViewModels;
using Fleet.Application.UseCases.VehicleUseCases.Edit;
using Fleet.Application.UseCases.VehicleUseCases.Register;
using Fleet.Application.UseCases.VehicleUseCases.Remove;
using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Fleet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly ILogger<VehicleController> _logger;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IMediator _mediator;

    public VehicleController(
        ILogger<VehicleController> logger, // TODO: use logger in methods
        IVehicleRepository vehicleRepo,
        IMediator mediator)
    {
        _logger = logger;
        _vehicleRepo = vehicleRepo;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVehicle([FromQuery] string? placa)
    {
        Expression<Func<Vehicle, bool>> predicate = x =>
            string.IsNullOrEmpty(placa) || x.LicensePlate == placa;

        Expression<Func<Vehicle, VehicleVM>> selector = x => new VehicleVM
        {
            Identifier = x.Identifier,
            Year = x.Year,
            Model = x.Model,
            LicensePlate = x.LicensePlate,
        };

        var vehicles = await _vehicleRepo.GetAllAsync(predicate, selector);

        return Ok(vehicles);
    }

    [HttpGet("{identifier}")]
    public async Task<IActionResult> GetDeliverymanByUsername(string identifier)
    {
        var vehicle = await _vehicleRepo.GetByIdentifierAsync(identifier);

        if (vehicle == null)
            return NotFound("Vehicle not found");

        var output = new VehicleVM
        {
            Identifier = vehicle.Identifier,
            Year = vehicle.Year,
            Model = vehicle.Model,
            LicensePlate = vehicle.LicensePlate,
        };

        return Ok(output);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterVehicleInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}/plate")]
    public async Task<IActionResult> Edit(string id, [FromBody] EditVehicleInput input)
    {
        input.Identifier = id;

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return Ok("License plate updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        RemoveVehicleInput input = new(id);

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Invalid data");

        return Ok();
    }
}