using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserCreateForm
  {
    [EmailAddress]
    [Required]
    [Display(Name = "Username")]
    [Description("Must be an email address")]
    public string UserName { get; set; }

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