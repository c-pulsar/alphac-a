using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Anagram.Integration.Tests
{
  public class FileDictionaryFixture
  {
    private static IEnumerable<string> LoadDictionary(string fileAddress)
    {
      using var client = new WebClient();
      using var stream = client.OpenRead(fileAddress);
      using var reader = new StreamReader(stream);

      return reader.ReadToEnd().Split(
        Environment.NewLine,
        StringSplitOptions.RemoveEmptyEntries);
    }

    public FileDictionaryFixture()
    {
      const string fileUrl = "https://raw.githubusercontent.com/openethereum/wordlist/master/res/wordlist.txt";
      this.Dictionary = LoadDictionary(fileUrl).ToArray();
    }

    public string[] Dictionary { get; }
  }
}