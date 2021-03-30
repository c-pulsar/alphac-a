using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Players.Representations
{
  public class PlayerCreateForm
  {
    [EmailAddress]
    [Required]
    [Display(Name = "Player email address")]
    [Description("Must be an email address")]
    public string EmailAddress { get; set; }

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