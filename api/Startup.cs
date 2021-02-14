using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Pulsar.AlphacA.Configuration;
using Pulsar.AlphacA.DocumentStorage;
using Pulsar.AlphacA.Representations;
using Pulsar.AlphacA.Representations.Formatters;
using Pulsar.AlphacA.Serialisation;

namespace Pulsar.AlphacA
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddConfiguration()
              .AddUriFactory()
              .AddDocumentStore()
              .AddControllers(options =>
                    {
                      options.RespectBrowserAcceptHeader = true;
                      options.OutputFormatters.Add(new HtmlFormOutputFormatter());
                      //options.InputFormatters.Add(new HtmlFormInputFormatter());
                    })
              .AddJsonSerialisation();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseDocumentStoreBuilder()
         .UseRouting()
         .UseStaticFiles(new StaticFileOptions
         {
           FileProvider = new PhysicalFileProvider(
             Path.Combine(env.ContentRootPath, "static")),
           RequestPath = "/static"
         })
         .UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
