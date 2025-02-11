using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Numerics;

namespace Test1.Models
{
    public class Provider
    {
        [JsonPropertyName("IdProvider")] 
        public int id {get; set;}
        [JsonPropertyName("NameProvider")]
        public string? name {get; set;}
        [JsonPropertyName("InnProvider")] 
        public double inn {get; set;}
        //[JsonIgnore]
        public int product_id {get;set;}
    }
}