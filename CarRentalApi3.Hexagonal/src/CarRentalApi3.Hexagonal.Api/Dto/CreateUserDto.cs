using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi3.Hexagonal.Api.Dto;

public record CreateUserDto(string Username, string Email, Guid? Id);
    
