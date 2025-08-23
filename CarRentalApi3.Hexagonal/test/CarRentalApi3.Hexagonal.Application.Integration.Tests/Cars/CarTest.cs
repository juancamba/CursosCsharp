using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Integration.Tests.Infrastructure;
using CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Users;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Application.Integration.Tests.Cars;



public class CarTest : BaseIntegrationTest
{
    public CarTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task ReserveRental_ShouldReturnRental_WhenDateRangeIsValid()
    {

        Car? car = await dbContext.Cars.FirstOrDefaultAsync();
        //User? user1 = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "user1");
        User? user1 = await dbContext.Users.FirstOrDefaultAsync();
        Console.WriteLine($"USUARIO::::::::::: {user1.Username}");
        DateOnly dateStart = DateOnly.Parse("2025-08-20");
        DateOnly dateEnd = DateOnly.Parse("2025-08-23");
        var command = new ReserveRentalCommand(
            user1!.Id,
            car!.Id,
            dateStart,
            dateEnd
        );


        var resultado = await Sender.Send(command);
        Assert.True(!resultado.IsError);

    }
    [Fact]

    public async Task ReserveRental_ShouldReturnError_WhenDateRangeIsInvalid()
    {

        Car? car = await dbContext.Cars.FirstOrDefaultAsync();
        User? user1 = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "user2");
        //User? user1 = await dbContext.Users.FirstOrDefaultAsync();
        Console.WriteLine($"[test 2]USUARIO::::::::::: {user1.Username}");
        DateOnly dateStart = DateOnly.Parse("2025-08-20");
        DateOnly dateEnd = DateOnly.Parse("2025-08-23");
        var command = new ReserveRentalCommand(
            user1!.Id,
            car!.Id,
            dateStart,
            dateEnd
        );

        //Rental Overlapping
        var resultado = await Sender.Send(command);
        var error = resultado.Errors.FirstOrDefault();
        Assert.Equal(ErrorType.Conflict, error.Type);
        Assert.Contains("Overlap", error.Code);
        Assert.True(resultado.IsError);
        

    }
}
