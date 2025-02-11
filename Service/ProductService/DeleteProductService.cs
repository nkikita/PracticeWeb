using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using Test1.Models;

namespace PracticeWeb.Service.ProductService
{
    public partial class DeleteProductService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public DeleteProductService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("deleteProducts");
            this.navigationManager = navigationManager;
        }

        partial void OnDeleteProductList(HttpRequestMessage request);
        partial void OnDeleteProductListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Product>> DeleteRankList(string id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"deleteProducts");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteProductList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnDeleteProductListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }
    }
}