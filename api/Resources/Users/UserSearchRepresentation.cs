using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AlphacA.Representations;

namespace AlphacA.Resources.Users
{
  public class UserSearchRepresentation : Representation
  {
    [DisplayName("Search For")]
    [Required]
    public string SearchText { get; set; }
  }
}