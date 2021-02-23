using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Resources.Users.Domain;
using AlphacA.Resources.Users.Indexing;
using AlphacA.Storage.CompareExchange;
using Raven.Client.Documents;

namespace AlphacA.Resources.Users
{
  public class UserResourceHandler
  {
    private readonly IDocumentStore documentStore;
    private readonly IClock clock;

    public UserResourceHandler(
      IDocumentStore documentStore, IClock clock)
    {
      this.documentStore = documentStore;
      this.clock = clock;
    }

    public User Create(User user)
    {
      user.CreatedAt = user.UpdatedAt = this.clock.UtcNow();

      using var session = this.documentStore.OpenSession();
      session.Store(user, Guid.NewGuid().ToString());

      using (var compareExchangeScope = new CompareExchangeScope(this.documentStore))
      {
        compareExchangeScope
          .Add($"usernames/{user.UserName}", user.Id, "Username already exist.");

        session.SaveChanges();
        compareExchangeScope.Complete();
      }

      return user;
    }

    public User Update(Guid id, User user)
    {
      using var session = this.documentStore.OpenSession();
      var existing = session.Load<User>(id.ToString());
      if (existing != null)
      {
        existing.UpdatedAt = this.clock.UtcNow();
        existing.FirstName = user.FirstName;
        existing.MiddleNames = user.MiddleNames;
        existing.LastName = user.LastName;

        session.SaveChanges();
      }

      return existing;
    }

    public User Get(string id)
    {
      using var session = this.documentStore.OpenSession();
      return session.Load<User>(id);
    }

    public IEnumerable<IResourceHeader> Find(string searchText)
    {
      using var session = this.documentStore.OpenSession();
      if (string.IsNullOrWhiteSpace(searchText))
      {
        return session.Query<User>().Select(x =>
          new UserHeader
          {
            Id = x.Id,
            FirstName = x.FirstName,
            MiddleNames = x.MiddleNames,
            LastName = x.LastName
          }).ToArray();
      }

      return session
        .Query<UserSearch.Result, UserSearch>()
        .Search(x => x.UserData, searchText)
        .ToArray();
    }
  }
}