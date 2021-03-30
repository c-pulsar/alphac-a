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
      Map = clubs => clubs
      .Where(x => x.Status != ClubStatus.Deleted)
      .Select(x =>
        new
        {
          x.Id,
          x.Name,
          x.ProfileImageUrl,
          ClubData = new[] { x.Name }
        });

      Index(x => x.ClubData, FieldIndexing.Search);
    }
  }
}