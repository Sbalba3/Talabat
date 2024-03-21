using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpec:BaseSpecification<Product>
    {
        // for get all product
        public ProductWithBrandAndTypeSpec(ProductSpecPrams specPrams )
            :base(P=>
                   (string.IsNullOrEmpty(specPrams.Search)|| P.Name.ToLower().Contains(specPrams.Search))&&
                   (!specPrams.TypeId.HasValue || P.ProductTypeId==specPrams.TypeId)&&
                   (!specPrams.BrandId.HasValue ||P.ProductBrandId==specPrams.BrandId)
            
            )
        {
           
            Includes.Add(P => P.ProductType);
            Includes.Add(P=>P.ProductBrand);

            if (!string.IsNullOrEmpty(specPrams.Sort))
            {
                switch (specPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default :
                        AddOrderBy(P => P.Name);
                        break;
                        
                    

                }
            }
            ApplyPagenation(specPrams.PageSize*(specPrams.PageIndex-1),specPrams.PageSize);
        }
        //for get by id 
        public ProductWithBrandAndTypeSpec(int id):base(P=>P.Id==id) 
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P=> P.ProductBrand);
        }
    }
}
