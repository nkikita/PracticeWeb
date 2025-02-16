using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Http;
using PracticeWeb.Models;
using System.Net.Http.Json;

namespace PracticeWeb.Service.ProductService
{
    public partial class GetProductService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public GetProductService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("getProducts");
            this.navigationManager = navigationManager;
        }

        partial void OnGetProductList(HttpRequestMessage request);
        partial void OnGetProductListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Product>> GetProductList()
        {
            var uri = new Uri(httpClient.BaseAddress, $"getProducts");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProductList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetProductListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }
    }
}