using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Representations.Schemas;

namespace AlphacA.Resources.Users
{
  public class UserRepresentationAdapter
  {
    private readonly UserUriFactory userUriFactory;

    public UserRepresentationAdapter(UserUriFactory userUriFactory)
    {
      this.userUriFactory = userUriFactory;
    }

    public User Domain(UserRepresentation representation)
    {
      return new User
      {
        // Id = Guid.NewGuid().ToString(),
        UserName = representation.UserName,
        FirstName = representation.FirstName,
        MiddleNames = representation.MiddleNames,
        LastName = representation.LastName
      };
    }

    public RepresentationCollection Representation(IEnumerable<IResourceHeader> users)
    {
      return new RepresentationCollection
      {
        Id = this.userUriFactory.MakeCollectionUri(),
        Title = "Users",
        Items = users.Select(x => new Representation
        {
          Id = this.userUriFactory.MakeUri(x.Id),
          Title = x.Title
        }).ToArray(),
        CreateForm = this.userUriFactory.MakeCreateFormUri()
      };
    }

    public UserRepresentation Representation(User user)
    {
      return new UserRepresentation
      {
        Id = this.userUriFactory.MakeUri(user.Id),
        Title = user.Title,
        UserName = user.UserName,
        FirstName = user.FirstName,
        MiddleNames = user.MiddleNames,
        LastName = user.LastName,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt
      };
    }

    public FormRepresentation CreateForm(UserRepresentation representation)
    {
      return new FormRepresentation
      {
        Id = this.userUriFactory.MakeCreateFormUri(),
        Destination = this.userUriFactory.MakeCollectionUri(),
        Title = "Create User",
        Schema = JsonSchema.Generate(representation),
        Form = JsonForm.Generate()
      };
    }

    public Uri GetUserUri(User user)
    {
      return this.userUriFactory.MakeUri(user.Id);
    }
  }
}