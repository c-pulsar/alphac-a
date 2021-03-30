using Raven.Client.Documents;

namespace AlphacA.Storage
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
      store.CreateStoreIfDoesntExist(store.Database).InitialiseIndexes();
    }
  }
}