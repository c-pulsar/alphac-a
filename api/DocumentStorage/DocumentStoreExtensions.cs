using System;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace AlphacA.DocumentStorage
{
  public static class DocumentStoreExtensions
  {
    public static IDocumentStore CreateStoreIfDoesntExist(this IDocumentStore store, string documentStoreName)
    {
      if (store.Exists(documentStoreName))
      {
        Console.WriteLine($"Document store [{documentStoreName}] already exists");
        // TODO: add logs
      }
      else
      {
        Console.WriteLine($"Creating document store [{documentStoreName}]");
        store.Create(documentStoreName);
        Console.WriteLine($"Document store [{documentStoreName}] created.");
      }

      return store;
    }

    private static IDocumentStore Create(this IDocumentStore store, string dbName)
    {
      try
      {
        store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(dbName)));
      }
      catch (ConcurrencyException)
      {
        // The database was already created before calling CreateDatabaseOperation
        // TODO: Log here
      }

      return store;
    }

    private static bool Exists(this IDocumentStore store, string dbName)
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
    }
  }
}