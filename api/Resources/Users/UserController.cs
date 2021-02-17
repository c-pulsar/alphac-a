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
    public RepresentationCollection Get() =>
      resourceHandler.GetAll().ToUserCollectionRepresentation(adapter);

    [HttpGet("{id:Guid}", Name = UserRoutes.User)]
    public UserRepresentation GetUser(Guid id) =>
      resourceHandler.Get(id.ToString()).ToRepresentation(adapter);

    [HttpPost("")]
    public ActionResult CreateUser(UserRepresentation representation) =>
      representation
        .ToDomain(this.adapter)
        .Create(this.resourceHandler)
        .ToCreatedResult(this.adapter);

    [HttpGet("create-form", Name = UserRoutes.UserCreateForm)]
    public FormRepresentation GetCreateUserForm() =>
      new UserRepresentation().ToCreateForm(adapter);
  }
}
