using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

public static class WebFile
{
  public static IEnumerable<string>  ReadLines(string fileAddress = "https://raw.githubusercontent.com/openethereum/wordlist/master/res/wordlist.txt")
  {
    using var client = new WebClient();
    using var stream = client.OpenRead(fileAddress);
    using var reader = new StreamReader(stream);

    return reader
      .ReadToEnd()
      .Split(
        Environment.NewLine,
        StringSplitOptions.RemoveEmptyEntries);
    }
}