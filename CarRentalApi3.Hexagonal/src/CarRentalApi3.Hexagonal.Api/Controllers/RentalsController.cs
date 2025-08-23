using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Api.Dto;
using CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarRentalApi3.Hexagonal.Application.Extensions;
namespace CarRentalApi3.Hexagonal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{

    private readonly IMediator _mediator;

    public RentalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("reserve")]
    public async Task<IResult> ReserveRental([FromBody] ReserveRentalRequestDto request, CancellationToken cancellationToken)
    {
        var command = new ReserveRentalCommand(
            request.UserId,
            request.CarId,
            request.StartDate,
            request.EndDate
        );

        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsError)
        {
            return response.Errors.ToProblem(); // Asumo que tienes una extensión para esto
        }

        // Si quieres devolver CreatedAtRoute en lugar de Ok:
        // return CreatedAtRoute(nameof(GetAlquiler), new { id = response.Value }, response.Value);

        return Results.Ok(response.Value);
    }
}
