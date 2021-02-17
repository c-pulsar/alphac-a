using Pulsar.AlphacA.Users;

namespace Pulsar.AlphacA.Representations.Users
{
  public static class UserResourceHandlerExtensions
  {
    public static User Create(this User user, UserResourceHandler resourceHandler)
       => resourceHandler.Create(user);
  }
}