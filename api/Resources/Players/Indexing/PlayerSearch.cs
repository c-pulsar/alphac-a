using System.Linq;
using AlphacA.Resources.Players.Domain;
using Raven.Client.Documents.Indexes;

namespace AlphacA.Resources.Players.Indexing
{
  public class PlayerSearch : AbstractIndexCreationTask<Player, PlayerSearch.Result>
  {
    public class Result : PlayerHeader
    {
      public string PlayerData { get; set; }
    }

    public PlayerSearch()
    {
      Map = Players => Players
      .Where(x => x.Status != PlayerStatus.Deleted)
      .Select(x =>
        new
        {
          x.Id,
          x.FirstName,
          x.MiddleNames,
          x.LastName,
          x.ProfileImageUrl,
          PlayerData = new[] { x.PlayerName, x.FirstName, x.MiddleNames, x.LastName }
        });

      Index(x => x.PlayerData, FieldIndexing.Search);
    }
  }
}