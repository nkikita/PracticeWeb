using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Numerics;

namespace PracticeWeb.Models
{
    public class Provider
    {
        public int id {get; set;}
        public string? name {get; set;}
        public double inn {get; set;}
        public int product_id {get;set;}
    }
}