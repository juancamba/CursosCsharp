using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Users;
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalApi3.Hexagonal.Application.Integration.Tests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{

    private readonly IServiceScope _scope;
    protected readonly ISender Sender;

    protected readonly AppDbContext dbContext;

    

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        dbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();


        Car car = Car.Create("Dodge", "Charger", 100, 2022);
        User user1 = User.Create("user1", "cambacodelabs1@email.com");
        User user2 = User.Create("user2", "cambacodelabs2@email.com");

        if (!dbContext.Cars.Any())
        {
            dbContext.Cars.Add(car);
            dbContext.SaveChanges();
        }
        if (!dbContext.Users.Any())
        {
            dbContext.Users.Add(user2);
            dbContext.Users.Add(user1);
            dbContext.SaveChanges();
        }
    }

}