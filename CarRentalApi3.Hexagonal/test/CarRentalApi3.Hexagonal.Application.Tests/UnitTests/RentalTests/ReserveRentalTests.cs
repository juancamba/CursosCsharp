using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using CarRentalApi3.Hexagonal.Application.Tests.UnitTests.Cars;
using CarRentalApi3.Hexagonal.Application.Tests.UnitTests.Users;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Rentals;
using CarRentalApi3.Hexagonal.Domain.Users;
using ErrorOr;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarRentalApi3.Hexagonal.Application.Tests.UnitTests.RentalTests;



public class ReserveRentalTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IRentalRepository> _rentalRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ICarRepository> _carRepositoryMock = new();
    private readonly Mock<ILogger<ReserveRentalCommandHandler>> _loggerMock = new();

    private ReserveRentalCommandHandler CreateHandler()
    {
        return new ReserveRentalCommandHandler(
            _unitOfWorkMock.Object,
            _rentalRepositoryMock.Object,
            _userRepositoryMock.Object,
            _carRepositoryMock.Object,
            _loggerMock.Object
        );
    }
    [Fact]
    public async Task Handle_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new ReserveRentalCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        );

        _userRepositoryMock
            .Setup(r => r.GetByIdAsync(command.UserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null); // Usuario no encontrado

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(ErrorType.NotFound, result.FirstError.Type);
        //Assert.Contains("User", result.FirstError.Description);
    }
    [Fact]
    public async Task Handle_ReturnsNotFound_WhenCarDoesNotExist()
    {
        // Arrange
        var command = new ReserveRentalCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        );

        _userRepositoryMock
            .Setup(r => r.GetByIdAsync(command.UserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(UserMock.Create()); // Usuario no encontrado

        _carRepositoryMock
            .Setup(r => r.GetByIdAsync(command.CarId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Car?)null);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(ErrorType.NotFound, result.FirstError.Type);
        //Assert.Contains("User", result.FirstError.Description);
    }
    [Fact]
    public async Task Handle_ReturnsConflict_WhenRentalOverlaps()
    {
        var command = new ReserveRentalCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        );

        var fakeUser = UserMock.Create();
        var fakeCar = CarMock.Create();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(command.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(fakeUser);
        _carRepositoryMock.Setup(r => r.GetByIdAsync(command.CarId, It.IsAny<CancellationToken>())).ReturnsAsync(fakeCar);
        _rentalRepositoryMock.Setup(r => r.IsOverlappingAsync(fakeCar, It.IsAny<DateRange>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(true); // Simula solapamiento

        var handler = CreateHandler();

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal(ErrorType.Conflict, result.FirstError.Type);
        
    }
    [Fact]
    public async Task Handle_ReturnsRentalResponse_WhenSuccess()
    {
        int days = 2;
        var command = new ReserveRentalCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(days))
        );
        var fakeUser = UserMock.Create();
        var fakeCar = CarMock.Create();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(command.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(fakeUser);
        _carRepositoryMock.Setup(r => r.GetByIdAsync(command.CarId, It.IsAny<CancellationToken>())).ReturnsAsync(fakeCar);
        _rentalRepositoryMock.Setup(r => r.IsOverlappingAsync(fakeCar, It.IsAny<DateRange>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(false);

        _rentalRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Rental>())).Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = CreateHandler();

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsError);
        Assert.IsType<RentalResponse>(result.Value);

        decimal rentalPrice = days * fakeCar.Price;

        Assert.Equal(result.Value.Price, rentalPrice);
    }
}
