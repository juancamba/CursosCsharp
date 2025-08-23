using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Abstractions;

namespace CarRentalApi3.Hexagonal.Domain.Users
{
    public interface IUserRepository : IRepository<User, Guid>
    {

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> IsUserExists(string email, CancellationToken cancellationToken = default);
    }
}