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
        Id = this.userUriFactory.MakeCollection(),
        Title = "Users",
        Type = "UserCollection",
        Items = users.Select(x => new Representation
        {
          Id = this.userUriFactory.Make(x.Id),
          Title = x.Title
        }).ToArray(),
        CreateForm = this.userUriFactory.MakeCreateForm()
      };
    }

    public UserRepresentation Representation(User user)
    {
      return new UserRepresentation
      {
        Id = this.userUriFactory.Make(user.Id),
        EditForm = this.userUriFactory.MakeEditForm(user.Id),
        Users = this.userUriFactory.MakeCollection(),
        Title = user.Title,
        Type = "User",
        UserName = user.UserName,
        FirstName = user.FirstName,
        MiddleNames = user.MiddleNames,
        LastName = user.LastName,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt
      };
    }

    public FormRepresentation EditForm(User user)
    {
      var representation = this.Representation(user);

      return new FormRepresentation
      {
        Id = this.userUriFactory.MakeEditForm(user.Id),
        Destination = this.userUriFactory.Make(user.Id),
        Title = "Edit User",
        Schema = JsonSchema.Generate(representation),
        Form = JsonForm.Generate()
      };
    }

    public FormRepresentation CreateForm(UserRepresentation representation)
    {
      return new FormRepresentation
      {
        Id = this.userUriFactory.MakeCreateForm(),
        Destination = this.userUriFactory.MakeCollection(),
        Title = "Create User",
        Schema = JsonSchema.Generate(representation),
        Form = JsonForm.Generate()
      };
    }

    public Uri GetUserUri(User user)
    {
      return this.userUriFactory.Make(user.Id);
    }
  }
}