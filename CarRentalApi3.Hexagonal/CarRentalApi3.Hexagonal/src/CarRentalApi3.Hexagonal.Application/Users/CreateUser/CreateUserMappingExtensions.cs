using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using CarRentalApi3.Hexagonal.Domain.Users;

namespace CarRentalApi3.Hexagonal.Application.Users.CreateUser;

internal static class CreateUserMappingExtensions
{
    public static UserResponse MapToResponse(this User user) => new UserResponse(user.Id, user.Username, user.Email);
}
