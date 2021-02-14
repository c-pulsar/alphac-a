using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Schema;
using Pulsar.AlphacA.Representations.Users;

namespace Pulsar.AlphacA.Representations.Formatters
{
  public class HtmlFormOutputFormatter : TextOutputFormatter
  {
    // private const string HtmlFormTemplate = @"
    //     <!DOCTYPE html>
    //     <html>
    //         <body>
    //             <form action='{0}' method='post' >
    //                 {1}
    //                 <input type='submit' value='Submit'>
    //             </form> 
    //         </body>
    //     </html>";

    public HtmlFormOutputFormatter()
    {
      SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));
      SupportedEncodings.Add(Encoding.UTF8);
      SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type type)
    {
      // if (typeof(UserRepresentation).IsAssignableFrom(type))
      // {
      //     return base.CanWriteType(type);
      // }

      return false;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
      //var j = JsonSchema.Parse()
      var response = context.HttpContext.Response;
      // var representation = context.Object as ResourceMetadataRepresentation;
      // var result = this.BuildForm(representation);

      await response.WriteAsync("change this").ConfigureAwait(false);
    }

    // private string BuildForm(ResourceMetadataRepresentation representation)
    // {
    //     return string.Format(
    //         HtmlFormTemplate,
    //         representation.CreateResourceUri,
    //         string.Join(' ', representation.Attributes.Select(x => x.ToHtml()).ToArray()));
    // }
  }
}