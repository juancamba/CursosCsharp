using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Api.Dto;
using CarRentalApi3.Hexagonal.Application.Extensions;
using CarRentalApi3.Hexagonal.Application.Users.CreateUser;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi3.Hexagonal.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }



    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
       return NoContent();
    }

    // GET: api/users/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return NoContent();
    }

    // POST: api/users
    [HttpPost]
    public async Task<IResult> Create([FromBody] CreateUserDto userdto, CancellationToken cancellationToken)
    {
         var command = new CreateUserCommand(
            userdto.Username,
            userdto.Email
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

    // PUT: api/users/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] User updatedUser, CancellationToken cancellationToken)
    {


        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {


        return NoContent();
    }
}

