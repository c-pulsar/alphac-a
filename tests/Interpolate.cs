
using System;
using System.Collections.Generic;
using System.Linq;

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

      return tokens
        .Aggregate(
          input, (current, token) =>
          string.IsNullOrWhiteSpace(token.Key)
            ? throw new FormatException("Invalid token format")
            : current.Replace($"[{token.Key}]", token.Value))
        .Replace("[[", "[")
        .Replace("]]", "]");
    }
  }
}