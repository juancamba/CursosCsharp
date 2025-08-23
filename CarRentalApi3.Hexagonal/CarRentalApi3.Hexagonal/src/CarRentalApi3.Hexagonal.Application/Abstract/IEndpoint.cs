using Microsoft.AspNetCore.Builder;

namespace CarRentalApi3.Hexagonal.Application.Abstract;

public interface IEndpoint
{
    void MapEndpoint(WebApplication app);
}