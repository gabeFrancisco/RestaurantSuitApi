using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiConventionType(typeof(DefaultApiConventions))]
	[Authorize(AuthenticationSchemes = "Bearer")]
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
			try
			{
				return Ok(await _categoryService.GetAllAsync());
			}
			catch
			{
				
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while fetching all categories."
				);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				return Ok(await _categoryService.AddAsync(categoryDTO));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while creating the category"
				);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				return Ok(await _categoryService.GetAsync(id));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while reading the category"
				);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CategoryDTO categoryDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				return Ok(await _categoryService.UpdateAsync(categoryDto, categoryDto.Id));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while updating the category"
				);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				return Ok(await _categoryService.DeleteAsync(id));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while removing the category"
				);
			}
		}
	}
}