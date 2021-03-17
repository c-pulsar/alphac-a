using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Core;
using AlphacA.Representations;
using AlphacA.Resources.Root;
using AlphacA.Resources.Users.Domain;

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

    public RepresentationCollection Collection(IEnumerable<IResourceHeader> users)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(IanaLinkRelations.Self, this.userUriFactory.MakeCollection(), "Self"),
          Link.Make(IanaLinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(IanaLinkRelations.CreateForm, this.userUriFactory.MakeCreateForm(), "Create"),
          Link.Make(IanaLinkRelations.Search, this.userUriFactory.MakeSearchForm(), "Search"),
        },

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
          Link.Make(IanaLinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(IanaLinkRelations.Self, this.userUriFactory.Make(user.Id), "Self"),
          Link.Make(IanaLinkRelations.Manifest, this.userUriFactory.MakeSchema(), "Schema"),
          Link.Make(IanaLinkRelations.EditForm, this.userUriFactory.MakeEditForm(user.Id), "Edit"),
          Link.Make(IanaLinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
        },

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
          Link.Make(IanaLinkRelations.Self, this.userUriFactory.MakeEditForm(user.Id), "Self"),
          Link.Make(IanaLinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(IanaLinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
          Link.Make(IanaLinkRelations.Manifest, this.userUriFactory.MakeEditFormSchema(), "Schema"),
          Link.Make(IanaLinkRelations.About, this.userUriFactory.Make(user.Id), "User")
        },

        Resource = "User",
        CanDelete = true,
        Title = "Edit User"
      };
    }

    public Representation SearchForm()
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(IanaLinkRelations.Self, this.userUriFactory.MakeSearchForm(), "Self"),
          Link.Make(IanaLinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(IanaLinkRelations.Manifest, this.userUriFactory.MakeSearchSchema(), "Schema"),
          Link.Make(IanaLinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
        },

        Resource = "User",
        Title = "Search Users"
      };
    }

    public CreateFormRepresentation CreateForm()
    {
      return new CreateFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(IanaLinkRelations.Self, this.userUriFactory.MakeCreateForm(), "Create"),
          Link.Make(IanaLinkRelations.Manifest, this.userUriFactory.MakeSchema(), "Schema"),
          Link.Make(IanaLinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
          Link.Make(IanaLinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
        },
        Resource = "User",
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