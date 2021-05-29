using System;
using System.Linq;
using Xunit;

namespace Anagram.Integration.Tests
{
  public class AnagramIntegrationTests: IClassFixture<FileDictionaryFixture>
  {
    private readonly string[] dictionary;

    public AnagramIntegrationTests(FileDictionaryFixture fixture)
    {
      this.dictionary = fixture.Dictionary;
    }

    [Theory]
    [InlineData("argden", "danger,gander,garden,ranged")]
    [InlineData("riftle", "filter,lifter,trifle")]
    [InlineData("rectan", "nectar,recant,trance")]
    [InlineData("DURHLE", "hurdle,hurled")] // case insensitive check
    [InlineData("rinelat", "latrine,reliant,retinal")]
    [InlineData("voerssap", "overpass,passover")]
    [InlineData("efrinish", "finisher,refinish")]
    [InlineData("kyspe", "pesky,skype")]
    [InlineData("flamengo", "")] // not found
    [InlineData("test", "")] // not found
    public void TestAnagrams(string input, string expected)
    {
      var actual = string.Join(
        ",",
        this.dictionary.Where(x => x.IsAnagramWith(input)).ToArray());

      Console.WriteLine(actual);

      Assert.Equal(expected, actual);
    }
  }
}