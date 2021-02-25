using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Representations.Schemas;
using AlphacA.Resources.Users.Domain;

namespace AlphacA.Resources.Users.Representations
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
        Links = new Link[]
        {
          Link.Make("self", userUriFactory.MakeCollection(), "Self"),
          Link.Make("create-form", userUriFactory.MakeCreateForm(), "Create"),
          Link.Make("search", userUriFactory.MakeSearchForm(), "Search"),
        },

        Title = "Users",
        Type = "UserCollection",
        Items = users.Select(x => new RepresentationCollectionItem
        {
          Reference = userUriFactory.Make(x.Id),
          Title = x.Title
        }).ToArray(),
      };
    }

    public UserRepresentation Representation(User user)
    {
      return new UserRepresentation
      {
        Links = new Link[]
        {
          Link.Make("self", userUriFactory.Make(user.Id), "Self"),
          Link.Make("edit-form", userUriFactory.MakeEditForm(user.Id), "Edit"),
          Link.Make("users", userUriFactory.MakeCollection(), "Users"),
        },

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

    public EditFormRepresentation EditForm(User user)
    {
      var representation = this.Representation(user);

      return new EditFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make("self", this.userUriFactory.MakeEditForm(user.Id), "Self"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users"),
          Link.Make("user", this.userUriFactory.Make(user.Id), "User")
        },

        PostUri = this.userUriFactory.Make(user.Id),
        Title = "Edit User",
        Schema = JsonSchema.Generate(representation),
      };
    }

    public SearchFormRepresentation SearchForm(UserSearchRepresentation representation)
    {
      return new SearchFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make("self", userUriFactory.MakeSearchForm(), "Self")
        },

        Title = "Search Users",
        Schema = JsonSchema.Generate(representation),
      };
    }

    public CreateFormRepresentation CreateForm(UserRepresentation representation)
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make("self", this.userUriFactory.MakeCreateForm(), "Self"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users")
        },

        PostUri = this.userUriFactory.MakeCollection(),
        Title = "Create User",
        Schema = JsonSchema.Generate(representation),
      };
    }

    public Uri GetCollectionUri(string search)
    {
      return userUriFactory.MakeCollection(search);
    }

    public Uri GetUserUri(User user)
    {
      return userUriFactory.Make(user.Id);
    }
  }
}