#!/bin/bash

# Nombre del proyecto base
solutionName="CarRentalApi3.Hexagonal"
srcDir="src"
testDir="test"

echo "📁 Creando estructura de carpetas..."
mkdir -p "$srcDir"
mkdir -p "$testDir"

echo "📦 Creando solución..."
dotnet new sln -n "$solutionName"

echo "🚀 Creando proyectos..."
dotnet new webapi -n "$solutionName.Api" -o "$srcDir/$solutionName.Api"
dotnet new classlib -n "$solutionName.Application" -o "$srcDir/$solutionName.Application"
dotnet new classlib -n "$solutionName.Domain" -o "$srcDir/$solutionName.Domain"
dotnet new classlib -n "$solutionName.Infrastructure" -o "$srcDir/$solutionName.Infrastructure"
dotnet new xunit -n "$solutionName.Tests" -o "$testDir/$solutionName.Tests"

echo "🔗 Agregando referencias entre proyectos..."
dotnet add "$srcDir/$solutionName.Application/$solutionName.Application.csproj" reference \
           "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj"

dotnet add "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference \
           "$srcDir/$solutionName.Application/$solutionName.Application.csproj" \
           "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj"

dotnet add "$srcDir/$solutionName.Api/$solutionName.Api.csproj" reference \
           "$srcDir/$solutionName.Application/$solutionName.Application.csproj" \
           "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"

dotnet add "$testDir/$solutionName.Tests/$solutionName.Tests.csproj" reference \
           "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj" \
           "$srcDir/$solutionName.Application/$solutionName.Application.csproj"

echo "📌 Agregando proyectos a la solución..."
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Api/$solutionName.Api.csproj"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Application/$solutionName.Application.csproj"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"
dotnet sln "$solutionName.sln" add "$testDir/$solutionName.Tests/$solutionName.Tests.csproj"

echo "✅ Proyecto '$solutionName' generado con arquitectura hexagonal."
