using System;
using AlphacA.Exceptions;
using AlphacA.Representations;
using AlphacA.Resources.Clubs.Domain;
using AlphacA.Resources.Clubs.Representations;
using AlphacA.Resources.Players.Domain;
using AlphacA.Resources.Players.Representations;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Resources.Clubs
{
  [ApiController]
  [Route("club/{id:Guid}/player")]
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

    [HttpGet("", Name = ClubPlayerRoutes.Collection)]
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

    [HttpGet("create-form", Name = ClubPlayerRoutes.CreateForm)]
    public ActionResult<CreateFormRepresentation> GetCreateForm(Guid id)
    {
      var club = this.clubResourceHandler.Get(id.ToString());
      if (club == null)
      {
        return new SimpleErrorResult(404, "Club not found");
      }

      return this.adapter.CreateForm(club);
    }

    [HttpPost("", Name = ClubPlayerRoutes.Create)]
    public ActionResult Create(Guid id, PlayerCreateForm createForm)
    {
      var player = this.playerResourceHandler.Create(
        this.adapter.Domain(id.ToString(), createForm));

      return this.adapter.CreatedResult(player);
    }
  }
}