using Microsoft.Extensions.DependencyInjection;
using AlphacA.Configuration;
using Raven.Client.Documents;

namespace AlphacA.Storage
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDocumentStore(this IServiceCollection services)
    {
      services.AddTransient(x => MakeDocumentStore(x.GetService<DocumentStorageConfig>()));

      return services;
    }

    private static IDocumentStore MakeDocumentStore(DocumentStorageConfig documentStoreConfig)
    {
      var store = new DocumentStore
      {
        Urls = new string[] { documentStoreConfig.DocumentStoreServerUrl },
        Database = documentStoreConfig.DocumentStoreName
      };

      store.Initialize();

      return store;
    }
  }
}