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

        Id = userUriFactory.MakeCollection(),
        Title = "Users",
        Type = "UserCollection",
        Items = users.Select(x => new Representation
        {
          Id = userUriFactory.Make(x.Id),
          Title = x.Title
        }).ToArray(),
        CreateForm = userUriFactory.MakeCreateForm(),
        SearchForm = userUriFactory.MakeSearchForm()
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

        Id = userUriFactory.Make(user.Id),
        EditForm = userUriFactory.MakeEditForm(user.Id),
        Users = userUriFactory.MakeCollection(),
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

    public EditFormRepresentation EditForm(User user, bool deleteEnabled = true)
    {
      var representation = Representation(user);

      return new EditFormRepresentation
      {
        Id = userUriFactory.MakeEditForm(user.Id),
        ResourceId = userUriFactory.Make(user.Id),
        CollectionId = userUriFactory.MakeCollection(),
        Title = "Edit User",
        CanDelete = deleteEnabled,
        Schema = JsonSchema.Generate(representation),
      };
    }

    public SearchFormRepresentation SearchForm(UserSearchRepresentation representation)
    {
      return new SearchFormRepresentation
      {
        Id = userUriFactory.MakeSearchForm(),
        Title = "Search Users",
        Schema = JsonSchema.Generate(representation),
      };
    }

    public CreateFormRepresentation CreateForm(UserRepresentation representation)
    {
      return new CreateFormRepresentation
      {
        Id = this.userUriFactory.MakeCreateForm(),
        CollectionId = this.userUriFactory.MakeCollection(),
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