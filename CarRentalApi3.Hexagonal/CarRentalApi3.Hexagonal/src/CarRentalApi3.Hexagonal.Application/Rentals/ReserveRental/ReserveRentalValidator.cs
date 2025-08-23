using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;

public class ReserveRentalValidator : AbstractValidator<ReserveRentalCommand>
{
    public ReserveRentalValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.CarId).NotEmpty();
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}
