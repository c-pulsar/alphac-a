using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pulsar.AlphacA.Representations;
using Pulsar.AlphacA.Representations.Schemas;
using Pulsar.AlphacA.Representations.Users;
using Pulsar.AlphacA.Users;

namespace Pulsar.AlphacA.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {
    private readonly ILogger<UserController> _logger;
    private readonly UserUriFactory userUriFactory;

    public UserController(ILogger<UserController> logger, UserUriFactory userUriFactory)
    {
      _logger = logger;
      this.userUriFactory = userUriFactory;
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

    [HttpGet("create-form", Name = UserRoutes.GetCreateUserForm)]
    public CreateFormRepresentation GetCreateUserForm()
    {
      var generator = new JsonSchemaGenerator();
      return new CreateFormRepresentation
      {
        Uri = this.userUriFactory.MakeGetUserCreateFormUri(),
        JsonSchema = generator.GenerateJSchemaObject(new UserRepresentation())
      };
    }
  }
}
