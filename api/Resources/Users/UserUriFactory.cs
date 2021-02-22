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

    public Uri MakeCollection()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.UserCollection)}");
    }

    public Uri MakeCreateForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.CreateForm)}");
    }

    public Uri MakeEditForm(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.EditForm, new { id })}");
    }

    public Uri Make(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.User, new { id })}");
    }
  }
}