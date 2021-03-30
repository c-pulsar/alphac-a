namespace AlphacA.Resources.Players.Domain
{
  public static class PlayerHeaderExtensions
  {
    public static string GetTitle(this IPlayerHeader header)
    {
      return $"{header.FirstName} {header.MiddleNames} {header.LastName}";
    }
  }
}