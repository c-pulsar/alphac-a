using Microsoft.AspNetCore.Mvc;
using AlphacA.Configuration;
using System;

namespace AlphacA.Resources.Root
{
  public class RootUriFactory
  {
    private readonly IUrlHelper urlHelper;
    private readonly ApiUriConfiguration apiUriConfiguration;

    public RootUriFactory(IUrlHelper urlHelper, ApiUriConfiguration apiUriConfiguration)
    {
      this.urlHelper = urlHelper;
      this.apiUriConfiguration = apiUriConfiguration;
    }
    public Uri MakeRootUri()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(RootRoutes.Root)}");
    }
  }
}