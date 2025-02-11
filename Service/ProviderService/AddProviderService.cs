using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using Test1.Models;


namespace PracticeWeb.Service
{
   public partial class AddProviderService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public AddProviderService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("addProvider");
            this.navigationManager = navigationManager;
        }
        partial void OnGetProviderList(HttpRequestMessage request);
        partial void OnGetProviderListResponse(HttpResponseMessage response);


        public async Task<IEnumerable<Provider>> AddProviderList(string name, double inn, int product_id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"addProvider");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("name", $"{name}");
            queryString.Add("inn", $"{inn}");
            queryString.Add("product_id", $"{product_id}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            OnGetProviderList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetProviderListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Provider>>();
        }
    }
}