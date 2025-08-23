using System.Text.Json.Serialization;
using CarRentalApi3.Hexagonal.Api.Converters;
using CarRentalApi3.Hexagonal.Api.Dto;
using CarRentalApi3.Hexagonal.Api.Seeding;
using CarRentalApi3.Hexagonal.Application;
using CarRentalApi3.Hexagonal.Application.Extensions;
using CarRentalApi3.Hexagonal.Application.Rentals.ReserveRental;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Infrastructure;
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFeatures(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
/*
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
*/
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();
await app.ApplyMigration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.SeedData();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program;