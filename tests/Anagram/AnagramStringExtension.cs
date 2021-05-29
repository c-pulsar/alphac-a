using System;
using System.Linq;

namespace Anagram
{
  public static class AnagramStringExtensions
  {
    public static bool IsAnagramWith(this string value, string otherValue)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentException($"Argument <{nameof(value)}> cannot be null or empty");
      }

      if (string.IsNullOrWhiteSpace(otherValue))
      {
        throw new ArgumentException($"Argument <{nameof(otherValue)}> cannot be null or empty");
      }

      return string.Equals(
        new string(value.ToLower().ToCharArray().OrderBy(x => x).ToArray()),
        new string(otherValue.ToLower().ToCharArray().OrderBy(x => x).ToArray()));
    }
  }
}