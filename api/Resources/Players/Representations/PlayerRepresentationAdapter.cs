using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Resources.Root;
using AlphacA.Resources.Players.Domain;

namespace AlphacA.Resources.Players.Representations
{
  public class PlayerRepresentationAdapter
  {
    private readonly PlayerUriFactory PlayerUriFactory;
    private readonly RootUriFactory rootUri;

    public PlayerRepresentationAdapter(
      PlayerUriFactory PlayerUriFactory,
      RootUriFactory rootUri)
    {
      this.PlayerUriFactory = PlayerUriFactory;
      this.rootUri = rootUri;
    }

    public Player Domain(PlayerCreateForm createForm)
    {
      return new Player
      {
        PlayerName = createForm.PlayerName,
        FirstName = createForm.FirstName,
        MiddleNames = createForm.MiddleNames,
        LastName = createForm.LastName,
        ProfileImageUrl = createForm.ProfileImageUrl
      };
    }

    public Player Domain(PlayerEditForm editForm)
    {
      return new Player
      {
        FirstName = editForm.FirstName,
        MiddleNames = editForm.MiddleNames,
        LastName = editForm.LastName,
        ProfileImageUrl = editForm.ProfileImageUrl
      };
    }

    public RepresentationCollection Collection(IEnumerable<IResourceHeader> Players)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.PlayerUriFactory.MakeCollection(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.CreateForm, this.PlayerUriFactory.MakeCreateForm(), "Create"),
          Link.Make(LinkRelations.Search, this.PlayerUriFactory.MakeSearchForm(), "Search"),
        },

        Title = "Players",
        Resource = "Player",
        Items = Players.Select(x => new RepresentationCollectionItem
        {
          Reference = PlayerUriFactory.Make(x.Id),
          Title = x.Title,
          Image = string.IsNullOrWhiteSpace(x.Image) ? null : new Uri(x.Image)
        }).ToArray(),
      };
    }

    public PlayerRepresentation Representation(Player Player)
    {
      var links = new List<Link>()
      {
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Self, this.PlayerUriFactory.Make(Player.Id), "Self"),
          Link.Make(LinkRelations.Manifest, this.PlayerUriFactory.MakeSchema(), "Schema"),
          Link.Make(LinkRelations.EditForm, this.PlayerUriFactory.MakeEditForm(Player.Id), "Edit"),
          Link.Make(LinkRelations.Collection, this.PlayerUriFactory.MakeCollection(), "Players")
      };

      if (Player.ProfileImageUrl != null)
      {
        links.Add(Link.Make(LinkRelations.Image, Player.ProfileImageUrl, "Image"));
      }

      return new PlayerRepresentation
      {
        Links = links.ToArray(),
        Title = Player.Title,
        Resource = "Player",
        PlayerName = Player.PlayerName,
        FirstName = Player.FirstName,
        MiddleNames = Player.MiddleNames,
        LastName = Player.LastName,
        CreatedAt = Player.CreatedAt,
        UpdatedAt = Player.UpdatedAt,
        ProfileImageUrl = Player.ProfileImageUrl
      };
    }

    public EditFormRepresentation EditForm(Player Player)
    {
      return new EditFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.PlayerUriFactory.MakeEditForm(Player.Id), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Collection, this.PlayerUriFactory.MakeCollection(), "Players"),
          Link.Make(LinkRelations.Manifest, this.PlayerUriFactory.MakeEditFormSchema(), "Schema"),
          Link.Make(LinkRelations.About, this.PlayerUriFactory.Make(Player.Id), "Player")
        },

        Resource = "Player",
        DeleteEnabled = true,
        Title = "Edit Player"
      };
    }

    public SearchFormRepresentation SearchForm()
    {
      return new SearchFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.PlayerUriFactory.MakeSearchForm(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Manifest, this.PlayerUriFactory.MakeSearchSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.PlayerUriFactory.MakeCollection(), "Players"),
        },

        Resource = "Player",
        Title = "Search Players"
      };
    }

    public CreateFormRepresentation CreateForm()
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.PlayerUriFactory.MakeCreateForm(), "Create"),
          Link.Make(LinkRelations.Manifest, this.PlayerUriFactory.MakeCreateFormSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.PlayerUriFactory.MakeCollection(), "Players"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
        },
        Resource = "Player",
        Title = "Create Player"
      };
    }

    public Uri GetCollectionUri(string search)
    {
      return PlayerUriFactory.MakeCollection(search);
    }

    public Uri GetPlayerUri(Player Player)
    {
      return PlayerUriFactory.Make(Player.Id);
    }
  }
}