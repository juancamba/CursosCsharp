using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Rentals;
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Infrastructure.Repositories;

public class RentalRepository(AppDbContext appDbContext) : IRentalRepository
{
   private static readonly RentalStatus[] ActiveRentalStatus = {
        RentalStatus.Reserved,
        RentalStatus.InProgress,
        
    };

    public async Task AddAsync(Rental rental)
    {
        await appDbContext.Rentals.AddAsync(rental);
    }

    public async Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await appDbContext.Rentals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsOverlappingAsync(Car car, DateRange duration, CancellationToken cancellationToken = default)
    {

       
        bool isCarAvailable = !await appDbContext.Rentals.AnyAsync(rentalInDb =>
               rentalInDb.CarId == car.Id &&
               (
                   (duration.Start >= rentalInDb.Duracion!.Start && duration.Start < rentalInDb.Duracion.End) ||
                   (duration.End > rentalInDb.Duracion.Start && duration.End <= rentalInDb.Duracion.End) ||
                   (duration.Start <= rentalInDb.Duracion.Start && duration.End >= rentalInDb.Duracion.End)
               ));

        return !isCarAvailable;                
    }
}
