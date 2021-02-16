using Pulsar.AlphacA.Users;

namespace Pulsar.AlphacA.Representations.Users
{
  public class UserRepresentationMapper
  {
    public User MakeNewUser(UserRepresentation representation)
    {
      return new User
      {
        UserName = representation.UserName,
        Email = representation.Email,
        FirstName = representation.FirstName,
        MiddleNames = representation.MiddleNames,
        LastName = representation.LastName
      };
    }
  }
}