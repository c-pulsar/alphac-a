using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Representations;
using AlphacA.Exceptions;

namespace AlphacA.Resources.Users
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
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
    public ActionResult<RepresentationCollection> Get(string search) =>
      resourceHandler.Find(search).CollectionRepresentation(adapter);

    [HttpGet("{id:Guid}", Name = UserRoutes.User)]
    public ActionResult<UserRepresentation> GetUser(Guid id) =>
      resourceHandler.Get(id.ToString()).NotFoundOrResult(this.adapter);

    [HttpPost("", Name = UserRoutes.Create)]
    public ActionResult CreateUser(UserRepresentation representation) =>
      representation
        .Domain(this.adapter)
        .Create(this.resourceHandler)
        .CreatedResult(this.adapter);

    [HttpPost("{id:Guid}", Name = UserRoutes.Update)]
    public ActionResult UpdateUser(Guid id, [FromBody] UserRepresentation representation)
    {
      var user = this.resourceHandler.Update(id, representation.Domain(this.adapter));
      if (user != null)
      {
        return new OkResult();
      }

      return new SimpleErrorResult(404, "User not found");
    }

    [HttpPost("search-form", Name = UserRoutes.CreateSearch)]
    public ActionResult CreateUserSearch([FromBody] UserSearchRepresentation representation)
    {
      return new CreatedResult(
        this.adapter.GetCollectionUri(representation.SearchText),
        new { message = "Search Created" });
    }

    [HttpGet("search-form", Name = UserRoutes.SearchForm)]
    public ActionResult<FormRepresentation> GetUserSearchForm() =>
      this.adapter.SearchForm(new UserSearchRepresentation());

    [HttpGet("{id:Guid}/edit-form", Name = UserRoutes.EditForm)]
    public ActionResult<FormRepresentation> GetUserEditForm(Guid id)
    {
      var user = this.resourceHandler.Get(id.ToString());
      if (user != null)
      {
        return this.adapter.EditForm(user);
      }

      return new SimpleErrorResult(404, "User create-form not found");
    }

    [HttpGet("create-form", Name = UserRoutes.CreateForm)]
    public ActionResult<FormRepresentation> GetUserCreateForm() =>
      new UserRepresentation().CreateForm(adapter);
  }
}
