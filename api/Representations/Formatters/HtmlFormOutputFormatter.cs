using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Pulsar.AlphacA.Representations.Formatters
{
  public class HtmlFormOutputFormatter : TextOutputFormatter
  {
    public HtmlFormOutputFormatter()
    {
      SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));
      SupportedEncodings.Add(Encoding.UTF8);
      SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type type)
    {
      // if (typeof(CreateFormRepresentation).IsAssignableFrom(type))
      // {
      //   return base.CanWriteType(type);
      // }

      return false;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
      var response = context.HttpContext.Response;
      var representation = context.Object as CreateFormRepresentation;

      var result = BuildCreateFormHtmlFromTemplate(representation);

      await response.WriteAsync(result).ConfigureAwait(false);
    }

    private static string BuildCreateFormHtmlFromTemplate(CreateFormRepresentation representation)
    {
      using var reader = new StreamReader(Path.Combine("static", "forms/create.html"));
      //return reader.ReadToEnd();
      var html = reader.ReadToEnd();
      return html.Replace("//{{SCHEMA}}", $"schema: {representation.Schema},");
    }
  }
}