using System.Collections.Generic;
using AlphacA.Core;
using AlphacA.Exceptions;
using AlphacA.Representations;
using AlphacA.Users;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Resources.Users
{
  public static class UserRepresentationAdapterExtensions
  {
    public static User Domain(
      this UserRepresentation representation,
      UserRepresentationAdapter adapter) => adapter.Domain(representation);

    public static UserRepresentation Representation(
      this User user, UserRepresentationAdapter adapter) => adapter.Representation(user);

    public static RepresentationCollection CollectionRepresentation(
      this IEnumerable<IResourceHeader> users,
      UserRepresentationAdapter adapter) => adapter.Representation(users);

    public static FormRepresentation CreateForm(
      this UserRepresentation representation,
      UserRepresentationAdapter adapter) => adapter.CreateForm(representation);

    public static CreatedResult CreatedResult(
      this User user, UserRepresentationAdapter adapter) =>
        new(adapter.GetUserUri(user), new { message = "User created." });

    public static ActionResult<UserRepresentation> NotFoundOrResult(
      this User user, UserRepresentationAdapter adapter) =>
        user != null
          ? adapter.Representation(user)
          : new SimpleErrorResult(404, "User not found");
  }
}