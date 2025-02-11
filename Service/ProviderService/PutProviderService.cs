using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using Test1.Models;

namespace PracticeWeb.Service.ProviderService
{
    public partial class PutProviderService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public PutProviderService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("setProvider");
            this.navigationManager = navigationManager;
        }

        partial void OnPutProviderList(HttpRequestMessage request);
        partial void OnPutProviderListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Provider>> PutRankList(string name,int id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"setProvider");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add($"id", $"{id}");
            queryString.Add($"name", $"{name}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Put, uri);

            OnPutProviderList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnPutProviderListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Provider>>();
        }
    }
}