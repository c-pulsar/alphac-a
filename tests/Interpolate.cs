
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public static class StringExtensions
{
  public static string Interpolate([NotNull] this string value, IDictionary<string, string> tokens)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      throw new ArgumentException($"Argument <{nameof(value)}> cannot be null or empty");
    }

    if (tokens == null || tokens.Count == 0)
    {
      throw new ArgumentException($"Argument <{nameof(tokens)}> cannot be null or empty");
    }

    string result = value;

    //tokens.Aggregate()

    foreach (var token in tokens)
    {
      if (string.IsNullOrWhiteSpace(token.Key))
      {
        throw new InvalidOperationException("");
      }

      // TODO: handle token not found in string

      result = result.Replace($"[{token.Key}]", token.Value);
      // {1} 
      //var pattern = @"\[guitarist\]";

      //result = Regex.Replace(result, @$"\[{x.Key}\]", x.Value);
      //result = Regex.Replace(value, pattern, x.Value);
    }

    return result.Replace("[[", "[").Replace("]]", "]");
  }
}