using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // [Authorize(Roles = "ADM")]
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
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _tableService.DeleteAsync(id));
        }

        [HttpGet("isBusy")]
        public async Task<IActionResult> SetBusyState(
            [FromQuery(Name = "tableId")] int tableId,
            [FromQuery(Name = "state")] bool state,
            [FromQuery(Name = "confirm")] bool confirm)
        {
            // if (!result)
            // {
            //     return Ok(
            //         // new
            //         // {
            //         // 	hasOrderSheet = true,
            //         // 	message = "This table is attached to an existing order sheet."
            //         // 	+ "Are you sure you want to change state?"
            //         // }
            //         false
            // 	);
            // }
            // else
            // {
            //     return Ok(true);
            // }

            return Ok(await _tableService.SetIsBusy(tableId, state, confirm));
        }
    }
}