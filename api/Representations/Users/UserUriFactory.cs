using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Configuration;

namespace Pulsar.AlphacA.Representations.Users
{
  public class UserUriFactory
  {
    private readonly IUrlHelper urlHelper;
    private readonly ApiUriConfiguration apiUriConfiguration;

    public UserUriFactory(IUrlHelper urlHelper, ApiUriConfiguration apiUriConfiguration)
    {
      this.urlHelper = urlHelper;
      this.apiUriConfiguration = apiUriConfiguration;
    }

    public string MakeUserCollectionUri()
    {
      return $"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.GetUsers)}";
    }
  }
}