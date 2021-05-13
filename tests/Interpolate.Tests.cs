
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class InterpolateTests
{
  [Fact]
  public void NullValueThrowsException()
  {
    var ex = Assert.Throws<ArgumentException>(() => (null as string).Interpolate(new Dictionary<string, string> { { "name", "test" } }));
    Assert.Equal("Argument <value> cannot be null or empty", ex.Message);
  }

  [Fact]
  public void EmptyValueThrowsException()
  {
    var ex = Assert.Throws<ArgumentException>(() => "".Interpolate(new Dictionary<string, string> { { "name", "test" } }));
    Assert.Equal("Argument <value> cannot be null or empty", ex.Message);
  }

  [Fact]
  public void NullTokensThrowsException()
  {
    var ex = Assert.Throws<ArgumentException>(() => "Some value".Interpolate(null));
    Assert.Equal("Argument <tokens> cannot be null or empty", ex.Message);
  }

  [Fact]
  public void EmptyTokensThrowsException()
  {
    var ex = Assert.Throws<ArgumentException>(() => "Some value".Interpolate(new Dictionary<string, string>()));
    Assert.Equal("Argument <tokens> cannot be null or empty", ex.Message);
  }

  [Fact]
  public void NoMatchReturnsUnmodifiedString()
  {
    Assert.Equal("Some value", "Some value".Interpolate(new Dictionary<string, string>{{"p", "x"}}));
  }

  [Fact]
  public void SingleMatchIsValid()
  {
    Assert.Equal("Hello Jim", "Hello [name]".Interpolate(new Dictionary<string, string>{{"name", "Jim"}}));
  }

  [Fact]
  public void EscapedStringIsIgnored()
  {
    Assert.Equal("Hello Jim [author]", "Hello [name] [[author]]".Interpolate(new Dictionary<string, string>{{"name", "Jim"}}));
  }
}