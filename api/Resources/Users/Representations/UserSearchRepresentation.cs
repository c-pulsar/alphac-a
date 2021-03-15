using System.ComponentModel.DataAnnotations;
using AlphacA.Representations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserSearchRepresentation : Representation
  {
    [Display(Name = "Search For")]
    [Required]
    public string SearchText { get; set; }
  }
}