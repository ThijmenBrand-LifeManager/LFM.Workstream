using Azure.Data.Tables;
using LFM.WorkStream.Core;
using LFM.WorkStream.Repository.Interfaces;
using LFM.WorkStream.Repository.TableRepository;
using LFM.WorkStream.Repository.TableRepository.WorkStreamTableRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.WorkStream.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITableClientFactory, TableClientFactory>();

        services.AddTransient<IWorkStreamTableRepository, WorkStreamTableRepository>(provider =>
            new WorkStreamTableRepository(GetTableServiceClient(provider, configuration)));
        services.AddTransient<IInitializer, WorkStreamTableRepository>(provider =>
            new WorkStreamTableRepository(GetTableServiceClient(provider, configuration)));
        

        return services;
    }

    private static TableServiceClient GetTableServiceClient(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var clientFactory = serviceProvider.GetRequiredService<ITableClientFactory>();
        
         var connectionString = configuration.GetSection("TableStorage").GetValue<string>("ConnectionString");
         if (!string.IsNullOrEmpty(connectionString))
             return clientFactory.CreateTableClientByConnectionString(connectionString);
         
         throw new Exception("Failed to configure TableServiceClient for LFM.WorkStream." +
                             "Config entry TableStorage.StorageAccountConnectionString must be present");
    }
}