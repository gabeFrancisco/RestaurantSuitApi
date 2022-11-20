using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
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
			return Ok(await _tableService.GetAllAsync());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			return Ok(await _tableService.GetAsync(id));
		}

		[HttpPost]
		[Authorize(Roles = "ADM")]
		public async Task<IActionResult> Post([FromBody] TableDTO tableDto)
		{
			return Ok(await _tableService.AddAsync(tableDto));
		}

		[Authorize(Roles = "ADM")]
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] TableDTO tableDto)
		{
			return Ok(await _tableService.UpdateAsync(tableDto, tableDto.Id));
		}

		[Authorize(Roles = "ADM")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int tableId)
		{
			return Ok(await _tableService.DeleteAsync(tableId));
		}
	}
}