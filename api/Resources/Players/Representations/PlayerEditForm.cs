using System;
using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Players.Representations
{
  public class PlayerEditForm
  {
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Middle Names")]
    public string MiddleNames { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Profile Image URL")]
    public Uri ProfileImageUrl { get; set; }
  }
}