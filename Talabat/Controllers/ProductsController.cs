using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IRepository;
using Talabat.Core.Models;
using Talabat.Core.Specifications;
using Talabat.Dtos;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IGenericRepo<Product> _productRepo;
        private readonly IGenericRepo<ProductBrand> _brandRepo;
        private readonly IGenericRepo<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepo<Product> productRepo,IGenericRepo<ProductBrand> brandRepo ,IGenericRepo<ProductType> typeRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecPrams specPrams)
        {

            var spec = new ProductWithBrandAndTypeSpec(specPrams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductWithFiltrationForCountSpecification(specPrams);
            var count=await _productRepo.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(specPrams.PageIndex, specPrams.PageSize, count,data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpec(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var productMapped = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productMapped);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
          var brands=await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var types=await _typeRepo.GetAllAsync(); 
            return Ok(types);
        }

    }
}
