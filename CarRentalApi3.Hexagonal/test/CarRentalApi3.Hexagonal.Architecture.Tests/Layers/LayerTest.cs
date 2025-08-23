using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Application.Extensions;
using CarRentalApi3.Hexagonal.Architecture.Tests.Infrastructure;
using NetArchTest.Rules;

namespace CarRentalApi3.Hexagonal.Architecture.Tests.Layers;

public class LayerTest : BaseTest
{
    [Fact]
    public void DomainLayer_ShouldHaveNotDependency_ApplicationLayer()
    {

        var result = Types.InAssembly(DomainAssembly) // un marker en Domain
            .ShouldNot()
            .HaveDependencyOn(ApplicationAssembly.GetName().Name) // namespace/capa de aplicación
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
    [Fact]
    public void DomainLayer_ShouldHaveNotDependency_InfrastructureLayer()
    {
        //domain no debe tener dependencia hacia infra
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();


        Assert.True(result.IsSuccessful);
    }
    [Fact]
    public void ApplicationLayer_ShouldHaveNotDependency_InfrastructureLayer()
    {

        var result = Types.InAssembly(ApplicationAssembly)
       .Should()
       .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
       .GetResult();

        Assert.True(result.IsSuccessful);
    }
    [Fact]
    public void ApplicationLayer_ShouldHaveNotDependency_PresentationLayer()
    {
        //application no debe tener dependencia de presentation
        var result = Types.InAssembly(ApplicationAssembly)
        .Should()
        .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
        .GetResult();

        Assert.True(result.IsSuccessful);
    }


    [Fact]
    public void InfrastructureLayer_ShouldHaveNotDependency_PresentationLayer()
    {
        //infra no debe tener dependencia hacia presentation
        var result = Types.InAssembly(InfrastructureAssembly)
        .Should()
        .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
        .GetResult();

        Assert.True(result.IsSuccessful);
    }

}
