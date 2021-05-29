using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Anagram
{
  public class AnagramTests
  {
    [Theory]
    [InlineData("abc", "cde", false)]
    [InlineData("abc", "bca", true)]
    [InlineData(" abc", "bca ", true)]
    [InlineData("abc", "bca ", false)]
    [InlineData("onetwothree", "threetwoone", true)]
    [InlineData("bicycle", "bicyclle", false)]
    [InlineData("person", "sonPer", true)] // case insentive check
    [InlineData("lueavmeos", "somevalue", true)]
    [InlineData("abc", "abc", true)]
    [InlineData("abc", "abcd", false)]
    [InlineData("abcd", "abc", false)]
    public void IsAnagram(string value, string otherValue, bool isAnagram)
    {
      Assert.Equal(value.IsAnagramWith(otherValue), isAnagram);
    }

    [Fact]
    public void WriteAnagrams()
    {
      var search = "ralsbtosa";

      var sw = new Stopwatch();

      sw.Start();
      var result = WebFile.ReadLines().Where(x => x.IsAnagramWith(search)).ToArray();
      sw.Stop();
      Console.WriteLine(sw.Elapsed);
      foreach (var w in result)
      {
        Console.WriteLine(w);
      }

    }
  }
}