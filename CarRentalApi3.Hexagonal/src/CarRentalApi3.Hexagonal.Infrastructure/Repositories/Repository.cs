using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
//TEntity debe ser una clase que hereda de Entity<TEntityId>. Esto implica que TEntity tiene al menos una propiedad Id del tipo TEntityId.
where TEntity : Entity<TEntityId>
//TEntityId debe ser una clase (por ejemplo, string, Guid, o cualquier tipo de referencia), no un tipo primitivo como int o float.
where TEntityId : notnull
{
    protected readonly AppDbContext _appDbContext;
    protected Repository(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }
    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Set<TEntity>()
            .ToListAsync(cancellationToken);
    }
    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken);
    }
    public virtual void Add(TEntity entity)
    {
        _appDbContext.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _appDbContext.Update(entity);
    }
    public virtual void Delete(TEntity entity)
    {
        _appDbContext.Remove(entity);
    }
    public async Task DeleteByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            Delete(entity);
        }
    }

 
}