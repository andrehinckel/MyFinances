using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyFinances.Common.Core.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyDatabaseMigration<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<T>();
        await dbContext.Database.MigrateAsync();
    }
}