using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pulsar.AlphacA.Representations.Users
{
  public class UserRepresentation : Representation
  {
    [EmailAddress]
    [Required]
    [DisplayName("Email")]
    [Description("User email address")]
    public string Email { get; set; }

    [Required]
    [DisplayName("Username")]
    public string UserName { get; set; }

    [Required]
    [DisplayName("First Name")]
    [Description("User first name")]
    public string FirstName { get; set; }

    [DisplayName("Middle Names")]
    [Description("User middle names")]
    public string MiddleNames { get; set; }

    [Required]
    [DisplayName("Last Name")]
    [Description("User last name")]
    public string LastName { get; set; }
  }
}