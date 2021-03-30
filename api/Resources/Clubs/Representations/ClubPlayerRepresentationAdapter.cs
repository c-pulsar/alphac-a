using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Resources.Clubs.Domain;
using AlphacA.Resources.Players;
using AlphacA.Resources.Players.Domain;
using AlphacA.Resources.Players.Representations;
using AlphacA.Resources.Root;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Resources.Clubs.Representations
{
  public class ClubPlayerRepresentationAdapter
  {
    private readonly RootUriFactory rootUriFactory;
    private readonly PlayerUriFactory playerUriFactory;
    private readonly ClubPlayerUriFactory uriFactory;

    public ClubPlayerRepresentationAdapter(
      RootUriFactory rootUriFactory,
      PlayerUriFactory playerUriFactory,
      ClubPlayerUriFactory uriFactory)
    {
      this.rootUriFactory = rootUriFactory;
      this.playerUriFactory = playerUriFactory;
      this.uriFactory = uriFactory;
    }

    public Player Domain(string clubId, PlayerCreateForm createForm)
    {
      return new Player
      {
        ClubId = clubId,
        Email = createForm.EmailAddress,
        FirstName = createForm.FirstName,
        MiddleNames = createForm.MiddleNames,
        LastName = createForm.LastName,
        ProfileImageUrl = createForm.ProfileImageUrl
      };
    }

    public RepresentationCollection Collection(Club club, IEnumerable<IResourceHeader> players)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.uriFactory.MakeCollection(club.Id), "Self"),
          Link.Make(LinkRelations.Start, this.rootUriFactory.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.CreateForm, this.uriFactory.MakeCreateForm(club.Id), "Create"),
        },

        Title = $"{club.Name}: Players",
        Resource = "Player",
        Items = players.Select(x => new RepresentationCollectionItem
        {
          Reference = this.playerUriFactory.Make(x.Id),
          Title = x.Title,
          Image = string.IsNullOrWhiteSpace(x.Image) ? null : new Uri(x.Image)
        }).ToArray(),
      };
    }

    public CreateFormRepresentation CreateForm(Club club)
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.uriFactory.MakeCreateForm(club.Id), "Create"),
          Link.Make(LinkRelations.Manifest, this.playerUriFactory.MakeCreateFormSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.uriFactory.MakeCollection(club.Id), "Players"),
          Link.Make(LinkRelations.Start, this.rootUriFactory.MakeRootUri(), "Home"),
        },
        Resource = "Player",
        Title = $"Create Player for {club.Name}"
      };
    }

    public CreatedResult CreatedResult(Player player)
    {
      return new CreatedResult(
         this.playerUriFactory.Make(player.Id),
         new { message = "Player created" });
    }
  }
}