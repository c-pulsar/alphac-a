using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using AlphacA.Representations;

namespace AlphacA.Resources.Players.Representations
{
  public class PlayerRepresentation : Representation
  {
    [EmailAddress]
    [Display(Name = "Playername")]
    [Description("Must be an email address")]
    public string PlayerName { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Middle Names")]
    public string MiddleNames { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Player Since")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Last Updated")]
    public DateTime UpdatedAt { get; set; }

    [Display(Name = "Profile Image URL")]
    public Uri ProfileImageUrl { get; set; }
  }
}