using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using PracticeWeb.Models;

namespace PracticeWeb.Service.ProductService
{
    public partial class PutProductService
    {
         private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public PutProductService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("setProducts");
            this.navigationManager = navigationManager;
        }

        partial void OnPutProductList(HttpRequestMessage request);
        partial void OnPutProductListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Product>> PutRankList(string name,int id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"setProducts");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add($"id", $"{id}");
            queryString.Add($"name", $"{name}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Put, uri);

            OnPutProductList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnPutProductListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }
    }
}