using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;
using AlphacA.Representations;
using NJsonSchema;
using NJsonSchema.Annotations;

namespace AlphacA.Resources.Users.Representations
{
  public class UserRepresentation : Representation
  {
    [EmailAddress]
    [Required]
    [Display(Name = "Username")]
    [Description("Must be an email address")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Middle Names")]
    public string MiddleNames { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "User Since")]
    [ReadOnly(true)]
    [JsonSchema(JsonObjectType.String)]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Last Updated")]
    [ReadOnly(true)]
    [JsonSchema(JsonObjectType.String)]
    public DateTime UpdatedAt { get; set; }
  }
}