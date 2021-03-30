using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Clubs.Representations
{
  public class ClubSearchForm
  {
    [Display(Name = "Search For")]
    [Required]
    public string SearchText { get; set; }
  }
}