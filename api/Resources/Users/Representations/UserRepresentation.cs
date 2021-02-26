using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;
using AlphacA.Representations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserRepresentation : Representation
  {
    [EmailAddress]
    [Required]
    [Editable(false)] // create only
    [DisplayName("Username")]
    [Description("Must be an email address")]
    public string UserName { get; set; }

    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [DisplayName("Middle Names")]
    public string MiddleNames { get; set; }

    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    //[Editable(false, AllowInitialValue = true)]
    //[IgnoreDataMember]
    [DisplayName("User Since")]
    [ReadOnly(true)]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Last Updated")]
    [ReadOnly(true)]
    public DateTime UpdatedAt { get; set; }
  }
}