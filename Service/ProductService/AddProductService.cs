using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Web;
using PracticeWeb.Models;

namespace PracticeWeb.Service.ProductService
{
    public partial class AddProductService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public AddProductService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("addProducts");
            this.navigationManager = navigationManager;
        }
        partial void OnGetProductList(HttpRequestMessage request);
        partial void OnGetProductListResponse(HttpResponseMessage response);


        public async Task<IEnumerable<Product>> AddProductList(string name, int count)
        {
            var uri = new Uri(httpClient.BaseAddress, $"addProducts");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("name", $"{name}");
            queryString.Add("count", $"{count}");


            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            OnGetProductList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetProductListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }
    }
}