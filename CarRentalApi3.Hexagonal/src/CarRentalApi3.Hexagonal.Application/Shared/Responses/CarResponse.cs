using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Application.Shared.Responses;
public sealed record CarResponse(
    Guid Id,
    string Brand,
    string Model,
    decimal Price,
    int Year
);
