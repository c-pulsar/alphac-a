using System.ComponentModel.DataAnnotations;
using AlphacA.Representations;

namespace AlphacA.Resources.Root
{
  public class RootRepresentation : Representation
  {
    public string Version { get; set; }

    [Display(Name = "About Football World")]
    public string About { get; set; }
  }
}