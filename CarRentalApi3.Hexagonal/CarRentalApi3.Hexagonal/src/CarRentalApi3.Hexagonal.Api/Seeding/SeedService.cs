using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Users;
using CarRentalApi3.Hexagonal.Infrastructure.Database;

namespace CarRentalApi3.Hexagonal.Api.Seeding;

public static class SeedService
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();


        try
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Cars.Any())
            {
                var car = Car.Create("Mercedez Benz", "E320", 150, 2025);


                context.Add(car);
                var car2 = Car.Create("BMW", "M5", 180, 2024);
                context.Add(car2);
                context.SaveChangesAsync().Wait();
            }

            if (!context.Users.Any())
            {
                var user1 = User.Create("CambaCodeLabs", "CambaCodeLabs@CambaCodeLabs.com");
                var user2 = User.Create("Bill Gates", "billgates@microsoft.com");
                context.Add(user1);
                context.Add(user2);
                context.SaveChangesAsync().Wait();

            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
