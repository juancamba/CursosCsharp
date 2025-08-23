using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Rentals;

namespace CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;

internal static class ReserveRentalMappingExtensions
{
    public static RentalResponse MapToResponse(this Rental rental, Car car) => new(
        rental.Duracion!.Start,
        rental.Duracion.End,
        rental.Price,
        new CarResponse(
            car.Id,
            car.Brand,
            car.Model,
            car.Price,
            car.Year
        )
    );
}
