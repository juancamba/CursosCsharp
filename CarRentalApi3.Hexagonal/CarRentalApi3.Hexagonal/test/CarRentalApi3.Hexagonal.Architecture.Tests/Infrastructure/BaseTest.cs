using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Extensions;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Infrastructure.Database;

namespace CarRentalApi3.Hexagonal.Architecture.Tests.Infrastructure;
public class BaseTest
{
    //quien representa Application ? IBaseCommand

    protected static readonly Assembly ApplicationAssembly = typeof(MapEndpointExtensions).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(IEntity).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(AppDbContext).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;

}
