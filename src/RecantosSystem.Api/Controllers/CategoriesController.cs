using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
	[ApiController]
	[ApiConventionType(typeof(DefaultApiConventions))]
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			return Ok(await _categoryService.GetAllCategoriesAsync());
		}
	}
}