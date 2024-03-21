using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        
        public int ProductBrandId { get; set; }// Foreign Key
        public ProductBrand ProductBrand { get; set; } // relationship One
        public int ProductTypeId { get; set; } // Foreign Key
        public ProductType ProductType { get; set; } // relationship One

    }
}
