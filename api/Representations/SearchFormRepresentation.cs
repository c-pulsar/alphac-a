namespace AlphacA.Representations
{
  public class SearchFormRepresentation : FormRepresentation
  {
    public override string TemplateId => "AlphacA.Representations.Templates.CreateFormTemplate.html";

    public override string ApplyTemplate(string template)
    {
      return template
        .Replace("//{{TITLE}}", this.Title)
        //.Replace("//{{CREATE_URI}}", this.Id.ToString())
        .Replace("//{{SCHEMA}}", $"schema: {this.Schema}");
    }
  }
}