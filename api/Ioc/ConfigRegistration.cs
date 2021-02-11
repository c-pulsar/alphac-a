using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Pulsar.AlphacA.Configuration;

namespace Pulsar.AlphacA.Ioc
{
  public static class ConfigRegistration
  {
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
      var environmentVariables = Environment.GetEnvironmentVariables();

      services.AddSingleton(_ => new DatabaseConfig
      {
        DatabaseName = environmentVariables.FindOrThrow("DATABASE_NAME"),
        DatabaseServerUrl = environmentVariables.FindOrThrow("DATABASE_SERVER_URL"),
      });

      return services;
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