using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace AlphacA.Representations.Formatters
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
      if (typeof(FormRepresentation).IsAssignableFrom(type))
      {
        return base.CanWriteType(type);
      }

      return false;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
      var response = context.HttpContext.Response;
      var formRepresentation = context.Object as FormRepresentation;

      var result = BuildFormHtmlFromTemplate(formRepresentation);

      await response.WriteAsync(result).ConfigureAwait(false);
    }

    private static string BuildFormHtmlFromTemplate(FormRepresentation representation)
    {
      // This is an embedded resource included in the .csproj file
      const string template = "AlphacA.Representations.Templates.FormTemplate.html";

      var assembly = Assembly.GetExecutingAssembly();
      using var stream = assembly.GetManifestResourceStream(template);
      using var reader = new StreamReader(stream);

      var html = reader.ReadToEnd();

      return html
        .Replace("//{{TITLE}}", representation.Title)
        .Replace("//{{POST_URI}}", representation.Destination.ToString())
        .Replace("//{{SCHEMA}}", $"schema: {representation.Schema}")
        .Replace("//{{FORM}}", $"form: {representation.Form}");
    }
  }
}