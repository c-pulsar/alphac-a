using AlphacA.Resources.Users;

namespace AlphacA.Resources.Root
{
  public class RootRepresentationAdapter
  {
    private readonly RootUriFactory rootUriFactory;
    private readonly UserUriFactory userUriFactory;

    public RootRepresentationAdapter(RootUriFactory rootUriFactory, UserUriFactory userUriFactory)
    {
      this.rootUriFactory = rootUriFactory;
      this.userUriFactory = userUriFactory;
    }

    public RootRepresentation MakeRepresentation()
    {
      return new RootRepresentation
      {
        Id = this.rootUriFactory.MakeRootUri(),
        Title = "Alpha Centauri",
        Type = "Root",
        Users = this.userUriFactory.MakeCollectionUri()
      };
    }
  }
}