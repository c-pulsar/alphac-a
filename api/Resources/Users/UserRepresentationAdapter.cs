using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Representations.Schemas;
using AlphacA.Resources.Users.Domain;

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
        CreateForm = this.userUriFactory.MakeCreateForm(),
        SearchForm = this.userUriFactory.MakeSearchForm()
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

    public FormRepresentation EditForm(User user, bool deleteEnabled = true)
    {
      var representation = this.Representation(user);

      return new FormRepresentation
      {
        Id = this.userUriFactory.MakeEditForm(user.Id),
        Destination = this.userUriFactory.Make(user.Id),
        Title = "Edit User",
        CanDelete = deleteEnabled,
        Schema = JsonSchema.Generate(representation),
      };
    }

    public FormRepresentation SearchForm(UserSearchRepresentation representation)
    {
      return new FormRepresentation
      {
        Id = this.userUriFactory.MakeSearchForm(),
        Destination = this.userUriFactory.MakeSearchForm(),
        Title = "Search Users",
        Schema = JsonSchema.Generate(representation),
      };
    }

    public FormRepresentation CreateForm(UserRepresentation representation)
    {
      return new FormRepresentation
      {
        Id = this.userUriFactory.MakeCreateForm(),
        Destination = this.userUriFactory.MakeCreateForm(),
        Title = "Create User",
        Schema = JsonSchema.Generate(representation),
      };
    }

    public Uri GetCollectionUri(string search)
    {
      return this.userUriFactory.MakeCollection(search);
    }

    public Uri GetUserUri(User user)
    {
      return this.userUriFactory.Make(user.Id);
    }
  }
}