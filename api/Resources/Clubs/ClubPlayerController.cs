using System;
using AlphacA.Exceptions;
using AlphacA.Representations;
using AlphacA.Resources.Clubs.Domain;
using AlphacA.Resources.Clubs.Representations;
using AlphacA.Resources.Players.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Resources.Clubs
{
  [ApiController]
  [Route("club")]
  public class ClubPlayerController
  {
    private readonly ClubPlayerRepresentationAdapter adapter;
    private readonly ClubResourceHandler clubResourceHandler;
    private readonly PlayerResourceHandler playerResourceHandler;

    public ClubPlayerController(
      ClubPlayerRepresentationAdapter adapter,
      ClubResourceHandler clubResourceHandler,
      PlayerResourceHandler playerResourceHandler)
    {
      this.adapter = adapter;
      this.clubResourceHandler = clubResourceHandler;
      this.playerResourceHandler = playerResourceHandler;
    }

    [HttpGet("{id:Guid}/player", Name = ClubPlayerRoutes.Collection)]
    public ActionResult<RepresentationCollection> GetCollection(Guid id)
    {
      var club = this.clubResourceHandler.Get(id.ToString());
      if (club == null)
      {
        return new SimpleErrorResult(404, "Club not found");
      }

      return this.adapter.Collection(
        club, this.playerResourceHandler.GetClubPlayers(club.Id));
    }

    [HttpGet("{id:Guid}/player/create-form", Name = ClubPlayerRoutes.CreateForm)]
    public ActionResult<CreateFormRepresentation> GetPlayerCreateForm(Guid id)
    {
      var club = this.clubResourceHandler.Get(id.ToString());
      if (club == null)
      {
        return new SimpleErrorResult(404, "Club not found");
      }

      return this.adapter.CreateForm(club);
    }
  }
}