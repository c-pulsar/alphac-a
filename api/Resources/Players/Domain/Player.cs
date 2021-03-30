
using System;

namespace AlphacA.Resources.Players.Domain
{
  public class Player : IPlayerHeader
  {
    public string Id { get; set; }
    public string PlayerName { get; set; }
    public string FirstName { get; set; }
    public string MiddleNames { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PlayerStatus Status { get; set; }
    public Uri ProfileImageUrl { get; set; }
    public string Title => this.GetTitle();
    public string Image => this.ProfileImageUrl?.ToString();
  }
}
