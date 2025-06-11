using Fleet.Api._2_Application.UseCases;
using Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;
using Fleet.Api._2_Application.UseCases.VehicleUseCases.Remove;
using Fleet.Api._3_Domain.Entities;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Fleet.Api._1_Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class MotosController : ControllerBase
{
    private readonly ILogger<MotosController> _logger;
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IMediator _mediator;

    public MotosController(
        ILogger<MotosController> logger, // TODO: use logger in methods
        IVehicleRepository vehicleRepo,
        IMediator mediator)
    {
        _logger = logger;
        _vehicleRepo = vehicleRepo;
        _mediator = mediator;
    }

    // TODO: implement authorization for these endpoints

    [HttpGet]
    public async Task<IActionResult> GetAllVehicle([FromQuery] string? placa)
    {
        Expression<Func<Vehicle, bool>> predicate = x =>
            string.IsNullOrEmpty(placa) || x.LicensePlate == placa;

        Expression<Func<Vehicle, object>> selector = x => new
        {
            Identificador = x.Identifier,
            Ano = x.Year,
            Modelo = x.Model,
            Placa = x.LicensePlate,
        };

        var vehicles = await _vehicleRepo.GetAllAsync(predicate, selector);

        var output = BaseOutput<IEnumerable<object>>.Success(vehicles);

        return Ok(output);
    }

    [HttpGet("{identificador}")]
    public async Task<IActionResult> GetDeliverymanByUsername(string identificador)
    {
        var vehicle = await _vehicleRepo.GetByIdentifierAsync(identificador);

        if (vehicle == null)
            return NotFound(BaseOutput<string>.Failure("Moto não encontrada"));

        var output = BaseOutput<object>.Success(new
        {
            Identificador = vehicle.Identifier,
            Ano = vehicle.Year,
            Modelo = vehicle.Model,
            Placa = vehicle.LicensePlate,
        });

        return Ok(output);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterVehicleInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Dados inválidos");

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        RemoveVehicleInput input = new(id);

        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok();
    }
}