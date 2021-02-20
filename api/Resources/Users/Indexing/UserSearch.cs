using System.Linq;
using Raven.Client.Documents.Indexes;

namespace AlphacA.Resources.Users.Indexing
{
  public class UserSearch : AbstractIndexCreationTask<User, UserSearch.Result>
  {
    public class Result : UserHeader
    {
      public string UserData { get; set; }
    }

    public UserSearch()
    {
      Map = users => users.Select(x =>
        new
        {
          x.Id,
          x.FirstName,
          x.MiddleNames,
          x.LastName,
          UserData = new[] { x.UserName, x.FirstName, x.MiddleNames, x.LastName }
        });

      Index(x => x.UserData, FieldIndexing.Search);
    }
  }
}