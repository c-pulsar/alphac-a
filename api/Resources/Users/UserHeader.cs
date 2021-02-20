using AlphacA.Core;

namespace AlphacA.Resources.Users
{
  public class UserHeader : IResourceHeader
  {
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleNames { get; set; }

    public string LastName { get; set; }

    public string Title => $"{this.FirstName} {this.MiddleNames} {this.LastName}";
  }
}