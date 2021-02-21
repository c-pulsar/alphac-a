using System.Collections.Generic;

namespace AlphacA.Serialisation
{
  public class JsonPropertyComparer : IComparer<string>
  {
    public int Compare(string x, string y)
    {
      if (x.StartsWith('@'))
      {
        // x preceeds 
        return -1;
      }

      if (y.StartsWith('@'))
      {
        return 1;
      }

      return string.Compare(x, y);
    }
  }
}