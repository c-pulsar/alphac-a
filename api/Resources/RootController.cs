using Microsoft.AspNetCore.Mvc;
using AlphacA.Resources.Users;

namespace AlphacA.Resources
{
  [ApiController]
  [Route("")]
  public class RootController : ControllerBase
  {
    private readonly RootUriFactory rootUriFactory;
    private readonly UserUriFactory userUriFactory;

    public RootController(RootUriFactory rootUriFactory, UserUriFactory userUriFactory)
    {
      this.rootUriFactory = rootUriFactory;
      this.userUriFactory = userUriFactory;
    }

    [HttpGet("", Name = RootRoutes.GetRoot)]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
    public ActionResult<RootRepresentation> GetRoot()
    {
      return new RootRepresentation
      {
        Uri = rootUriFactory.MakeRootUri(),
        ImageUri = rootUriFactory.MakeRootUri(),
        Title = "This is the root representation",
        UsersUri = userUriFactory.MakeCollectionUri(),
        AmountText = "Amount",
        MountText = "Mount"
      };
    }
  }
}