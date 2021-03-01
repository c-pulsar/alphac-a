using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Auth
{
  [ApiController]
  [Route("Account")]
  public class AccountController : Controller
  {
    [HttpGet("Login")]
    public async Task Login(string returnUrl = "/")
    {
      await this.HttpContext
        .ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl })
        .ConfigureAwait(false);
    }

    [Authorize]
    [HttpGet("Logout")]
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
          RedirectUri = "http://localhost:3010"
          //RedirectUri = Url.Action("GetRoot", "Root")
        })
        .ConfigureAwait(false);

      await this.HttpContext
        .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme)
        .ConfigureAwait(false);
    }
  }
}