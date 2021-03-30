using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Resources.Clubs.Indexing;
using Raven.Client.Documents;

namespace AlphacA.Resources.Clubs.Domain
{
  public class ClubResourceHandler
  {
    private readonly IDocumentStore documentStore;
    private readonly IClock clock;

    public ClubResourceHandler(
      IDocumentStore documentStore, IClock clock)
    {
      this.documentStore = documentStore;
      this.clock = clock;
    }

    public Club Create(Club club)
    {
      club.CreatedAt = club.UpdatedAt = clock.UtcNow();

      using var session = documentStore.OpenSession();
      session.Store(club, Guid.NewGuid().ToString());

      session.SaveChanges();

      return club;
    }

    public Club Update(Guid id, Club club)
    {
      using var session = documentStore.OpenSession();
      var existing = session.Load<Club>(id.ToString());
      if (existing != null)
      {
        existing.UpdatedAt = clock.UtcNow();
        existing.Name = club.Name;
        existing.ProfileImageUrl = club.ProfileImageUrl;

        session.SaveChanges();
      }

      return existing;
    }

    public bool Delete(Guid id)
    {
      using var session = documentStore.OpenSession();
      var club = session.Load<Club>(id.ToString());
      if (club != null)
      {
        club.Status = ClubStatus.Deleted;
        session.SaveChanges();

        return true;
      }

      return false;
    }

    public Club Get(string id)
    {
      using var session = documentStore.OpenSession();
      var club = session.Load<Club>(id);
      if (club != null && club.Status != ClubStatus.Deleted)
      {
        return club;
      }

      return null;
    }

    public IEnumerable<IResourceHeader> Find(string searchText)
    {
      using var session = documentStore.OpenSession();
      if (string.IsNullOrWhiteSpace(searchText))
      {
        return session
          .Query<Club>()
          .Where(x => x.Status != ClubStatus.Deleted)
          .Select(x => new ClubHeader
          {
            Id = x.Id,
            Name = x.Name,
            ProfileImageUrl = x.ProfileImageUrl
          })
          .ToArray();
      }

      return session
        .Query<ClubSearch.Result, ClubSearch>()
        .Search(x => x.ClubData, searchText)
        .ToArray();
    }
  }
}