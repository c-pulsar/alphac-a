using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Resources.Clubs.Domain;
using AlphacA.Resources.Players;
using AlphacA.Resources.Root;

namespace AlphacA.Resources.Clubs.Representations
{
  public class ClubRepresentationAdapter
  {
    private readonly ClubUriFactory clubUriFactory;
    private readonly PlayerUriFactory playerUriFactory;
    private readonly RootUriFactory rootUri;

    public ClubRepresentationAdapter(
      ClubUriFactory clubUriFactory,
      PlayerUriFactory playerUriFactory,
      RootUriFactory rootUri)
    {
      this.clubUriFactory = clubUriFactory;
      this.playerUriFactory = playerUriFactory;
      this.rootUri = rootUri;
    }

    public Club Domain(ClubCreateForm createForm)
    {
      return new Club
      {
        Name = createForm.Name,
        ProfileImageUrl = createForm.ProfileImageUrl
      };
    }

    public Club Domain(ClubEditForm editForm)
    {
      return new Club
      {
        Name = editForm.Name,
        ProfileImageUrl = editForm.ProfileImageUrl
      };
    }

    public RepresentationCollection Collection(IEnumerable<IResourceHeader> clubs)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.clubUriFactory.MakeCollection(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.CreateForm, this.clubUriFactory.MakeCreateForm(), "Create"),
          Link.Make(LinkRelations.Search, this.clubUriFactory.MakeSearchForm(), "Search"),
        },

        Title = "Clubs",
        Resource = "Club",
        Items = clubs.Select(x => new RepresentationCollectionItem
        {
          Reference = clubUriFactory.Make(x.Id),
          Title = x.Title,
          Image = string.IsNullOrWhiteSpace(x.Image) ? null : new Uri(x.Image)
        }).ToArray(),
      };
    }

    public ClubRepresentation Representation(Club club)
    {
      var links = new List<Link>()
      {
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Self, this.clubUriFactory.Make(club.Id), "Self"),
          Link.Make(LinkRelations.Manifest, this.clubUriFactory.MakeSchema(), "Schema"),
          Link.Make(LinkRelations.EditForm, this.clubUriFactory.MakeEditForm(club.Id), "Edit"),
          Link.Make(LinkRelations.Collection, this.clubUriFactory.MakeCollection(), "Clubs"),
          Link.Make("players", this.clubUriFactory.MakePlayersCollection(club.Id), "Players")
      };

      if (club.ProfileImageUrl != null)
      {
        links.Add(Link.Make(LinkRelations.Image, club.ProfileImageUrl, "Image"));
      }

      return new ClubRepresentation
      {
        Links = links.ToArray(),
        Title = club.Title,
        Resource = "Club",
        Name = club.Name,
        CreatedAt = club.CreatedAt,
        UpdatedAt = club.UpdatedAt,
        ProfileImageUrl = club.ProfileImageUrl
      };
    }

    public EditFormRepresentation EditForm(Club club)
    {
      return new EditFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.clubUriFactory.MakeEditForm(club.Id), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Collection, this.clubUriFactory.MakeCollection(), "Clubs"),
          Link.Make(LinkRelations.Manifest, this.clubUriFactory.MakeEditFormSchema(), "Schema"),
          Link.Make(LinkRelations.About, this.clubUriFactory.Make(club.Id), "Club")
        },

        Resource = "Club",
        DeleteEnabled = true,
        Title = "Edit Club"
      };
    }

    public SearchFormRepresentation SearchForm()
    {
      return new SearchFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.clubUriFactory.MakeSearchForm(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Manifest, this.clubUriFactory.MakeSearchSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.clubUriFactory.MakeCollection(), "Clubs"),
        },

        Resource = "Club",
        Title = "Search Clubs"
      };
    }

    public CreateFormRepresentation CreateForm()
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.clubUriFactory.MakeCreateForm(), "Create"),
          Link.Make(LinkRelations.Manifest, this.clubUriFactory.MakeCreateFormSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.clubUriFactory.MakeCollection(), "Clubs"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
        },
        Resource = "Club",
        Title = "Create Club"
      };
    }

    public Uri GetCollectionUri(string search)
    {
      return clubUriFactory.MakeCollection(search);
    }

    public Uri GetClubUri(Club club)
    {
      return clubUriFactory.Make(club.Id);
    }

    public RepresentationCollection PlayerCollection(Club club, IEnumerable<IResourceHeader> players)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.clubUriFactory.MakePlayersCollection(club.Id), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.CreateForm, this.clubUriFactory.MakePlayerCreateForm(club.Id), "Create"),
        },

        Title = $"{club.Name} Players",
        Resource = "Player",
        Items = players.Select(x => new RepresentationCollectionItem
        {
          Reference = clubUriFactory.Make(x.Id),
          Title = x.Title,
          Image = string.IsNullOrWhiteSpace(x.Image) ? null : new Uri(x.Image)
        }).ToArray(),
      };
    }

    public CreateFormRepresentation PlayerCreateForm(Club club)
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.clubUriFactory.MakePlayerCreateForm(club.Id), "Create"),
          Link.Make(LinkRelations.Manifest, this.playerUriFactory.MakeCreateFormSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.clubUriFactory.MakePlayersCollection(club.Id), "Players"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
        },
        Resource = "Player",
        Title = $"Create Player for {club.Name}"
      };
    }
  }
}