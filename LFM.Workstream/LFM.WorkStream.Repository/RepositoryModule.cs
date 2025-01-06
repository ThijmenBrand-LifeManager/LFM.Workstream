using LFM.WorkStream.Repository.Interfaces;
using LFM.WorkStream.Repository.TableRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LFM.WorkStream.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterDatabaseContext(services, configuration);

        services.AddTransient<IWorkStreamRepository, WorkStreamRepository>();
        services.AddTransient<IProjectRepository, ProjectRepository>();
        
        return services;
    }
    
    private static void RegisterDatabaseContext(IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = configuration.GetSection("Database");
        var sslmode = configuration.GetValue<string>("Environment") == "Production" ? SslMode.Require : SslMode.Prefer;
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = databaseConfiguration.GetValue<string>("Host"),
            Port = databaseConfiguration.GetValue<int>("Port"),
            Database = databaseConfiguration.GetValue<string>("Database"),
            Username = databaseConfiguration.GetValue<string>("Username"),
            SslMode = sslmode,
            Password = configuration.GetSection("Database").GetValue<string>("Password")
        }.ToString();
        
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connectionString));
    }
}