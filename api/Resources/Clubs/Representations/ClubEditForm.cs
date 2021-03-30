using System;
using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Clubs.Representations
{
  public class ClubEditForm
  {
    [Display(Name = "Club Name")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Profile Image URL")]
    public Uri ProfileImageUrl { get; set; }
  }
}