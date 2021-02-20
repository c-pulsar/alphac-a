using System.Linq;
using AlphacA.Users;
using Raven.Client.Documents.Indexes;

namespace AlphacA.Resources.Users.Indexing
{
  public class UserSearch : AbstractIndexCreationTask<User, UserSearch.Result>
  {
    public class Result
    {
      public string UserData { get; set; }
    }

    public UserSearch()
    {
      Map = users => users.Select(x =>
        new
        {
          UserData = new object[]
          {
            x.UserName,
            x.FirstName,
            x.MiddleNames,
            x.LastName
          }
        });

      Index(x => x.UserData, FieldIndexing.Search);
    }
  }
}