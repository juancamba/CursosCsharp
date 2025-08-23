using System.Threading.Tasks;
using CarRental.Api.Controllers;
using CarRental.Api.Dto;
using CarRental.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class RentalsControllerTests
{
    [Fact]
    public async Task CreateRental_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var rentalDto = new RentalDto();
        var rentalResult = new RentalResult
        {
            Success = true,
            Rental = new CarRental.Api.Models.Rental { Id = 1 },
            TotalPrice = 100
        };

        var serviceMock = new Mock<IRentalService>();
        serviceMock.Setup(s => s.CreateRentalAsync(rentalDto))
            .ReturnsAsync(rentalResult);

        var controller = new RentalsController(serviceMock.Object);

        // Act
        var result = await controller.CreateRental(rentalDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        dynamic value = okResult.Value;
        Assert.Equal(1, (int)value.Id);
        Assert.Equal(100, (int)value.TotalPrice);
    }

    [Fact]
    public async Task CreateRental_ReturnsNotFound_WhenCarNotFound()
    {
        var rentalDto = new RentalDto();
        var rentalResult = new RentalResult
        {
            Success = false,
            Error = "Car not found."
        };

        var serviceMock = new Mock<IRentalService>();
        serviceMock.Setup(s => s.CreateRentalAsync(rentalDto))
            .ReturnsAsync(rentalResult);

        var controller = new RentalsController(serviceMock.Object);

        var result = await controller.CreateRental(rentalDto);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task CreateRental_ReturnsConflict_WhenCarNotAvailable()
    {
        var rentalDto = new RentalDto();
        var rentalResult = new RentalResult
        {
            Success = false,
            Error = "Car is not available for the selected period."
        };

        var serviceMock = new Mock<IRentalService>();
        serviceMock.Setup(s => s.CreateRentalAsync(rentalDto))
            .ReturnsAsync(rentalResult);

        var controller = new RentalsController(serviceMock.Object);

        var result = await controller.CreateRental(rentalDto);

        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task CreateRental_ReturnsBadRequest_WhenOtherError()
    {
        var rentalDto = new RentalDto();
        var rentalResult = new RentalResult
        {
            Success = false,
            Error = "Unknown error"
        };

        var serviceMock = new Mock<IRentalService>();
        serviceMock.Setup(s => s.CreateRentalAsync(rentalDto))
            .ReturnsAsync(rentalResult);

        var controller = new RentalsController(serviceMock.Object);

        var result = await controller.CreateRental(rentalDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }