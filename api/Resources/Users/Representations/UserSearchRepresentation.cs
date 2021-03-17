using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserSearchRepresentation
  {
    [Display(Name = "Search For")]
    [Required]
    public string SearchText { get; set; }
  }
}