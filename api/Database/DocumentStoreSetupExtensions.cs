using System;
using Microsoft.AspNetCore.Builder;
using Raven.Client.Documents;

namespace Pulsar.AlphacA.Database
{
  public static class DocumentStoreSetupExtensions
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

      }

      //EnsureDabaseIsCreated(app);

      // using var scope = app.ApplicationServices.CreateScope();
      // var dbCreator = scope.ServiceProvider.GetService<IDatabaseCreatorService>();
      // if(dbCreator!=null)
      // {
      //     dbCreator.Create();
      // }

      // var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
      // runner.MigrateUp();

      return app;
    }
  }
}