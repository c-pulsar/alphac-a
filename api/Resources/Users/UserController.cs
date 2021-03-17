using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Representations;
using AlphacA.Exceptions;
using AlphacA.Resources.Users.Domain;
using AlphacA.Resources.Users.Representations;
using NJsonSchema.Generation;
using NJsonSchema;

namespace AlphacA.Resources.Users
{
  [ApiController]
  [Route("user")]
  //[Authorize]
  public class UserController
  {
    private readonly UserResourceHandler resourceHandler;
    private readonly UserRepresentationAdapter adapter;
    private readonly JsonSchemaGenerator schemaGenerator;

    public UserController(
      UserResourceHandler resourceHandler,
      UserRepresentationAdapter adapter,
      JsonSchemaGenerator schemaGenerator)
    {
      this.resourceHandler = resourceHandler;
      this.adapter = adapter;
      this.schemaGenerator = schemaGenerator;
    }

    [HttpGet("", Name = UserRoutes.Collection)]
    public ActionResult<RepresentationCollection> GetCollection(string search)
    {
      return this.adapter.Collection(resourceHandler.Find(search));
    }

    [HttpGet("schema", Name = UserRoutes.Schema)]
    public ActionResult<JsonSchema> GetSchema()
    {
      return this.schemaGenerator.Generate(typeof(UserRepresentation));
    }

    [HttpGet("create-form/schema", Name = UserRoutes.CreateFormSchema)]
    public ActionResult<JsonSchema> GetCreateFormSchema()
    {
      return this.schemaGenerator.Generate(typeof(UserCreateForm));
    }

    [HttpGet("edit-form/schema", Name = UserRoutes.EditFormSchema)]
    public ActionResult<JsonSchema> GetEditFormSchema()
    {
      return this.schemaGenerator.Generate(typeof(UserEditForm));
    }

    [HttpGet("search", Name = UserRoutes.SearchForm)]
    public ActionResult<Representation> GetSearchForm()
    {
      return this.adapter.SearchForm();
    }

    [HttpGet("search/schema", Name = UserRoutes.SearchSchema)]
    public ActionResult<JsonSchema> GetSearchSchema()
    {
      return this.schemaGenerator.Generate(typeof(UserSearchForm));
    }

    [HttpGet("create-form", Name = UserRoutes.CreateForm)]
    public ActionResult<CreateFormRepresentation> GetCreateForm()
    {
      return this.adapter.CreateForm();
    }

    [HttpGet("{id:Guid}", Name = UserRoutes.User)]
    public ActionResult<UserRepresentation> Get(Guid id)
    {
      var user = resourceHandler.Get(id.ToString());
      return user != null
        ? this.adapter.Representation(user)
        : new SimpleErrorResult(404, "User not found");
    }

    [HttpGet("{id:Guid}/edit-form", Name = UserRoutes.EditForm)]
    public ActionResult<EditFormRepresentation> GetEditForm(Guid id)
    {
      var user = this.resourceHandler.Get(id.ToString());
      if (user != null)
      {
        return this.adapter.EditForm(user);
      }

      return new SimpleErrorResult(404, "User not found");
    }

    [HttpPost("", Name = UserRoutes.Create)]
    public ActionResult Create(UserCreateForm createForm)
    {
      var user = this.resourceHandler.Create(this.adapter.Domain(createForm));
      return new CreatedResult(
        this.adapter.GetUserUri(user),
        new { message = "User created" });
    }

    [HttpPost("search-form", Name = UserRoutes.CreateSearch)]
    public ActionResult CreateSearch([FromBody] UserSearchForm searchForm)
    {
      return new CreatedResult(
        this.adapter.GetCollectionUri(searchForm.SearchText),
        new { message = "Search Created" });
    }

    [HttpPost("{id:Guid}", Name = UserRoutes.Update)]
    public ActionResult Update(Guid id, [FromBody] UserEditForm editForm)
    {
      var user = this.resourceHandler.Update(id, this.adapter.Domain(editForm));
      if (user != null)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "User not found");
    }

    [HttpDelete("{id:Guid}", Name = UserRoutes.Delete)]
    public ActionResult Delete(Guid id)
    {
      var found = this.resourceHandler.Delete(id);
      if (found)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "User not found");
    }
  }
}
