using AlphacA.Resources.Clubs.Domain;
using AlphacA.Resources.Clubs.Representations;
using Microsoft.Extensions.DependencyInjection;

namespace AlphacA.Resources.Clubs
{
  public static class DependencyInjectionExtensions
  {
    public static IServiceCollection AddClub(this IServiceCollection services)
    {
      return services
        .AddTransient<ClubUriFactory>()
        .AddTransient<ClubRepresentationAdapter>()
        .AddTransient<ClubResourceHandler>();
    }
  }
}