using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using PracticeWeb.Models;
using PracticeWeb.Service.ProductService;
using PracticeWeb.Service.ProviderService;

namespace PracticeWeb.Pages
{
    
    public partial class TestPage
    {    
        [Inject] GetProductService GetProd  {get; set;}
        [Inject] PutProductService PutProd  {get; set;}
        [Inject] DeleteProductService DelProd   {get; set;}
        [Inject] AddProductService AddProd {get; set;}
        [Inject] GetProvToIDService GetProv {get; set;}

    IList<Product>? SelectedProduct { get; set; }
    IList<Product>? products = new List<Product>{}; 

    //для диалогового окна
     protected IEnumerable<Product>? prods;
    protected IEnumerable<Provider>? providers;
    protected override async Task OnInitializedAsync()
    {
        prods = await GetProd.GetProductList();

        foreach(var c in prods){
            providers = await GetProv.GetProvToIdList(c.id);

            if(providers != null){
                c.Provider = providers.ToList<Provider>();
            }
            products.Add(c);
        }
       SelectedProduct = new List<Product>(){products.FirstOrDefault()}; 
    }
    }

}