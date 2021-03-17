using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserEditForm
  {
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Middle Names")]
    public string MiddleNames { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
  }
}