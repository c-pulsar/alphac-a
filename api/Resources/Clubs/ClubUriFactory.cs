using System;
using Microsoft.AspNetCore.Mvc;
using AlphacA.Configuration;

namespace AlphacA.Resources.Clubs
{
  public class ClubUriFactory
  {
    private readonly IUrlHelper urlHelper;
    private readonly ApiUriConfiguration apiUriConfiguration;

    public ClubUriFactory(IUrlHelper urlHelper, ApiUriConfiguration apiUriConfiguration)
    {
      this.urlHelper = urlHelper;
      this.apiUriConfiguration = apiUriConfiguration;
    }

    public Uri MakeCollection(string search = null)
    {
      return string.IsNullOrWhiteSpace(search)
        ? new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.Collection)}")
        : new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.Collection, new { search })}");
    }

    public Uri MakePlayersCollection(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.ClubPlayers, new { id })}");
    }

    public Uri MakePlayerCreateForm(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.ClubPlayerCreateForm, new { id })}");
    }

    public Uri MakeCreateForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.CreateForm)}");
    }

    public Uri MakeSearchForm()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.SearchForm)}");
    }

    public Uri MakeEditForm(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.EditForm, new { id })}");
    }

    public Uri MakeSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.Schema)}");
    }

    public Uri MakeEditFormSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.EditFormSchema)}");
    }

    public Uri MakeCreateFormSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.CreateFormSchema)}");
    }

    public Uri MakeSearchSchema()
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.SearchSchema)}");
    }

    public Uri Make(string id)
    {
      return new Uri($"{apiUriConfiguration.BaseUri}{urlHelper.RouteUrl(ClubRoutes.Club, new { id })}");
    }
  }
}