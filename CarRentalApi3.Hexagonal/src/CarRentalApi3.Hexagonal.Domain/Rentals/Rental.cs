using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Users;
using ErrorOr;

namespace CarRentalApi3.Hexagonal.Domain.Rentals;

public sealed class Rental : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid CarId { get; private set; }
    public DateRange? Duracion { get; private set; }

    public RentalStatus Status { get; private set; }

    public decimal Price { get; private set; }



    private Rental() { }

    private Rental(Guid id, Guid userId, Guid carId, DateRange? duracion, RentalStatus status, decimal price)
    {
        Id = id;
        UserId = userId;
        CarId = carId;
        Duracion = duracion;
        Status = status;
        Price = price;
    }

    public static Rental Reserve(Guid userId, Guid carId, DateRange? duracion, Car car)
    {

        decimal totalPrice = car.Price * duracion!.CantidadDias;
        var rental = new Rental(Guid.NewGuid(), userId, carId, duracion, RentalStatus.Reserved, totalPrice);

        // raise event if you want
        return rental;
    }

    public ErrorOr<Success> Start()
    {
        if (Status != RentalStatus.Reserved)
        {
            return Error.Validation("Can only Start a Rental from Reserved status");
        }
        Status = RentalStatus.InProgress;
        return Result.Success;
    }

    public ErrorOr<Success> Complete()
    {
        if (Status != RentalStatus.InProgress)
        {
            return Error.Validation("Can only Complete a Rental from InProgress");
        }
        Status = RentalStatus.Completed;
        
        return Result.Success;
    }
    public ErrorOr<Success> Cancel()
    {
        if (Status != RentalStatus.InProgress)
        {
            return Error.Validation("Can only Cancel a Rental from Reserved InProgress");
        }
        Status = RentalStatus.Canceled;
        return Result.Success;
    }
}
