using System;
using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Representations;

namespace Pulsar.AlphacA.Users
{
  [ApiController]
  [Route("user")]
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
    public RepresentationCollection Get() =>
      resourceHandler.GetAll().ToUserCollectionRepresentation(adapter);

    [HttpGet("{id:Guid}", Name = UserRoutes.User)]
    public UserRepresentation GetUser(Guid id) =>
      resourceHandler.Get(id).ToRepresentation(adapter);

    [HttpPost("")]
    public ActionResult CreateUser(UserRepresentation representation)
    {

      throw new NotImplementedException();

      //return representation.ToDomain(this.adapter)

      //IServiceProvider services = null;

      //return representation.MakeUser(this.mapper).Create(this.service).CreatedResult(this.uriFactory);

      // var mapper = new UserRepresentationAdapter();
      // var user = mapper.MakeUser(representation);
      // this.service.CreateUser(user);
      // return mapper.Created(user);


      //var s = this.userService.Create(user)

      // using (var session = this.documentStore.OpenSession())
      // {

      //   var cmpxng = new PutCompareExchangeValueOperation<string>(
      //   "ayende",
      //   "users/2",
      //   0 // meaning empty
      //   );

      //   var result = documentStore.Operations.Send(cmpxng);

      //   session.Store(user, user.Id);
      //   session.SaveChanges();
      // }

      // Console.WriteLine("###################" + user.Id);
      // return this.Created();
    }

    [HttpGet("create-form", Name = UserRoutes.UserCreateForm)]
    public FormRepresentation GetCreateUserForm() => new UserRepresentation().ToCreateForm(adapter);
  }
}
