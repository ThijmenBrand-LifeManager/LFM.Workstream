using LFM.WorkStream.Repository;
using Microsoft.EntityFrameworkCore;

namespace LFM.WorkStream.Api.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        
        context.Database.Migrate();
    }
}