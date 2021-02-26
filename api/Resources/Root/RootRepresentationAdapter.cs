using AlphacA.Representations;
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
        Links = new Link[]
        {
          Link.Make("self", this.rootUriFactory.MakeRootUri(), "Self"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users")
        },

        Title = "Alpha Centauri",
        Type = "Root",
        Version = "0.1.0",
      };
    }
  }
}