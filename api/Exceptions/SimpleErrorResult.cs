using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Exceptions
{
  public class SimpleErrorResult : JsonResult
  {
    public SimpleErrorResult(int status, string message) : base(new { status, message })
    {
      this.StatusCode = status;
    }
  }
}