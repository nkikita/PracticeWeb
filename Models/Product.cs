using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Test1.Models
{
    public class Product
    {
        [JsonPropertyName("IdProduct")] 
        public int id {get; set;}
        [JsonPropertyName("NameProduct")]
        public string? name {get; set;}
        public int count {get; set;}
    }
}