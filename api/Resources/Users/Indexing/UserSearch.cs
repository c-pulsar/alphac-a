using System.Linq;
using AlphacA.Resources.Users.Domain;
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
      Map = users => users
      .Where(x => x.Status != UserStatus.Deleted)
      .Select(x =>
        new
        {
          x.Id,
          x.FirstName,
          x.MiddleNames,
          x.LastName,
          x.Image,
          UserData = new[] { x.UserName, x.FirstName, x.MiddleNames, x.LastName }
        });

      Index(x => x.UserData, FieldIndexing.Search);
    }
  }
}