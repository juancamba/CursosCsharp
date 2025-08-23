using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Cars;

namespace CarRentalApi3.Hexagonal.Domain.Rentals;

public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(Car car, DateRange duration, CancellationToken cancellationToken = default);

    Task AddAsync(Rental rental);
}
 