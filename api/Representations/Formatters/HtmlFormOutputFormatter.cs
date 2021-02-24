using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AlphacA.Representations.Schemas;
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
      if (typeof(Representation).IsAssignableFrom(type))
      {
        return base.CanWriteType(type);
      }

      return false;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
      var response = context.HttpContext.Response;

      var representation = context.Object as Representation;

      var result = BuildHtmlFromTemplate(representation);

      await response.WriteAsync(result).ConfigureAwait(false);
    }

    private static string BuildHtmlFromTemplate(Representation representation)
    {
      if (representation is FormRepresentation)
      {
        return BuildFormHtmlFromTemplate(representation as FormRepresentation);
      }

      return BuildResourceHtmlFromTemplate(representation);
    }

    private static string BuildResourceHtmlFromTemplate(Representation representation)
    {
      return representation.Html();
      // const string templateId = "AlphacA.Representations.Templates.ResourceViewTemplate.html";

      // var assembly = Assembly.GetExecutingAssembly();
      // using var stream = assembly.GetManifestResourceStream(templateId);
      // using var reader = new StreamReader(stream);

      // var template = reader.ReadToEnd();

      // string resourceHtml;
      // if (representation is RepresentationCollection)
      // {
      //   resourceHtml = HtmlResourceViewGenerator.CollectionHtml(representation as RepresentationCollection).ToString();
      // }
      // else
      // {
      //   resourceHtml = HtmlResourceViewGenerator.RepresentationHtml(representation).ToString();
      // }

      // return template
      //   .Replace("//{{TITLE}}", representation.Title)
      //   .Replace("//{{RESOURCE}}", resourceHtml);
    }

    private static string BuildFormHtmlFromTemplate(FormRepresentation representation)
    {
      var assembly = Assembly.GetExecutingAssembly();
      using var stream = assembly.GetManifestResourceStream(representation.TemplateId);
      using var reader = new StreamReader(stream);

      var template = reader.ReadToEnd();
      return representation.ApplyTemplate(template);
    }
  }
}