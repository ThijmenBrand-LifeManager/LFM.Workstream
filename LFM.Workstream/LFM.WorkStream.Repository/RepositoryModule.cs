using LFM.WorkStream.Repository.Interfaces;
using LFM.WorkStream.Repository.TableRepository.WorkStreamRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.WorkStream.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(configuration.GetSection("Postgres").GetValue<string>("ConnectionString")));

        services.AddTransient<IWorkStreamRepository, WorkStreamRepository>();
        
        return services;
    }
}