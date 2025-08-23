using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Infrastructure.Repositories;

public class CarRepository(AppDbContext appDbContext) : ICarRepository
{
    public async Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Cars.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
