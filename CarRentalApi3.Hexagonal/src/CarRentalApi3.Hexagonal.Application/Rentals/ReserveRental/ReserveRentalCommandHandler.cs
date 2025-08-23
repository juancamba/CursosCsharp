using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Shared.Responses;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Cars;
using CarRentalApi3.Hexagonal.Domain.Rentals;
using CarRentalApi3.Hexagonal.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental
{
    public class ReserveRentalCommandHandler : IRequestHandler<ReserveRentalCommand, ErrorOr<RentalResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentalRepository _rentalRespository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly ILogger<ReserveRentalCommandHandler> _logger;

        public ReserveRentalCommandHandler(IUnitOfWork unitOfWork, IRentalRepository rentalRespository, IUserRepository userRepository, ICarRepository carRepository, ILogger<ReserveRentalCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _rentalRespository = rentalRespository;
            _userRepository = userRepository;
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<ErrorOr<RentalResponse>> Handle(ReserveRentalCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                return Error.NotFound($"User '{request.UserId}' not found");

            }

            var car = await _carRepository.GetByIdAsync(request.CarId);

            if (car is null)
            {
                return Error.NotFound($"Car '{request.CarId}' not found");
            }

            var duration = DateRange.Create(request.StartDate, request.EndDate);

            if (await _rentalRespository.IsOverlappingAsync(car, duration, cancellationToken))
            {
                return Error.Conflict($"Rental Overlapping");
            }

            try
            {
                var rental = Rental.Reserve(request.UserId, request.CarId, duration, car);
                await  _rentalRespository.AddAsync(rental);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                RentalResponse response = rental.MapToResponse(car);
                _logger.LogInformation($"Rental saved! {response}");
                return response;

            }
            catch (Exception e)
            {

                _logger.LogError($"Some error to save Rental: {e.StackTrace}");
                return Error.Conflict($"Some error to save Rental");

            }
        }
        
    }
}