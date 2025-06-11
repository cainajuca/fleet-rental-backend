using Fleet.Api._2_Application.UseCases.VehicleUseCases.Register;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterVehicleInput input)
    {
        var result = await _mediator.Send(input);

        if (!result.IsSuccess)
            return BadRequest("Dados inválidos");

        return StatusCode(StatusCodes.Status201Created);
    }
}