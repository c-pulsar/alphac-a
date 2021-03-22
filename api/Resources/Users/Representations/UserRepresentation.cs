using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using AlphacA.Representations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserRepresentation : Representation
  {
    [EmailAddress]
    [Display(Name = "Username")]
    [Description("Must be an email address")]
    public string UserName { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Middle Names")]
    public string MiddleNames { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "User Since")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Last Updated")]
    public DateTime UpdatedAt { get; set; }

    [Display(Name = "Profile Image URL")]
    public Uri ProfileImageUrl { get; set; }
  }
}