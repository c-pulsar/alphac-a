using Microsoft.AspNetCore.Builder;
using Raven.Client.Documents;

namespace AlphacA.DocumentStorage
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseDocumentStoreBuilder(this IApplicationBuilder app)
    {
      if (app.ApplicationServices.GetService(typeof(IDocumentStore)) is IDocumentStore documentStore)
      {
        var builder = new DocumentStoreBuilder(documentStore);
        builder.Build();
      }
      else
      {
        // TODO: log here
      }

      return app;
    }
  }
}