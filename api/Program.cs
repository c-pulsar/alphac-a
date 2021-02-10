using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pulsar.AlphacA.Database;

namespace Pulsar.AlphacA
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var dbM = new RavendbMaintainance("http://database:8080", "SampleDataDB");
      dbM.Setup();

      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
              builder => builder
                .UseUrls("http://*:3010")
                .UseStartup<Startup>());
  }
}
