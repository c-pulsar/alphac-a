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

    public Uri MakeCollection(string search = null)
    {
      return string.IsNullOrWhiteSpace(search)
        ? new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.Collection)}")
        : new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.Collection, new { search })}");
    }

    public Uri MakeCreateForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.CreateForm)}");
    }

    public Uri MakeSearchForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.SearchForm)}");
    }

    public Uri MakeEditForm(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.EditForm, new { id })}");
    }

    public Uri MakeSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.Schema)}");
    }

    public Uri MakeEditFormSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.EditFormSchema)}");
    }

    public Uri MakeCreateFormSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.CreateFormSchema)}");
    }

    public Uri MakeSearchSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.SearchSchema)}");
    }

    public Uri Make(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(UserRoutes.User, new { id })}");
    }
  }
}