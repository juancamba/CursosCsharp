using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Domain.Cars;

public interface ICarRepository
{

    Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    //Task<IReadOnlyList<Car>> GetAllWithSpec(ISpecification<Vehiculo, VehiculoId> spec);

    //Task<int> CountAsync(ISpecification<Vehiculo, VehiculoId> spec);
}
