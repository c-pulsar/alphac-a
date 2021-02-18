using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Representations;

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
    public ActionResult<RepresentationCollection> Get() =>
      resourceHandler.GetAll().ToUserCollectionRepresentation(adapter);

    [HttpGet("{id:Guid}", Name = UserRoutes.User)]
    public ActionResult<UserRepresentation> GetUser(Guid id) =>
      resourceHandler.Get(id.ToString()).ToNotFoundOrReprentation(adapter);

    [HttpPost("")]
    public ActionResult CreateUser(UserRepresentation representation) =>
      representation
        .ToDomain(this.adapter)
        .Create(this.resourceHandler)
        .ToCreatedResult(this.adapter);

    [HttpGet("create-form", Name = UserRoutes.UserCreateForm)]
    public ActionResult<FormRepresentation> GetCreateUserForm() =>
      new UserRepresentation().ToCreateForm(adapter);
  }
}
