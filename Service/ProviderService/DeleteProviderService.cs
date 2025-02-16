using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using PracticeWeb.Models;


namespace PracticeWeb.Service.ProviderService
{
    public partial class DeleteProviderService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public DeleteProviderService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("deleteProvider");
            this.navigationManager = navigationManager;
        }

        partial void OnDeleteProviderList(HttpRequestMessage request);
        partial void OnDeleteProviderListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Provider>> DeleteRankList(string id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"deleteProvider");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteProviderList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnDeleteProviderListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Provider>>();
        }
    }
}