using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Users;

namespace CarRentalApi3.Hexagonal.Application.Tests.UnitTests.Users;

internal static class UserMock
{
    public static User Create() => User.Create("Mock_username", "mock@mail.com");
}
