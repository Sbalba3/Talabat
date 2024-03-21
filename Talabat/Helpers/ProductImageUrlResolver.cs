﻿using AutoMapper;
using Talabat.Core.Models;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class ProductImageUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.ImageUrl}";
            }
            return string.Empty;
        }
    }
}
