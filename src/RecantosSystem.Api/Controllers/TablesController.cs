using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Services.Logging;

namespace RecantosSystem.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiConventionType(typeof(DefaultApiConventions))]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class TablesController : ControllerBase
	{
		private readonly ITableService _tableService;
		private readonly LogService _logService;
		public TablesController(ITableService tableService,
								LogService logService)
		{
			_tableService = tableService;
			_logService = logService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _tableService.GetAllAsync());
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while fetching all tables."
				);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				return Ok(await _tableService.GetAsync(id));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while fetching all tables."
				);
			}
		}
	}
}