using System.ComponentModel.DataAnnotations;
using System;
using AlphacA.Representations;

namespace AlphacA.Resources.Clubs.Representations
{
  public class ClubRepresentation : Representation
  {
    [Display(Name = "Club Name")]
    public string Name { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Last Updated")]
    public DateTime UpdatedAt { get; set; }

    [Display(Name = "Profile Image URL")]
    public Uri ProfileImageUrl { get; set; }
  }
}