namespace AlphacA.Resources.Users
{
  public static class UserHeaderExtensions
  {
    public static string GetTitle(this IUserHeader header)
    {
      return $"{header.FirstName} {header.MiddleNames} {header.LastName}";
    }
  }
}