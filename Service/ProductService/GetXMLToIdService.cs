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
    public partial class GetXMLToIdService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager? navigationManager;

        public GetXMLToIdService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("getXMLToId");
            this.navigationManager = navigationManager;
        }

        partial void OnGetProductXMLList(HttpRequestMessage request);
        partial void OnGetProductXMLListResponse(HttpResponseMessage response);

        public async Task<IEnumerable<Product>> GetProductXMLList(int id)
        {
            var uri = new Uri(httpClient.BaseAddress, $"getXMLToId");

            var uriBuilder = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

            queryString.Add("id", $"{id}");

            uriBuilder.Query = queryString.ToString();
            uri = uriBuilder.Uri;

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProductXMLList(request);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            OnGetProductXMLListResponse(response);

            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }
    }
}