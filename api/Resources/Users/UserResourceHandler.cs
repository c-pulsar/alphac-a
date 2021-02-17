using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Users;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.CompareExchange;

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

      var reserveEmail = this.documentStore.Operations.Send(
           new PutCompareExchangeValueOperation<string>(
             $"emails/{user.Email}", user.Id, 0));

      if (!reserveEmail.Successful)
      {
        throw new InvalidOperationException("User with email already exist");
      }

      session.SaveChanges();
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

  //var s = this.userService.Create(user)

  // using (var session = this.documentStore.OpenSession())
  // {



  //   session.Store(user, user.Id);
  //   session.SaveChanges();
  // }

  // Console.WriteLine("###################" + user.Id);
  //return this.Created();


}