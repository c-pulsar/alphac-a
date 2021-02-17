using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Configuration;

namespace Pulsar.AlphacA.Resources
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
    public string MakeRootUri()
    {
      return $"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(RootRoutes.GetRoot)}";
    }
  }
}