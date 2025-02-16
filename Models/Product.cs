using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PracticeWeb.Models
{
    public class Product
    {
        public int id {get; set;}
        public string? name {get; set;}
        public int count {get; set;}
        public List<Provider> Provider { get; set; }
    }
}