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
          Link.Make(LinkRelations.Self, this.rootUriFactory.MakeRootUri(), "Self"),
          Link.Make(LinkRelations.Manifest, this.rootUriFactory.MakeSchema(), "Schema"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users")
        },

        Title = "Alpha Centauri",
        Resource = "Home",
        Version = "0.1.0",
      };
    }
  }
}