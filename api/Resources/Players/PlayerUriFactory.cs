using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Configuration;

namespace AlphacA.Resources.Players
{
  public class PlayerUriFactory
  {
    private readonly IUrlHelper urlHelper;
    private readonly ApiUriConfiguration apiUriConfiguration;

    public PlayerUriFactory(IUrlHelper urlHelper, ApiUriConfiguration apiUriConfiguration)
    {
      this.urlHelper = urlHelper;
      this.apiUriConfiguration = apiUriConfiguration;
    }

    public Uri MakeCollection(string search = null)
    {
      return string.IsNullOrWhiteSpace(search)
        ? new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.Collection)}")
        : new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.Collection, new { search })}");
    }

    public Uri MakeCreateForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.CreateForm)}");
    }

    public Uri MakeSearchForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.SearchForm)}");
    }

    public Uri MakeEditForm(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.EditForm, new { id })}");
    }

    public Uri MakeSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.Schema)}");
    }

    public Uri MakeEditFormSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.EditFormSchema)}");
    }

    public Uri MakeCreateFormSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.CreateFormSchema)}");
    }

    public Uri MakeSearchSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.SearchSchema)}");
    }

    public Uri Make(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(PlayerRoutes.Player, new { id })}");
    }
  }
}