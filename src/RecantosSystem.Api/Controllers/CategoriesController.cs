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

		/// <summary>
		/// Return all categories from the database
		/// </summary>
		/// <returns>A list object containing categories</returns>
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

		/// <summary>
		/// Create a new Category into the database
		/// </summary>
		/// <param name="categoryDTO">Category data transfer object</param>
		/// <returns>If the operation is done succesfully, it returns the category dto</returns>
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

		/// <summary>
		/// Read a category from the database
		/// </summary>
		/// <param name="id">The category id</param>
		/// <returns>Return the a category dto.</returns>
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

		/// <summary>
		/// Updates a existing category in the database
		/// </summary>
		/// <param name="categoryDto">The new category data</param>
		/// <returns>Return the updated category</returns>
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

		/// <summary>
		/// Removes a category from the database
		/// </summary>
		/// <param name="id">The category's id that will be removed</param>
		/// <returns>Returns true if the category was deleted</returns>
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