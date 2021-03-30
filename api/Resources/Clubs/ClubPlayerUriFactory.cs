using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Configuration;

namespace AlphacA.Resources.Clubs
{
  public class ClubPlayerUriFactory
  {
    private readonly IUrlHelper urlHelper;
    private readonly ApiUriConfiguration apiUriConfiguration;

    public ClubPlayerUriFactory(IUrlHelper urlHelper, ApiUriConfiguration apiUriConfiguration)
    {
      this.urlHelper = urlHelper;
      this.apiUriConfiguration = apiUriConfiguration;
    }

    public Uri MakeCollection(string clubId)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubPlayerRoutes.Collection, new { id = clubId })}");
    }

    public Uri MakeCreateForm(string clubId)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubPlayerRoutes.CreateForm, new { id = clubId })}");
    }
  }
}