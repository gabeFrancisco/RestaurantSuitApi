using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EventService(AppDbContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDTO>> GetAllAsync()
        {
            var events = await _context.Events.ToListAsync();
            return _mapper.Map<List<EventDTO>>(events);
        }

        public Task<EventDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<EventDTO> AddAsync(EventDTO eventDto)
        {
            if (eventDto == null)
            {
                throw new NullReferenceException("Data transfer object is null");
            }

            var newEvent = _mapper.Map<EventDTO, Event>(eventDto);
            newEvent.EventDate = DateTime.Parse($"{eventDto.Date} {eventDto.Hour}:{eventDto.Minutes}:00");
            newEvent.CreatedAt = DateTime.Now;

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return eventDto;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<EventDTO> UpdateAsync(EventDTO entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}