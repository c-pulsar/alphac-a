using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AlphacA.Configuration;
using AlphacA.Representations;
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
              .AddJsonSchemaGeneration()
              .AddAuth()
              .AddCore()
              .AddExceptions()
              .AddDocumentStore()
              .AddRoot()
              .AddUser()
              .AddCors(o => o.AddPolicy("DefaultPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("location")))
              .AddControllers()
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
         .UseCookiePolicy()
         .UseRouting()
         .UseCors("DefaultPolicy")
         .UseAuthentication()
         .UseAuthorization()
         .UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
