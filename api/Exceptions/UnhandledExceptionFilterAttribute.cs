using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlphacA.Exceptions
{
  public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      CheckConflict(context);
      CheckUnhandled(context);
    }

    private static void CheckConflict(ExceptionContext context)
    {
      if (!context.ExceptionHandled)
      {
        if (context.Exception is ResourceConflictException)
        {
          context.HttpContext.Response.StatusCode = 409;
          context.Result = new JsonResult(new { error = context.Exception.Message });
          context.ExceptionHandled = true;
        }
      }
    }

    private static void CheckUnhandled(ExceptionContext context)
    {
      if (!context.ExceptionHandled)
      {
        throw context.Exception;
      }
    }
  }
}