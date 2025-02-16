
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using PracticeWeb.Models;

namespace PracticeWeb.Service.ProviderService
{
    public partial class GetProvToIDService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public GetProvToIDService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("searchProv");
            this.navigationManager = navigationManager;
        }

        partial void OnGetProvToIDList(HttpRequestMessage request);
        partial void OnGetProvToIDListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Provider>> GetProvToIdList(int id)
        {
           var uri = new Uri(httpClient.BaseAddress, $"searchProv");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("Id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProvToIDList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetProvToIDListResponse(response);


            return await response.Content.ReadFromJsonAsync<IEnumerable<Provider>>();
        }
    }
}