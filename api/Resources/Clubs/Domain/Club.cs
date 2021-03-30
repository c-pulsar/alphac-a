
using System;

namespace AlphacA.Resources.Clubs.Domain
{
  public class Club : IClubHeader
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ClubStatus Status { get; set; }
    public Uri ProfileImageUrl { get; set; }
    public string Title => this.Name;
    public string Image => this.ProfileImageUrl?.ToString();
  }
}
