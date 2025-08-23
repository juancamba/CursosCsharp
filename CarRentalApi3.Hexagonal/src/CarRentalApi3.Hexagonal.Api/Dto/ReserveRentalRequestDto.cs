using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Api.Dto;

public record ReserveRentalRequestDto
(
    Guid CarId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);
