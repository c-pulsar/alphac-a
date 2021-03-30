using System.Linq;
using AlphacA.Resources.Clubs.Domain;
using Raven.Client.Documents.Indexes;

namespace AlphacA.Resources.Clubs.Indexing
{
  public class ClubSearch : AbstractIndexCreationTask<Club, ClubSearch.Result>
  {
    public class Result : ClubHeader
    {
      public string ClubData { get; set; }
    }

    public ClubSearch()
    {
      Map = users => users
      .Where(x => x.Status != ClubStatus.Deleted)
      .Select(x =>
        new
        {
          x.Id,
          x.Name,
          x.ProfileImageUrl,
          UserData = new[] { x.Name }
        });

      Index(x => x.ClubData, FieldIndexing.Search);
    }
  }
}