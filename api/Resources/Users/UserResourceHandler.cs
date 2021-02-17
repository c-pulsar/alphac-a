using System;
using System.Collections.Generic;
using System.Linq;
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

  //   var cmpxng = new PutCompareExchangeValueOperation<string>(
  //   "ayende",
  //   "users/2",
  //   0 // meaning empty
  //   );

  //   var result = documentStore.Operations.Send(cmpxng);

  //   session.Store(user, user.Id);
  //   session.SaveChanges();
  // }

  // Console.WriteLine("###################" + user.Id);
  //return this.Created();


}