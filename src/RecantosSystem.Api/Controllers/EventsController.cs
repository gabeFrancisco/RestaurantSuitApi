using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _eventService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventDTO eventDto)
        {
            return Ok(await _eventService.AddAsync(eventDto));
        }
    }
}