using System;
using AlphacA.Representations;
using AlphacA.Resources.Clubs;
using AlphacA.Resources.Players;

namespace AlphacA.Resources.Root
{
  public class RootRepresentationAdapter
  {
    private readonly RootUriFactory rootUriFactory;
    private readonly PlayerUriFactory playerUriFactory;
    private readonly ClubUriFactory clubUriFactory;

    public RootRepresentationAdapter(
      RootUriFactory rootUriFactory,
      PlayerUriFactory playerUriFactory,
      ClubUriFactory clubUriFactory)
    {
      this.rootUriFactory = rootUriFactory;
      this.playerUriFactory = playerUriFactory;
      this.clubUriFactory = clubUriFactory;
    }

    public RootRepresentation MakeRepresentation()
    {
      return new RootRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.rootUriFactory.MakeRootUri(), "Self"),
          Link.Make(LinkRelations.Manifest, this.rootUriFactory.MakeSchema(), "Schema"),
          Link.Make("players", this.playerUriFactory.MakeCollection(), "Players"),
          Link.Make("clubs", this.clubUriFactory.MakeCollection(), "Clubs"),
          Link.Make(
            LinkRelations.Image,
            new Uri("https://pbs.twimg.com/profile_images/787000536759431168/j5soSgvM_400x400.jpg"),
            "Image")
        },

        Title = "Football Universe",
        Resource = "Home",
        Version = "0.1.0",
      };
    }
  }
}