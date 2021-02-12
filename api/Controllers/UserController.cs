using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pulsar.AlphacA.Representations;
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
        Items = new string[] { "http://localhost:3010/users/1", "http://localhost:3010/users/2" }
      };
    }
  }
}
