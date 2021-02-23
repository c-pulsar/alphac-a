using System;

namespace AlphacA.Representations
{
  public class CreateFormRepresentation : FormRepresentation
  {
    public Uri CollectionId { get; set; }
    public override string TemplateId => "AlphacA.Representations.Templates.CreateFormTemplate.html";

    public override string ApplyTemplate(string template)
    {
      return template
        .Replace("//{{TITLE}}", this.Title)
        .Replace("//{{CREATE_URI}}", this.CollectionId.ToString())
        .Replace("//{{SCHEMA}}", $"schema: {this.Schema}");
    }
  }
}