using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Representations;
using AlphacA.Exceptions;
using AlphacA.Resources.Users.Domain;
using AlphacA.Resources.Users.Representations;
using Microsoft.AspNetCore.Authorization;

namespace AlphacA.Resources.Users
{
  [ApiController]
  [Route("user")]
  [Authorize]
  public class UserController 
  {
    private readonly UserResourceHandler resourceHandler;
    private readonly UserRepresentationAdapter adapter;

    public UserController(
      UserResourceHandler resourceHandler,
      UserRepresentationAdapter adapter)
    {
      this.resourceHandler = resourceHandler;
      this.adapter = adapter;
    }

    [HttpGet("", Name = UserRoutes.UserCollection)]
    public ActionResult<RepresentationCollection> GetCollection(string search)
    {
      return this.adapter.Representation(resourceHandler.Find(search));
    }

    [HttpGet("search-form", Name = UserRoutes.SearchForm)]
    public ActionResult<FormRepresentation> GetSearchForm()
    {
      return this.adapter.SearchForm(new UserSearchRepresentation());
    }

    [HttpGet("create-form", Name = UserRoutes.CreateForm)]
    public ActionResult<FormRepresentation> GetCreateForm()
    {
      return this.adapter.CreateForm(new UserRepresentation());
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
    public ActionResult<FormRepresentation> GetEditForm(Guid id)
    {
      var user = this.resourceHandler.Get(id.ToString());
      if (user != null)
      {
        return this.adapter.EditForm(user);
      }

      return new SimpleErrorResult(404, "User not found");
    }

    [HttpPost("", Name = UserRoutes.Create)]
    public ActionResult Create(UserRepresentation representation)
    {
      var user = this.resourceHandler.Create(this.adapter.Domain(representation));
      return new CreatedResult(
        this.adapter.GetUserUri(user),
        new { message = "User created" });
    }

    [HttpPost("search-form", Name = UserRoutes.CreateSearch)]
    public ActionResult CreateSearch([FromBody] UserSearchRepresentation representation)
    {
      return new CreatedResult(
        this.adapter.GetCollectionUri(representation.SearchText),
        new { message = "Search Created" });
    }

    [HttpPost("{id:Guid}", Name = UserRoutes.Update)]
    public ActionResult Update(Guid id, [FromBody] UserRepresentation representation)
    {
      var user = this.resourceHandler.Update(id, this.adapter.Domain(representation));
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
