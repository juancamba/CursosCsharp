using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Users;
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Infrastructure.Repositories;

//public class UserRepository(AppDbContext appDbContext) : IUserRepository
//internal sealed class UserRepository : Repository<User, Guid>, IUserRepository
internal sealed class UserRepository : Repository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
  /*  public void Add(User user)
    {
        throw new NotImplementedException();
    }*/

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.Where(c => c.Email == email).FirstOrDefaultAsync(cancellationToken);
    }

   /* public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
*/
    public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.Where(c => c.Email == email).AnyAsync(cancellationToken);
    }
}
