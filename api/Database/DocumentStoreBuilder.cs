using Raven.Client.Documents;

namespace Pulsar.AlphacA.Database
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
      this.store.CreateStoreIfDoesntExist(store.Database);
    }
  }
}