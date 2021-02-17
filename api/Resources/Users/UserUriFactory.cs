using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Configuration;

namespace AlphacA.Resources.Users
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

    public Uri MakeCollectionUri()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.UserCollection)}");
    }

    public Uri MakeCreateFormUri()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.UserCreateForm)}");
    }

    public Uri MakeUri(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.User, new { id })}");
    }
  }
}