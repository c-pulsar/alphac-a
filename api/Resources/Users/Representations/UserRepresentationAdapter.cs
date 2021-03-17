using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Resources.Root;
using AlphacA.Resources.Users.Domain;
using NJsonSchema.Generation;

namespace AlphacA.Resources.Users.Representations
{
  public class UserRepresentationAdapter
  {
    private readonly UserUriFactory userUriFactory;
    private readonly RootUriFactory rootUri;

    public UserRepresentationAdapter(
      UserUriFactory userUriFactory,
      RootUriFactory rootUri)
    {
      this.userUriFactory = userUriFactory;
      this.rootUri = rootUri;
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

    public User Domain(UserEditForm editForm)
    {
      return new User
      {
        FirstName = editForm.FirstName,
        MiddleNames = editForm.MiddleNames,
        LastName = editForm.LastName
      };
    }

    public RepresentationCollection Representation(IEnumerable<IResourceHeader> users)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make("home", this.rootUri.MakeRootUri(), "Home"),
          Link.Make("create-form", this.userUriFactory.MakeCreateForm(), "Create"),
          Link.Make("search", this.userUriFactory.MakeSearchForm(), "Search"),
        },

        Id = this.userUriFactory.MakeCollection(),
        Title = "Users",
        Resource = "User",
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
          Link.Make("home", this.rootUri.MakeRootUri(), "Home"),
          Link.Make("edit-form", this.userUriFactory.MakeEditForm(user.Id), "Edit"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users"),
        },

        Id = this.userUriFactory.Make(user.Id),
        Schema = this.userUriFactory.MakeSchema(),
        Title = user.Title,
        Resource = "User",
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
      return new EditFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make("home", this.rootUri.MakeRootUri(), "Home"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users"),
          Link.Make("user", this.userUriFactory.Make(user.Id), "User")
        },

        Id = this.userUriFactory.MakeEditForm(user.Id),
        Resource = "User",
        Schema = this.userUriFactory.MakeEditFormSchema(),
        PostLocation = this.userUriFactory.Make(user.Id),
        ParentLocation = this.userUriFactory.MakeCollection(),
        CanDelete = true,
        Title = "Edit User"
      };
    }

    public FormRepresentation SearchForm()
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make("home", this.rootUri.MakeRootUri(), "Home"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users"),
        },

        Id = this.userUriFactory.MakeSearchForm(),
        Schema = this.userUriFactory.MakeSearchSchema(),
        Resource = "User",
        ParentLocation = this.userUriFactory.MakeCollection(),
        PostLocation = this.userUriFactory.MakeSearchForm(),
        Title = "Search Users"
      };
    }

    public CreateFormRepresentation CreateForm()
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make("home", this.rootUri.MakeRootUri(), "Home"),
          Link.Make("users", this.userUriFactory.MakeCollection(), "Users")
        },

        Id = this.userUriFactory.MakeCreateForm(),
        ParentLocation = this.userUriFactory.MakeCollection(),
        Schema = this.userUriFactory.MakeSchema(),
        Resource = "User",
        PostLocation = this.userUriFactory.MakeCollection(),
        Title = "Create User"
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