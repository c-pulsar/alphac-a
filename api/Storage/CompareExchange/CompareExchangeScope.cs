using System;
using System.Collections.Generic;
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

    public CompareExchangeScope Start(params KeyValuePair<string, string>[] compareExchangePairs)
    {
      foreach (var cep in compareExchangePairs)
      {
        var result = this.documentStore.Operations.Send(
              new PutCompareExchangeValueOperation<string>(
              cep.Key, cep.Value, 0));

        if (result.Successful)
        {
          this.rollbackList.Add(new KeyValuePair<string, long>(cep.Key, result.Index));
          Console.WriteLine($"Compare exchange key {cep.Key} created.");
        }
        else
        {
          throw new InvalidOperationException($"Compare exchange key {cep.Key} already exists");
        }
      }

      return this;
    }

    public void Complete()
    {
      this.rollbackList.Clear();
    }

    void IDisposable.Dispose()
    {
      foreach (var cep in this.rollbackList)
      {
        // var existing = this.documentStore.Operations.Send(
        //   new GetCompareExchangeValueOperation<string>(cep.Key));

        var result = this.documentStore.Operations.Send(
          new DeleteCompareExchangeValueOperation<string>(cep.Key, cep.Value));

        if (result.Successful)
        {
          // TODO: LOG HERE
          Console.WriteLine($"Compare exchange key {cep.Key} deleted");
        }
        else
        {
          Console.WriteLine($"Compare exchange key {cep.Key} could not be deleted");
        }
      }
    }
  }
}