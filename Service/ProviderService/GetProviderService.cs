using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Http;
using Test1.Models;
using System.Net.Http.Json;


namespace PracticeWeb.Service.ProviderService
{
    public partial class GetProviderService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public GetProviderService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("getProvider");
            this.navigationManager = navigationManager;
        }

        partial void OnGetProviderList(HttpRequestMessage request);
        partial void OnGetProviderListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Provider>> GetProductList()
        {
            var uri = new Uri(httpClient.BaseAddress, $"getProvider");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProviderList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetProviderListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Provider>>();
        }
    }
}
