using System.ComponentModel.DataAnnotations;

namespace AlphacA.Resources.Players.Representations
{
  public class PlayerSearchForm
  {
    [Display(Name = "Search For")]
    [Required]
    public string SearchText { get; set; }
  }
}