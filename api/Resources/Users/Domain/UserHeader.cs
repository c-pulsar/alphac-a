using AlphacA.Core;

namespace AlphacA.Resources.Users.Domain
{
  public interface IUserHeader : IResourceHeader
  {
    string FirstName { get; set; }

    string MiddleNames { get; set; }

    string LastName { get; set; }
  }

  public class UserHeader : IUserHeader
  {
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleNames { get; set; }

    public string LastName { get; set; }

    public string Image { get; set; }

    public string Title => this.GetTitle();
  }
}