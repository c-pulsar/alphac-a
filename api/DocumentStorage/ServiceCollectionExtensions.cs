using Microsoft.Extensions.DependencyInjection;
using Pulsar.AlphacA.Configuration;
using Raven.Client.Documents;

namespace Pulsar.AlphacA.DocumentStorage
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDocumentStore(this IServiceCollection services)
    {
      services.AddTransient(x => MakeDocumentStore(x.GetService<DatabaseConfig>()));

      return services;
    }

    private static IDocumentStore MakeDocumentStore(DatabaseConfig databaseConfig)
    {
      var store = new DocumentStore
      {
        Urls = new string[] { databaseConfig.DatabaseServerUrl },
        Database = databaseConfig.DatabaseName
      };

      store.Initialize();

      return store;
    }
  }
}