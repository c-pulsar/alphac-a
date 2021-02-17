using System;
using System.Collections.Generic;
using Pulsar.AlphacA.Users;

namespace Pulsar.AlphacA.Representations.Users
{
  public class UserResourceHandler
  {
    public User Create(User user)
    {
      return user;
    }

    public User Get(Guid id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Guid> GetAll()
    {
      return Array.Empty<Guid>();
    }
  }
}