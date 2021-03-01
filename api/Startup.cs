using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using AlphacA.Configuration;
using AlphacA.Representations;
using AlphacA.Representations.Formatters;
using AlphacA.Serialisation;
using AlphacA.Resources.Users;
using AlphacA.Resources.Root;
using AlphacA.Storage;
using AlphacA.Exceptions;
using AlphacA.Core;
using AlphacA.Auth;

namespace AlphacA
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
              .AddUriInfrastructure()
              .AddAuth()
              .AddCore()
              .AddExceptions()
              .AddDocumentStore()
              .AddRoot()
              .AddUser()
              .AddControllers(options =>
                    {
                      options.RespectBrowserAcceptHeader = true;
                      options.OutputFormatters.Add(new HtmlFormOutputFormatter());
                    })
              .AddJsonSerialisation()
              .AddExceptionFilters();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseDocumentStoreBuilder()
         //.UseCookiePolicy()
         .UseRouting()
         .UseAuthentication()
         .UseAuthorization()
         .UseStaticFiles(new StaticFileOptions
         {
           FileProvider = new PhysicalFileProvider(
             Path.Combine("/app", "static_content")),
           RequestPath = "/static"
         })
         .UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
