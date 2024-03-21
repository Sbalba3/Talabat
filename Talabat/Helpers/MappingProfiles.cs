using AutoMapper;
using Talabat.Core.Models;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.ImageUrl,o=>o.MapFrom<ProductImageUrlResolver>());
        }
    }
}
