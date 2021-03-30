using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Resources.Root;
using AlphacA.Resources.Players.Domain;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Resources.Clubs;

namespace AlphacA.Resources.Players.Representations
{
  public class PlayerRepresentationAdapter
  {
    private readonly PlayerUriFactory playerUriFactory;
    private readonly ClubUriFactory clubUriFactory;
    private readonly RootUriFactory rootUri;

    public PlayerRepresentationAdapter(
      PlayerUriFactory playerUriFactory,
      ClubUriFactory clubUriFactory,
      RootUriFactory rootUri)
    {
      this.playerUriFactory = playerUriFactory;
      this.clubUriFactory = clubUriFactory;
      this.rootUri = rootUri;
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

    public RepresentationCollection Collection(IEnumerable<IResourceHeader> players)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.playerUriFactory.MakeCollection(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Search, this.playerUriFactory.MakeSearchForm(), "Search"),
        },

        Title = "Players",
        Resource = "Player",
        Items = players.Select(x => new RepresentationCollectionItem
        {
          Reference = playerUriFactory.Make(x.Id),
          Title = x.Title,
          Image = string.IsNullOrWhiteSpace(x.Image) ? null : new Uri(x.Image)
        }).ToArray(),
      };
    }

    public PlayerRepresentation Representation(Player player)
    {
      var links = new List<Link>()
      {
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Self, this.playerUriFactory.Make(player.Id), "Self"),
          Link.Make(LinkRelations.Manifest, this.playerUriFactory.MakeSchema(), "Schema"),
          Link.Make(LinkRelations.EditForm, this.playerUriFactory.MakeEditForm(player.Id), "Edit"),
          Link.Make(LinkRelations.Collection, this.playerUriFactory.MakeCollection(), "Players"),
          Link.Make("club", this.clubUriFactory.Make(player.ClubId), "Club")
      };

      if (player.ProfileImageUrl != null)
      {
        links.Add(Link.Make(LinkRelations.Image, player.ProfileImageUrl, "Image"));
      }

      return new PlayerRepresentation
      {
        Links = links.ToArray(),
        Title = player.Title,
        Resource = "Player",
        PlayerName = player.Email,
        FirstName = player.FirstName,
        MiddleNames = player.MiddleNames,
        LastName = player.LastName,
        CreatedAt = player.CreatedAt,
        UpdatedAt = player.UpdatedAt,
        ProfileImageUrl = player.ProfileImageUrl
      };
    }

    public EditFormRepresentation EditForm(Player player)
    {
      return new EditFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.playerUriFactory.MakeEditForm(player.Id), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Collection, this.playerUriFactory.MakeCollection(), "Players"),
          Link.Make(LinkRelations.Manifest, this.playerUriFactory.MakeEditFormSchema(), "Schema"),
          Link.Make(LinkRelations.About, this.playerUriFactory.Make(player.Id), "Player")
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
          Link.Make(LinkRelations.Self, this.playerUriFactory.MakeSearchForm(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Manifest, this.playerUriFactory.MakeSearchSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.playerUriFactory.MakeCollection(), "Players"),
        },

        Resource = "Player",
        Title = "Search Players"
      };
    }

    public CreatedResult CreatedSearchResult(string search)
    {
      return new CreatedResult(
        this.playerUriFactory.MakeCollection(search),
        new { message = "Search Created" });
    }
  }
}