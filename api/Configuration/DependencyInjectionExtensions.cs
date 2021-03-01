using System;
using System.Collections;
using AlphacA.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Configuration
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
      var environmentVariables = Environment.GetEnvironmentVariables();

      return services
      .AddSingleton(_ => new DocumentStorageConfig
      {
        DocumentStoreName = environmentVariables.FindOrThrow("DOCUMENT_STORE_NAME"),
        DocumentStoreServerUrl = environmentVariables.FindOrThrow("DOCUMENT_STORE_SERVER_URL"),
      })
      .AddSingleton(_ => new ApiUriConfiguration
      {
        BaseUri = environmentVariables.FindOrThrow("API_BASE_URI")
      })
      .AddSingleton(_ => new AuthConfig
      {
        Authority = environmentVariables.FindOrThrow("AUTH_AUTHORITY"),
        Audience = environmentVariables.FindOrThrow("AUTH_AUDIENCE"),
        ClientId = environmentVariables.FindOrThrow("AUTH_CLIENT_ID"),
        ClientSecret = environmentVariables.FindOrThrow("AUTH_CLIENT_SECRET")
      });
    }

    private static string FindOrThrow(this IDictionary environmentVariables, string variableName)
    {
      var result = environmentVariables[variableName] as string;
      if (string.IsNullOrWhiteSpace(result))
      {
        throw new InvalidOperationException($"Environment variable {variableName} not defined");
      }

      return result;
    }
  }
}