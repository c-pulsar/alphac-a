using System;
using Microsoft.AspNetCore.Mvc;
using Pulsar.AlphacA.Configuration;

namespace Pulsar.AlphacA.Users
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

    public string MakeCollectionUri()
    {
      return $"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.UserCollection)}";
    }

    public string MakeCreateFormUri()
    {
      return $"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.UserCreateForm)}";
    }

    public string MakeUri(Guid id)
    {
      return $"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.User, new { id })}";
    }
  }
}