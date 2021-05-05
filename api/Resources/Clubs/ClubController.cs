using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Representations;
using AlphacA.Exceptions;
using NJsonSchema.Generation;
using NJsonSchema;
using AlphacA.Resources.Clubs.Domain;
using AlphacA.Resources.Clubs.Representations;

namespace AlphacA.Resources.Clubs
{
  [ApiController]
  [Route("club")]
  public class ClubController
  {
    private readonly ClubResourceHandler resourceHandler;
    private readonly ClubRepresentationAdapter adapter;
    private readonly JsonSchemaGenerator schemaGenerator;

    public ClubController(
      ClubResourceHandler resourceHandler,
      ClubRepresentationAdapter adapter,
      JsonSchemaGenerator schemaGenerator)
    {
      this.resourceHandler = resourceHandler;
      this.adapter = adapter;
      this.schemaGenerator = schemaGenerator;
    }

    [HttpGet("", Name = ClubRoutes.Collection)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<RepresentationCollection> GetCollection(string search)
    {
      return this.adapter.Collection(resourceHandler.Find(search));
    }

    [HttpGet("schema", Name = ClubRoutes.Schema)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<JsonSchema> GetSchema()
    {
      return this.schemaGenerator.Generate(typeof(ClubRepresentation));
    }

    [HttpGet("create-form/schema", Name = ClubRoutes.CreateFormSchema)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<JsonSchema> GetCreateFormSchema()
    {
      return this.schemaGenerator.Generate(typeof(ClubCreateForm));
    }

    [HttpGet("edit-form/schema", Name = ClubRoutes.EditFormSchema)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<JsonSchema> GetEditFormSchema()
    {
      return this.schemaGenerator.Generate(typeof(ClubEditForm));
    }

    [HttpGet("search", Name = ClubRoutes.SearchForm)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<SearchFormRepresentation> GetSearchForm()
    {
      return this.adapter.SearchForm();
    }

    [HttpGet("search/schema", Name = ClubRoutes.SearchSchema)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<JsonSchema> GetSearchSchema()
    {
      return this.schemaGenerator.Generate(typeof(ClubSearchForm));
    }

    [HttpGet("create-form", Name = ClubRoutes.CreateForm)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<CreateFormRepresentation> GetCreateForm()
    {
      return this.adapter.CreateForm();
    }

    [HttpGet("{id:Guid}", Name = ClubRoutes.Club)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<ClubRepresentation> Get(Guid id)
    {
      var club = this.resourceHandler.Get(id.ToString());
      return club != null
        ? this.adapter.Representation(club)
        : new SimpleErrorResult(404, "Club not found");
    }

    [HttpGet("{id:Guid}/edit-form", Name = ClubRoutes.EditForm)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
    public ActionResult<EditFormRepresentation> GetEditForm(Guid id)
    {
      var club = this.resourceHandler.Get(id.ToString());
      if (club != null)
      {
        return this.adapter.EditForm(club);
      }

      return new SimpleErrorResult(404, "Club not found");
    }

    [HttpPost("", Name = ClubRoutes.Create)]
    public ActionResult Create(ClubCreateForm createForm)
    {
      var club = this.resourceHandler.Create(this.adapter.Domain(createForm));
      return new CreatedResult(
        this.adapter.GetClubUri(club),
        new { message = "Club created" });
    }

    [HttpPost("search", Name = ClubRoutes.CreateSearch)]
    public ActionResult CreateSearch([FromBody] ClubSearchForm searchForm)
    {
      return new CreatedResult(
        this.adapter.GetCollectionUri(searchForm.SearchText),
        new { message = "Search Created" });
    }

    [HttpPost("{id:Guid}", Name = ClubRoutes.Update)]
    public ActionResult Update(Guid id, [FromBody] ClubEditForm editForm)
    {
      var club = this.resourceHandler.Update(id, this.adapter.Domain(editForm));
      if (club != null)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "Club not found");
    }

    [HttpDelete("{id:Guid}", Name = ClubRoutes.Delete)]
    public ActionResult Delete(Guid id)
    {
      var found = this.resourceHandler.Delete(id);
      if (found)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "Club not found");
    }
  }
}
