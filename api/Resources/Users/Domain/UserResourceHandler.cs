using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Resources.Users.Indexing;
using AlphacA.Storage.CompareExchange;
using Raven.Client.Documents;

namespace AlphacA.Resources.Users.Domain
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
      user.CreatedAt = user.UpdatedAt = clock.UtcNow();

      using var session = documentStore.OpenSession();
      session.Store(user, Guid.NewGuid().ToString());

      using (var compareExchangeScope = new CompareExchangeScope(documentStore))
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
      using var session = documentStore.OpenSession();
      var existing = session.Load<User>(id.ToString());
      if (existing != null)
      {
        existing.UpdatedAt = clock.UtcNow();
        existing.FirstName = user.FirstName;
        existing.MiddleNames = user.MiddleNames;
        existing.LastName = user.LastName;
        existing.ProfileImageUrl = user.ProfileImageUrl;

        session.SaveChanges();
      }

      return existing;
    }

    public bool Delete(Guid id)
    {
      using var session = documentStore.OpenSession();
      var user = session.Load<User>(id.ToString());
      if (user != null)
      {
        user.Status = UserStatus.Deleted;
        session.SaveChanges();

        return true;
      }

      return false;
    }

    public User Get(string id)
    {
      using var session = documentStore.OpenSession();
      var user = session.Load<User>(id);
      if (user != null && user.Status != UserStatus.Deleted)
      {
        return user;
      }

      return null;
    }

    public IEnumerable<IResourceHeader> Find(string searchText)
    {
      using var session = documentStore.OpenSession();
      if (string.IsNullOrWhiteSpace(searchText))
      {
        return session
          .Query<User>()
          .Where(x => x.Status != UserStatus.Deleted)
          .Select(x => new UserHeader
          {
            Id = x.Id,
            FirstName = x.FirstName,
            MiddleNames = x.MiddleNames,
            LastName = x.LastName,
            ProfileImageUrl = x.ProfileImageUrl
          })
          .ToArray();
      }

      return session
        .Query<UserSearch.Result, UserSearch>()
        .Search(x => x.UserData, searchText)
        .ToArray();
    }
  }
}