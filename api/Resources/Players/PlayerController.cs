﻿using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Representations;
using AlphacA.Exceptions;
using AlphacA.Resources.Players.Domain;
using AlphacA.Resources.Players.Representations;
using NJsonSchema.Generation;
using NJsonSchema;

namespace AlphacA.Resources.Players
{
  [ApiController]
  [Route("player")]
  //[Authorize]
  public class PlayerController
  {
    private readonly PlayerResourceHandler resourceHandler;
    private readonly PlayerRepresentationAdapter adapter;
    private readonly JsonSchemaGenerator schemaGenerator;

    public PlayerController(
      PlayerResourceHandler resourceHandler,
      PlayerRepresentationAdapter adapter,
      JsonSchemaGenerator schemaGenerator)
    {
      this.resourceHandler = resourceHandler;
      this.adapter = adapter;
      this.schemaGenerator = schemaGenerator;
    }

    [HttpGet("", Name = PlayerRoutes.Collection)]
    public ActionResult<RepresentationCollection> GetCollection(string search)
    {
      return this.adapter.Collection(resourceHandler.Find(search));
    }

    [HttpGet("schema", Name = PlayerRoutes.Schema)]
    public ActionResult<JsonSchema> GetSchema()
    {
      return this.schemaGenerator.Generate(typeof(PlayerRepresentation));
    }

    [HttpGet("create-form/schema", Name = PlayerRoutes.CreateFormSchema)]
    public ActionResult<JsonSchema> GetCreateFormSchema()
    {
      return this.schemaGenerator.Generate(typeof(PlayerCreateForm));
    }

    [HttpGet("edit-form/schema", Name = PlayerRoutes.EditFormSchema)]
    public ActionResult<JsonSchema> GetEditFormSchema()
    {
      return this.schemaGenerator.Generate(typeof(PlayerEditForm));
    }

    [HttpGet("search", Name = PlayerRoutes.SearchForm)]
    public ActionResult<SearchFormRepresentation> GetSearchForm()
    {
      return this.adapter.SearchForm();
    }

    [HttpGet("search/schema", Name = PlayerRoutes.SearchSchema)]
    public ActionResult<JsonSchema> GetSearchSchema()
    {
      return this.schemaGenerator.Generate(typeof(PlayerSearchForm));
    }

    [HttpGet("create-form", Name = PlayerRoutes.CreateForm)]
    public ActionResult<CreateFormRepresentation> GetCreateForm()
    {
      return this.adapter.CreateForm();
    }

    [HttpGet("{id:Guid}", Name = PlayerRoutes.Player)]
    public ActionResult<PlayerRepresentation> Get(Guid id)
    {
      var Player = resourceHandler.Get(id.ToString());
      return Player != null
        ? this.adapter.Representation(Player)
        : new SimpleErrorResult(404, "Player not found");
    }

    [HttpGet("{id:Guid}/edit-form", Name = PlayerRoutes.EditForm)]
    public ActionResult<EditFormRepresentation> GetEditForm(Guid id)
    {
      var Player = this.resourceHandler.Get(id.ToString());
      if (Player != null)
      {
        return this.adapter.EditForm(Player);
      }

      return new SimpleErrorResult(404, "Player not found");
    }

    [HttpPost("", Name = PlayerRoutes.Create)]
    public ActionResult Create(PlayerCreateForm createForm)
    {
      var Player = this.resourceHandler.Create(this.adapter.Domain(createForm));
      return new CreatedResult(
        this.adapter.GetPlayerUri(Player),
        new { message = "Player created" });
    }

    [HttpPost("search", Name = PlayerRoutes.CreateSearch)]
    public ActionResult CreateSearch([FromBody] PlayerSearchForm searchForm)
    {
      return new CreatedResult(
        this.adapter.GetCollectionUri(searchForm.SearchText),
        new { message = "Search Created" });
    }

    [HttpPost("{id:Guid}", Name = PlayerRoutes.Update)]
    public ActionResult Update(Guid id, [FromBody] PlayerEditForm editForm)
    {
      var Player = this.resourceHandler.Update(id, this.adapter.Domain(editForm));
      if (Player != null)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "Player not found");
    }

    [HttpDelete("{id:Guid}", Name = PlayerRoutes.Delete)]
    public ActionResult Delete(Guid id)
    {
      var found = this.resourceHandler.Delete(id);
      if (found)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "Player not found");
    }
  }
}