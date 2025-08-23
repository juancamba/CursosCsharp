using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Application.Shared.Responses;

public record UserResponse(Guid? Id, string Username, string Email);