using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Rentals;
using CarRentalApi3.Hexagonal.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Infrastructure.Database;

public class AppDbContext : DbContext, IUnitOfWork
//public class AppDbContext(DbContextOptions<AppDbContext> options)	: DbContext(options), IUnitOfWork

{
    public DbSet<User> Users { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    public DbSet<Car> Cars { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public AppDbContext(){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        //modelBuilder.HasDefaultSchema("rentals");

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(v => v.Id);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.OwnsOne(r => r.Duracion, dr =>
            {
                dr.Property(d => d.Start)
                    .HasColumnName("StartDate")
                    .IsRequired();

                dr.Property(d => d.End)
                    .HasColumnName("EndDate")
                    .IsRequired();

                dr.Ignore(d => d.CantidadDias); // No se mapea, es calculado
            });

            entity.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            entity.OwnsOne(alquiler => alquiler.Duracion);

            entity.HasOne<Car>()
                .WithMany()
                .HasForeignKey(rental => rental.CarId);

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(rental => rental.UserId);
        });
   
    }
}
