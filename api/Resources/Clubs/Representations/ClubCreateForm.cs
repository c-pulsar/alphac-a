using System;
using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Clubs.Representations
{
  public class ClubCreateForm
  {
    [Required]
    [Display(Name = "Club Name")]
    public string Name { get; set; }

    [Display(Name = "Profile Image URL")]
    public Uri ProfileImageUrl { get; set; }
  }
}