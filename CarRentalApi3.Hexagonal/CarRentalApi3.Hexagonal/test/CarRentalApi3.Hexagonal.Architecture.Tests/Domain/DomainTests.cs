using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Architecture.Tests.Infrastructure;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using NetArchTest.Rules;

namespace CarRentalApi3.Hexagonal.Architecture.Tests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void Entities_ShoulHave_PrivateConstructorNoParameteres()
    {
        // todas la entidades que heredan de Entity
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity<>))
            .GetTypes();

        var errorEntities = new List<Type>();
        // constructores deben ser privados y no contener parámetros
        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructores = entityType.GetConstructors(
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            if (!constructores.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            {
                errorEntities.Add(entityType);
            }
        }

        //errorEntities.Should().BeEmpty();
        Assert.Empty(errorEntities);

    }
}
