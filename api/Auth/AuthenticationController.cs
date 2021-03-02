using System.Threading.Tasks;
using AlphacA.Resources.Root;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Auth
{
  [ApiController]
  [Route("auth")]
  public class AuthenticationController : Controller
  {
    private readonly RootUriFactory rootUriFactory;

    public AuthenticationController(RootUriFactory rootUriFactory)
    {
      this.rootUriFactory = rootUriFactory;
    }

    [HttpGet("login")]
    public async Task Login(string redirectUri = "/")
    {
      await this.HttpContext
        .ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = redirectUri })
        .ConfigureAwait(false);
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task Logout()
    {
      await this.HttpContext
        .SignOutAsync(
        "Auth0",
        new AuthenticationProperties
        {
          // Indicate here where Auth0 should redirect the user after a logout.
          // Note that the resulting absolute Uri must be added to the
          // **Allowed Logout URLs** settings for the app.
          RedirectUri = this.rootUriFactory.MakeRootUri().ToString()
        })
        .ConfigureAwait(false);

      await this.HttpContext
        .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
        .ConfigureAwait(false);
    }
  }
}