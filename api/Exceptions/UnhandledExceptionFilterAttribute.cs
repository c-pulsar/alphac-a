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
          context.Result = new SimpleErrorResult(409, context.Exception.Message);
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