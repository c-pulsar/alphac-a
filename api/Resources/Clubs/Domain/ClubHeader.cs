using System;
using AlphacA.Core;

namespace AlphacA.Resources.Clubs.Domain
{
  public interface IClubHeader : IResourceHeader
  {
    string Name { get; set; }
  }

  public class ClubHeader : IClubHeader
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public Uri ProfileImageUrl { get; set; }

    public string Image => this.ProfileImageUrl?.ToString();

    public string Title => this.Name;
  }
}