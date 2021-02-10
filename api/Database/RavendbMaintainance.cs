using System;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide.Operations;

namespace Pulsar.AlphacA.Database
{
  public class RavendbMaintainance
  {
    private readonly string url;
    private readonly string dbName;

    public RavendbMaintainance(string url, string dbName)
    {
      this.url = url;
      this.dbName = dbName;
    }

    public void Setup()
    {
      using var store = MakeStore(url, dbName);
      store.Initialize();
      EnsureDbIsCreated(store);
    }

    private void EnsureDbIsCreated(DocumentStore store)
    {
      if (!DbExists(store, dbName))
      {
        Console.WriteLine("Creating DB...");
      }
      else
      {
        Console.WriteLine("DbAlready Exists");
      }
    }

    private static bool DbExists(DocumentStore store, string dbName)
    {
      try
      {
        store.Maintenance.ForDatabase(dbName).Send(new GetStatisticsOperation());
        return true;
      }
      catch (DatabaseDoesNotExistException)
      {
        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return false;
      }
    }

    private static DocumentStore MakeStore(string url, string dbName)
    {
      return new DocumentStore
      {
        Urls = new string[] { url },
        Database = dbName
      };
    }
  }
}