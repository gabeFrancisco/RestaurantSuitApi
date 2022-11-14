using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
	[Route("api/[controller]")]
	public class CategoriesController : ControllerBase
	{
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
		public ActionResult Get()
		{
            return Ok(_categoryService.GetAllCategories());
		}
	}
}