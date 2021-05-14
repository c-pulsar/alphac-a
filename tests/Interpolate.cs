
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

      if (tokens.Keys.Any(k => string.IsNullOrWhiteSpace(k)))
      {
        throw new FormatException("Invalid token format");
      }

      var matches = Regex.Matches(input, @"\[\w+\]");
      var result = new StringBuilder();
      var position = 0;

      foreach (Match match in matches)
      {
        if (match.Index > position)
        {
          result.Append(input, position, match.Index - position);
        }

        var tokenKey = match.Value[1..(match.Length - 1)];
        var tokenValue = IgnoreTokenMatch(match, input) ? null : tokens.GetValueOrDefault(tokenKey);
        result.Append(tokenValue ?? tokenKey);

        position = match.Index + match.Length;
      }

      if (position < input.Length)
      {
        result.Append(input, position, input.Length - position);
      }

      return result.ToString();
    }

    private static string GetValueOrDefault(this IDictionary<string, string> tokens, string tokenKey)
    {
      if (tokens.ContainsKey(tokenKey))
      {
        return tokens[tokenKey];
      }

      return null;
    }

    private static bool IgnoreTokenMatch(Match match, string input)
    {
      var nextIndex = match.Index + match.Length;
      return match.Index > 0 &&
        input[match.Index] == '[' &&
        nextIndex < input.Length &&
        input[nextIndex] == ']';
    }

    public static string Interpolate2(this string input, IDictionary<string, string> tokens)
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