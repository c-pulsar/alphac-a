using System;
using System.Collections.Generic;
using System.Linq;
using AlphacA.Representations;
using AlphacA.Representations.Schemas;
using AlphacA.Users;

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

    public RepresentationCollection ToRepresentation(IEnumerable<Guid> users)
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
        //Uri = userUriFactory.MakeCollectionUri(),
        // Title = "Users",
        // Email = "christiano@gmail.com"
        //Items = System.Array.Empty<string>(),
        //CreateFormUri = "http://localhost:3010/users/create-form"
        //Items = new string[] { "http://localhost:3010/users/1", "http://localhost:3010/users/2" }
      };
    }

    public FormRepresentation ToCreateForm(UserRepresentation representation)
    {
      return new FormRepresentation
      {
        Id = userUriFactory.MakeCreateFormUri(),
        //DestinationUri = userUriFactory.MakeCollectionUri(),
        Title = "Create User",
        Schema = JsonSchema.Generate(representation),
        Form = JsonForm.Generate()
      };
    }
  }
}