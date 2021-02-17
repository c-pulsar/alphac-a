using System;
using System.Collections.Generic;
using AlphacA.Representations;
using AlphacA.Users;

namespace AlphacA.Resources.Users
{
  public static class UserRepresentationAdapterExtensions
  {
    public static User ToDomain(
      this UserRepresentation representation,
      UserRepresentationAdapter adapter) => adapter.ToDomain(representation);

    public static UserRepresentation ToRepresentation(
      this User user, UserRepresentationAdapter adapter) => adapter.ToRepresentation(user);

    public static RepresentationCollection ToUserCollectionRepresentation(
      this IEnumerable<Guid> users,
      UserRepresentationAdapter adapter) => adapter.ToRepresentation(users);

    public static FormRepresentation ToCreateForm(
      this UserRepresentation representation,
      UserRepresentationAdapter adapter) => adapter.ToCreateForm(representation);
  }
}