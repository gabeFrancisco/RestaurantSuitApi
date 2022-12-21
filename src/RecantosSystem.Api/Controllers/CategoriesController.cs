using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    // [WorkGroupHeaderAttribute]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoryService.AddAsync(categoryDTO));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.GetAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoryService.UpdateAsync(categoryDto, categoryDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _categoryService.DeleteAsync(id));
        }
    }
}