using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using ErrorOr;
using MediatR;

namespace CarRentalApi3.Hexagonal.Application.Users.CreateUser;

public sealed record CreateUserCommand(string Username, string Email) : IRequest<ErrorOr<UserResponse>>;

