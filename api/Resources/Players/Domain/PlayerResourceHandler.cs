using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Resources.Players.Indexing;
using AlphacA.Storage.CompareExchange;
using Raven.Client.Documents;

namespace AlphacA.Resources.Players.Domain
{
  public class PlayerResourceHandler
  {
    private readonly IDocumentStore documentStore;
    private readonly IClock clock;

    public PlayerResourceHandler(
      IDocumentStore documentStore, IClock clock)
    {
      this.documentStore = documentStore;
      this.clock = clock;
    }

    public Player Create(Player Player)
    {
      Player.CreatedAt = Player.UpdatedAt = clock.UtcNow();

      using var session = documentStore.OpenSession();
      session.Store(Player, Guid.NewGuid().ToString());

      using (var compareExchangeScope = new CompareExchangeScope(documentStore))
      {
        compareExchangeScope
          .Add($"Playernames/{Player.Email}", Player.Id, "Playername already exist.");

        session.SaveChanges();
        compareExchangeScope.Complete();
      }

      return Player;
    }

    public Player Update(Guid id, Player Player)
    {
      using var session = documentStore.OpenSession();
      var existing = session.Load<Player>(id.ToString());
      if (existing != null)
      {
        existing.UpdatedAt = clock.UtcNow();
        existing.FirstName = Player.FirstName;
        existing.MiddleNames = Player.MiddleNames;
        existing.LastName = Player.LastName;
        existing.ProfileImageUrl = Player.ProfileImageUrl;

        session.SaveChanges();
      }

      return existing;
    }

    public bool Delete(Guid id)
    {
      using var session = documentStore.OpenSession();
      var Player = session.Load<Player>(id.ToString());
      if (Player != null)
      {
        Player.Status = PlayerStatus.Deleted;
        session.SaveChanges();

        return true;
      }

      return false;
    }

    public Player Get(string id)
    {
      using var session = documentStore.OpenSession();
      var Player = session.Load<Player>(id);
      if (Player != null && Player.Status != PlayerStatus.Deleted)
      {
        return Player;
      }

      return null;
    }

    public IEnumerable<IResourceHeader> Find(string searchText)
    {
      using var session = documentStore.OpenSession();
      if (string.IsNullOrWhiteSpace(searchText))
      {
        return session
          .Query<Player>()
          .Where(x => x.Status != PlayerStatus.Deleted)
          .Select(x => new PlayerHeader
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
        .Query<PlayerSearch.Result, PlayerSearch>()
        .Search(x => x.PlayerData, searchText)
        .ToArray();
    }

    public IEnumerable<IResourceHeader> GetClubPlayers(string clubId)
    {
      using var session = documentStore.OpenSession();
      return session
        .Query<Player>()
        .Where(x => x.ClubId == clubId)
        .Select(x => new PlayerHeader
        {
          Id = x.Id,
          FirstName = x.FirstName,
          MiddleNames = x.MiddleNames,
          LastName = x.LastName,
          ProfileImageUrl = x.ProfileImageUrl
        })
        .ToArray();
    }
  }
}