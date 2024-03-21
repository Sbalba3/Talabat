using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltrationForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltrationForCountSpecification(ProductSpecPrams specPrams)
             : base(P =>
                   (string.IsNullOrEmpty(specPrams.Search) || P.Name.ToLower().Contains(specPrams.Search)) &&
                   (!specPrams.TypeId.HasValue || P.ProductTypeId == specPrams.TypeId) &&
                   (!specPrams.BrandId.HasValue || P.ProductBrandId == specPrams.BrandId)

            )
        {
            
        }

    }
}
