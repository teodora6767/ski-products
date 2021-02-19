using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiProductsWebApp.Models
{
    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public int Order_id { get; set; }

    }
}