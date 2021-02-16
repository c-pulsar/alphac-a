using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pulsar.AlphacA.Representations;
using Pulsar.AlphacA.Representations.Schemas;
using Pulsar.AlphacA.Representations.Users;
using Raven.Client.Documents;

namespace Pulsar.AlphacA.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {
    private readonly ILogger<UserController> _logger;
    private readonly UserUriFactory userUriFactory;
    private readonly IDocumentStore documentStore;

    public UserController(
      IDocumentStore documentStore,
      ILogger<UserController> logger,
      UserUriFactory userUriFactory)
    {
      _logger = logger;
      this.userUriFactory = userUriFactory;
      this.documentStore = documentStore;
    }

    [HttpGet("", Name = UserRoutes.GetUsers)]
    public RepresentationCollection Get()
    {
      return new RepresentationCollection
      {
        Uri = this.userUriFactory.MakeUserCollectionUri(),
        Title = "Users",
        Items = System.Array.Empty<string>(),
        CreateFormUri = this.userUriFactory.MakeGetUserCreateFormUri()
      };
    }

    [HttpGet("{id:int}")]
    public UserRepresentation GetUser(int id)
    {
      var user = new UserRepresentation
      {
        Uri = this.userUriFactory.MakeUserCollectionUri(),
        Title = "Users",
        Email = "christiano@gmail.com"
        //Items = System.Array.Empty<string>(),
        //CreateFormUri = "http://localhost:3010/users/create-form"
        //Items = new string[] { "http://localhost:3010/users/1", "http://localhost:3010/users/2" }
      };

      return user;
    }

    [HttpPost("", Name = UserRoutes.CreateUser)]
    public ActionResult CreateUser(UserRepresentation representation)
    {
      var mapper = new UserRepresentationMapper();
      var user = mapper.MakeNewUser(representation);

      using(var session = this.documentStore.OpenSession())
      {
        session.Store(user);
        session.SaveChanges();
      }
      
      Console.WriteLine("###################" + user.Id);
      return this.Ok();
    }

    [HttpGet("create-form", Name = UserRoutes.GetCreateUserForm)]
    public FormRepresentation GetCreateUserForm()
    {
      return new FormRepresentation
      {
        Uri = this.userUriFactory.MakeGetUserCreateFormUri(),
        DestinationUri = this.userUriFactory.MakeCreateUserUri(),
        Title = "Create User",
        Schema = JsonSchema.Generate(new UserRepresentation()),
        Form = JsonForm.Generate()
      };
    }
  }
}
