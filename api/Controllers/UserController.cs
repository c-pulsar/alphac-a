using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pulsar.AlphacA.Users;

namespace Pulsar.AlphacA.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
      return new User[]
      {
        new User
        {
          UserName = "roger.waters",
          FirstName = "Roger",
          LastName = "Waters"
        }
      };
    }
  }
}
