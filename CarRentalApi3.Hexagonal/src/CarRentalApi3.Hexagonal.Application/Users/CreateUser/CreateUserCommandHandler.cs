using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRentalApi3.Hexagonal.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler(IUserRepository _userRepository, IUnitOfWork _unitOfWork, ILogger<CreateUserCommandHandler> _logger)
 : IRequestHandler<CreateUserCommand, ErrorOr<UserResponse>>
{



    public async Task<ErrorOr<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _userRepository.IsUserExists(request.Email, cancellationToken);

        if (userExists)
        {
            _logger.LogInformation($"User email {request.Email} already exists");

            return Error.Conflict($"User email {request.Email} already exists");
        }

        User user = User.Create(request.Username, request.Email);
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync();

        var response = user.MapToResponse();
        _logger.LogInformation($"User created! {response} ");
        return response;
    }
}
