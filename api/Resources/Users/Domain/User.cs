
using System;

namespace AlphacA.Resources.Users.Domain
{
  public class User : IUserHeader
  {
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string MiddleNames { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public UserStatus Status { get; set; }
    public Uri ProfileImageUrl { get; set; }
    public string Title => this.GetTitle();
    public string Image => this.ProfileImageUrl?.ToString();
  }
}
