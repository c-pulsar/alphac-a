using System.Collections.Generic;
using AlphacA.Representations;
using AlphacA.Users;
using Microsoft.AspNetCore.Mvc;

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
      this IEnumerable<string> users,
      UserRepresentationAdapter adapter) => adapter.ToRepresentation(users);

    public static FormRepresentation ToCreateForm(
      this UserRepresentation representation,
      UserRepresentationAdapter adapter) => adapter.ToCreateForm(representation);

    public static CreatedResult ToCreatedResult(
      this User user, UserRepresentationAdapter adapter) => adapter.ToCreatedResult(user);
  }
}