using System;
using AlphacA.Core;

namespace AlphacA.Resources.Players.Domain
{
  public interface IPlayerHeader : IResourceHeader
  {
    string FirstName { get; set; }

    string MiddleNames { get; set; }

    string LastName { get; set; }
  }

  public class PlayerHeader : IPlayerHeader
  {
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleNames { get; set; }

    public string LastName { get; set; }

    public Uri ProfileImageUrl { get; set; }

    public string Image => this.ProfileImageUrl?.ToString();

    public string Title => this.GetTitle();
  }
}