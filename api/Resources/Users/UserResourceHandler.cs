using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Storage.CompareExchange;
using AlphacA.Users;
using Raven.Client.Documents;

namespace AlphacA.Resources.Users
{
  public class UserResourceHandler
  {
    private readonly IDocumentStore documentStore;

    public UserResourceHandler(IDocumentStore documentStore)
    {
      this.documentStore = documentStore;
    }

    public User Create(User user)
    {
      using var session = this.documentStore.OpenSession();
      session.Store(user, Guid.NewGuid().ToString());

      using (var compareExchangeScope = new CompareExchangeScope(this.documentStore))
      {
        compareExchangeScope.Start(
          new KeyValuePair<string, string>($"usernames/{user.UserName}", user.Id),
          new KeyValuePair<string, string>($"emails/{user.Email}", user.Id));

        session.SaveChanges();
        compareExchangeScope.Complete();
      }

      return user;
    }

    public User Get(string id)
    {
      using var session = this.documentStore.OpenSession();
      return session.Load<User>(id);
    }

    public IEnumerable<string> GetAll()
    {
      using var session = this.documentStore.OpenSession();
      return session.Query<User>().ToArray().Select(x => x.Id);
    }
  }
}