using System;
using Xunit;

namespace Anagram.Tests
{
  public class AnagramTests
  {
    [Fact]
    public void NullValueParamThrowsException()
    {
      var ex = Assert.Throws<ArgumentException>(() => (null as string).IsAnagramWith("test"));
      Assert.Equal("Argument <value> cannot be null or empty", ex.Message);
    }

    [Fact]
    public void EmptyValueParamThrowsException()
    {
      var ex = Assert.Throws<ArgumentException>(() => string.Empty.IsAnagramWith("test"));
      Assert.Equal("Argument <value> cannot be null or empty", ex.Message);
    }

    [Fact]
    public void NullOtherValueParamThrowsException()
    {
      var ex = Assert.Throws<ArgumentException>(() => "test".IsAnagramWith(null));
      Assert.Equal("Argument <otherValue> cannot be null or empty", ex.Message);
    }

    [Fact]
    public void EmptyOtherValueParamThrowsException()
    {
      var ex = Assert.Throws<ArgumentException>(() => "test".IsAnagramWith(string.Empty));
      Assert.Equal("Argument <otherValue> cannot be null or empty", ex.Message);
    }

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
  }
}