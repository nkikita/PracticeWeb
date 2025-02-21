using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PracticeWeb.Service.ProductService;
using PracticeWeb;
using PracticeWeb.Service.ProviderService;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<ThemeService>();


builder.Services.AddHttpClient("PracticeWeb", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


builder.Services.AddHttpClient("addProducts", client => client.BaseAddress = new Uri("http://localhost:5084/"));
builder.Services.AddScoped<AddProductService>();
builder.Services.AddHttpClient("setProducts", client => client.BaseAddress = new Uri("http://localhost:5084/"));
builder.Services.AddScoped<PutProductService>();
builder.Services.AddHttpClient("getProducts", client => client.BaseAddress = new Uri("http://localhost:5084/"));
builder.Services.AddScoped<GetProductService>();
builder.Services.AddHttpClient("deleteProducts", client => client.BaseAddress = new Uri("http://localhost:5084/"));
builder.Services.AddScoped<DeleteProductService>();



builder.Services.AddHttpClient("getXMLToId", client => client.BaseAddress = new Uri("http://localhost:5084/"));
builder.Services.AddScoped<GetXMLToIdService>();



builder.Services.AddHttpClient("searchProv", client => client.BaseAddress = new Uri("http://localhost:5084/"));
builder.Services.AddScoped<GetProvToIDService>();


await builder.Build().RunAsync();