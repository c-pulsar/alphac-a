using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using AlphacA.Representations;
using System;

namespace AlphacA.Resources.Users
{
  public class UserRepresentation : Representation
  {
    [EmailAddress]
    [Required]
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

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Uri Users { get; set; }
  }
}