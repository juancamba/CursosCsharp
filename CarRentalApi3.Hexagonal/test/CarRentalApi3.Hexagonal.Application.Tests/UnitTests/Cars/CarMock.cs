using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Cars;

namespace CarRentalApi3.Hexagonal.Application.Tests.UnitTests.Cars;

internal static class CarMock
{
    public static Car Create() => Car.Create("Car XX", "Model XX", 100, 2025);
}
