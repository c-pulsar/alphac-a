using Raven.Client.Documents;

namespace Pulsar.AlphacA.DocumentStorage
{
  public class DocumentStoreBuilder
  {
    private readonly IDocumentStore store;

    public DocumentStoreBuilder(IDocumentStore store)
    {
      this.store = store;
    }

    public void Build()
    {
      store.CreateStoreIfDoesntExist(store.Database);
    }
  }
}