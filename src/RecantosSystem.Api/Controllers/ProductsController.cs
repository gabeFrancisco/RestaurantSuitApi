using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Filters;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [WorkGroupHeaderAttribute]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _productService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _productService.AddAsync(productDto));
        }
    }
}