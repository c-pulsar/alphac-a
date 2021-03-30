using System;
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

    [HttpGet("{id:Guid}", Name = PlayerRoutes.Player)]
    public ActionResult<PlayerRepresentation> Get(Guid id)
    {
      var player = resourceHandler.Get(id.ToString());
      return player != null
        ? this.adapter.Representation(player)
        : new SimpleErrorResult(404, "Player not found");
    }

    [HttpGet("{id:Guid}/edit-form", Name = PlayerRoutes.EditForm)]
    public ActionResult<EditFormRepresentation> GetEditForm(Guid id)
    {
      var player = this.resourceHandler.Get(id.ToString());
      if (player != null)
      {
        return this.adapter.EditForm(player);
      }

      return new SimpleErrorResult(404, "Player not found");
    }

    [HttpPost("search", Name = PlayerRoutes.CreateSearch)]
    public ActionResult CreateSearch([FromBody] PlayerSearchForm searchForm)
    {
      return this.adapter.CreatedSearchResult(searchForm.SearchText);
    }

    [HttpPost("{id:Guid}", Name = PlayerRoutes.Update)]
    public ActionResult Update(Guid id, [FromBody] PlayerEditForm editForm)
    {
      var player = this.resourceHandler.Update(id, this.adapter.Domain(editForm));
      if (player != null)
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
