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

    public User Domain(UserCreateForm createForm)
    {
      return new User
      {
        UserName = createForm.UserName,
        FirstName = createForm.FirstName,
        MiddleNames = createForm.MiddleNames,
        LastName = createForm.LastName,
        ProfileImageUrl = createForm.ProfileImageUrl
      };
    }

    public User Domain(UserEditForm editForm)
    {
      return new User
      {
        FirstName = editForm.FirstName,
        MiddleNames = editForm.MiddleNames,
        LastName = editForm.LastName,
        ProfileImageUrl = editForm.ProfileImageUrl
      };
    }

    public RepresentationCollection Collection(IEnumerable<IResourceHeader> users)
    {
      return new RepresentationCollection
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.userUriFactory.MakeCollection(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.CreateForm, this.userUriFactory.MakeCreateForm(), "Create"),
          Link.Make(LinkRelations.Search, this.userUriFactory.MakeSearchForm(), "Search"),
        },

        Title = "Users",
        Resource = "User",
        Items = users.Select(x => new RepresentationCollectionItem
        {
          Reference = userUriFactory.Make(x.Id),
          Title = x.Title,
          Image = string.IsNullOrWhiteSpace(x.Image) ? null : new Uri(x.Image)
        }).ToArray(),
      };
    }

    public UserRepresentation Representation(User user)
    {
      var links = new List<Link>()
      {
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Self, this.userUriFactory.Make(user.Id), "Self"),
          Link.Make(LinkRelations.Manifest, this.userUriFactory.MakeSchema(), "Schema"),
          Link.Make(LinkRelations.EditForm, this.userUriFactory.MakeEditForm(user.Id), "Edit"),
          Link.Make(LinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users")
      };

      if (user.ProfileImageUrl != null)
      {
        links.Add(Link.Make(LinkRelations.Image, user.ProfileImageUrl, "Image"));
      }

      return new UserRepresentation
      {
        Links = links.ToArray(),
        Title = user.Title,
        Resource = "User",
        UserName = user.UserName,
        FirstName = user.FirstName,
        MiddleNames = user.MiddleNames,
        LastName = user.LastName,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt,
        ProfileImageUrl = user.ProfileImageUrl
      };
    }

    public EditFormRepresentation EditForm(User user)
    {
      return new EditFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.userUriFactory.MakeEditForm(user.Id), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
          Link.Make(LinkRelations.Manifest, this.userUriFactory.MakeEditFormSchema(), "Schema"),
          Link.Make(LinkRelations.About, this.userUriFactory.Make(user.Id), "User")
        },

        Resource = "User",
        DeleteEnabled = true,
        Title = "Edit User"
      };
    }

    public SearchFormRepresentation SearchForm()
    {
      return new SearchFormRepresentation
      {
        Links = new Link[]
        {
          Link.Make(LinkRelations.Self, this.userUriFactory.MakeSearchForm(), "Self"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
          Link.Make(LinkRelations.Manifest, this.userUriFactory.MakeSearchSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
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
          Link.Make(LinkRelations.Self, this.userUriFactory.MakeCreateForm(), "Create"),
          Link.Make(LinkRelations.Manifest, this.userUriFactory.MakeCreateFormSchema(), "Schema"),
          Link.Make(LinkRelations.Collection, this.userUriFactory.MakeCollection(), "Users"),
          Link.Make(LinkRelations.Start, this.rootUri.MakeRootUri(), "Home"),
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