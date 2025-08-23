
using CarRentalApi3.Hexagonal.Domain.Cars;

namespace CarRentalApi3.Hexagonal.Application.Shared.Responses;
public sealed record RentalResponse(
    DateOnly StartDate, DateOnly EndDate, decimal Price, CarResponse car

);