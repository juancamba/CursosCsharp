
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using ErrorOr;
using MediatR;

namespace CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;

public record ReserveRentalCommand(Guid UserId, Guid CarId, DateOnly StartDate, DateOnly EndDate)
    : IRequest<ErrorOr<RentalResponse>>
;
