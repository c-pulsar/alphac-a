using System.Collections.Generic;
using System.Linq;
using AlphacA.Representations;
using AlphacA.Representations.Schemas;
using AlphacA.Users;
using Microsoft.AspNetCore.Mvc;

namespace AlphacA.Resources.Users
{
  public class UserRepresentationAdapter
  {
    private readonly UserUriFactory userUriFactory;

    public UserRepresentationAdapter(UserUriFactory userUriFactory)
    {
      this.userUriFactory = userUriFactory;
    }

    public User ToDomain(UserRepresentation representation)
    {
      return new User
      {
        // Id = Guid.NewGuid().ToString(),
        UserName = representation.UserName,
        Email = representation.Email,
        FirstName = representation.FirstName,
        MiddleNames = representation.MiddleNames,
        LastName = representation.LastName
      };
    }

    public RepresentationCollection ToRepresentation(IEnumerable<string> users)
    {
      return new RepresentationCollection
      {
        Id = userUriFactory.MakeCollectionUri(),
        Title = "Users",
        Items = users.Select(x => userUriFactory.MakeUri(x)).ToArray(),
        CreateForm = userUriFactory.MakeCreateFormUri()
      };
    }

    public UserRepresentation ToRepresentation(User user)
    {
      return new UserRepresentation
      {
        Id = userUriFactory.MakeUri(user.Id),
        Email = user.Email,
        Title = $"{user.FirstName} {user.MiddleNames} {user.LastName}",
        UserName = user.UserName,
        FirstName = user.FirstName,
        MiddleNames = user.MiddleNames,
        LastName = user.LastName
      };
    }

    public FormRepresentation ToCreateForm(UserRepresentation representation)
    {
      return new FormRepresentation
      {
        Id = userUriFactory.MakeCreateFormUri(),
        Destination = userUriFactory.MakeCollectionUri(),
        Title = "Create User",
        Schema = JsonSchema.Generate(representation),
        Form = JsonForm.Generate()
      };
    }

    public CreatedResult ToCreatedResult(User user)
    {
      return new CreatedResult(userUriFactory.MakeUri(user.Id), new { message = "User created." });
    }
  }
}