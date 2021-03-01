using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AlphacA.Auth
{
  public class AuthHandler
  {
    private readonly RequestDelegate next;

    public AuthHandler(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      // Call the next delegate/middleware in the pipeline
      await this.next(context).ConfigureAwait(false);

      if (context.Response.StatusCode == 401)
      {
         if (context.Request.Headers.TryGetValue("accept", out StringValues values))
         {
           if (values.Any(x => x.Contains("html")))
           {
             context.Response.Redirect("http://www.google.com/?q=bla");
           }
         }
      }
    }
  }
}