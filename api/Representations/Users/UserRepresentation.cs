using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pulsar.AlphacA.Representations.Users
{
  public class UserRepresentation : Representation
  {
    [EmailAddress]
    [JsonProperty("email", Required = Required.Always)]
    [DisplayName("Street Address")]
    [Description("The street address. For example, 1600 Amphitheatre Pkwy.")]
    public string Email { get; set; }
  }
}