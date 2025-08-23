using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Api.Controllers;
using CarRentalApi3.Hexagonal.Api.Dto;
using CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarRentalApi3.Hexagonal.Api.Tests.Controllers;

public class RentalsControllerTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly RentalsController _controller;
    public RentalsControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new RentalsController(_mediatorMock.Object);
    }
    [Fact]
    public async Task ReserveRental_ReturnsOk_WhenReservationIsSuccessful()
    {
        // Arrange
        var request = new ReserveRentalRequestDto
        (
           Guid.NewGuid(),
           Guid.NewGuid(),
           DateOnly.FromDateTime(DateTime.UtcNow),
           DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        );


        var carResponse = new CarResponse(
            request.CarId,
            "Ford",
            "Fiesta",
            100m,
            2024
        );

        var rentalResponse = new RentalResponse(
            request.StartDate,
            request.EndDate,
            250m,
            carResponse
        );

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ReserveRentalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(rentalResponse); // éxito

        // Act
        var result = await _controller.ReserveRental(request, CancellationToken.None);

        // Assert
        //var okResult = Assert.IsType<OkObjectResult>(result);
        //var value = Assert.IsType<RentalResponse>(okResult.Value);
        var okResult = Assert.IsType<Ok<RentalResponse>>(result);
        Assert.Equal(rentalResponse, okResult.Value);


        //Assert.Equal(rentalResponse, value); // Igualdad estructural de records
    }

    [Fact]
    public async Task ReserveRental_ReturnsProblem_WhenReservationFails()
    {
        // Arrange
        var request = new ReserveRentalRequestDto
        (
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        );

        var error = Error.Conflict("Rental Overlapping");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ReserveRentalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(error); // fallo

        // Act
        var result = await _controller.ReserveRental(request, CancellationToken.None);

        // Assert
        var problemResult = Assert.IsType<ProblemHttpResult>(result);
        Assert.Equal(StatusCodes.Status409Conflict, problemResult.StatusCode);
    }
}
