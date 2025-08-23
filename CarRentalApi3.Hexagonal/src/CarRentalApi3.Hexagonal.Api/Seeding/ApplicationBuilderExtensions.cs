
using CarRentalApi3.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi3.Hexagonal.Api.Seeding;

public static class ApplicationBuilderExtensions
{

    public static async Task ApplyMigration(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error en migracion");
            }
        }
    }

}