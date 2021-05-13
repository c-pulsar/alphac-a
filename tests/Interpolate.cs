
using System;
using System.Collections.Generic;

namespace Interpolation
{
  public static class StringExtensions
  {
    public static string Interpolate(this string input, IDictionary<string, string> tokens)
    {
      if (string.IsNullOrWhiteSpace(input))
      {
        throw new ArgumentException($"Argument <{nameof(input)}> cannot be null or empty");
      }

      if (tokens == null || tokens.Count == 0)
      {
        throw new ArgumentException($"Argument <{nameof(tokens)}> cannot be null or empty");
      }

      string result = input;

      foreach (var token in tokens)
      {
        result = result.Replace($"[{token.Key}]", token.Value);
      }

      return result.Replace("[[", "[").Replace("]]", "]");
    }
  }
}