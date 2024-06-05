using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Dtos;

using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_specs;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenaricRepository<Product> productsRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenaricRepository<Product> productsRepo, IMapper mapper)
        {
            this.productsRepo = productsRepo;
            this.mapper = mapper;
        }

        // / api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await productsRepo.GetAllWithSpecAsync(spec);

            ///JsonResult result = new JsonResult(products);
            ///OkResult result = new OkResult(products);

            return Ok(mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var products = await productsRepo.GetWithSpecAsync(spec);

            

            return Ok(mapper.Map<Product, ProductToReturnDto>(products)); // 200 
        }

    }
}
