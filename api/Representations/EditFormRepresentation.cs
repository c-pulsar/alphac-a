using System;

namespace AlphacA.Representations
{
  public class EditFormRepresentation : FormRepresentation
  {
    public Uri CollectionId { get; set; }
    public Uri ResourceId { get; set; }
    public bool CanDelete { get; set; }
    public override string TemplateId => "AlphacA.Representations.Templates.EditFormTemplate.html";
    public override string ApplyTemplate(string template)
    {
      return template
        .Replace("//{{TITLE}}", this.Title)
        .Replace("//{{COLLECTION_URI}}", this.CollectionId.ToString())
        .Replace("//{{RESOURCE_URI}}", this.ResourceId.ToString())
        .Replace("//{{DELETE_VISIBLE}}", this.CanDelete ? "visible" : "invisible")
        .Replace("//{{SCHEMA}}", $"schema: {this.Schema}");
    }
  }
}