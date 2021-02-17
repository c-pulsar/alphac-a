using System;
using System.Collections.Generic;
using AlphacA.Exceptions;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.CompareExchange;

namespace AlphacA.Storage.CompareExchange
{
  public sealed class CompareExchangeScope : IDisposable
  {
    private readonly IDocumentStore documentStore;
    private readonly List<KeyValuePair<string, long>> rollbackList;

    public CompareExchangeScope(IDocumentStore documentStore)
    {
      this.documentStore = documentStore;
      this.rollbackList = new List<KeyValuePair<string, long>>();
    }

    public CompareExchangeScope Add(string key, string value, string conflictMessage)
    {
      var result = this.documentStore.Operations.Send(
        new PutCompareExchangeValueOperation<string>(key, value, 0));

      if (result.Successful)
      {
        this.rollbackList.Add(new KeyValuePair<string, long>(key, result.Index));
        Console.WriteLine($"Compare exchange key {key} created.");
      }
      else
      {
        throw new ResourceConflictException(conflictMessage);
      }

      return this;
    }

    public void Complete()
    {
      this.rollbackList.Clear();
    }

    void IDisposable.Dispose()
    {
      foreach (var exchange in this.rollbackList)
      {
        var result = this.documentStore.Operations.Send(
          new DeleteCompareExchangeValueOperation<string>(exchange.Key, exchange.Value));

        if (result.Successful)
        {
          // TODO: LOG HERE
          Console.WriteLine($"Compare exchange key {exchange.Key} deleted");
        }
        else
        {
          Console.WriteLine($"Compare exchange key {exchange.Key} could not be deleted");
        }
      }
    }
  }
}