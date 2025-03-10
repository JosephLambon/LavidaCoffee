﻿using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LavidaCoffee.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly LavidaCoffeeDbContext _lavidaCoffeeDbContext;
        public EventRepository(LavidaCoffeeDbContext lavidaCoffeeDbContext)
        {
            _lavidaCoffeeDbContext = lavidaCoffeeDbContext;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _lavidaCoffeeDbContext.Events
                .AsNoTracking()
                .OrderBy(e => e.EventId)
                .ToListAsync();   
        }
        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            Console.WriteLine("DateTime.Now: " + DateTime.Today);

            return await _lavidaCoffeeDbContext.Events
                .AsNoTracking()
                .Where(e=> e.Date > DateTime.Now)
                .OrderBy(e => e.Date)
                .ToListAsync();   
        }
        public async Task<Event?> GetEventByIdAsync(int id)
        {
            if (id >= 0)
            {
                var selectedEvent = await _lavidaCoffeeDbContext
                    .Events
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EventId == id);

                if (selectedEvent != null)
                {
                    return selectedEvent;
                }
            }

            return null;
        }

        public async Task CreateEventAsync(Event new_event)
        {
            await _lavidaCoffeeDbContext.Events.AddAsync(new_event);
            await _lavidaCoffeeDbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Event target_event)
        {
            _lavidaCoffeeDbContext.Events.Remove(target_event);
            await _lavidaCoffeeDbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event updated_event)
        {
            _lavidaCoffeeDbContext.Events.Update(updated_event);
            await _lavidaCoffeeDbContext.SaveChangesAsync();
        }

        public async Task<int> GetAllEventsCountAsync()
        {
            return await _lavidaCoffeeDbContext.Events.AsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsPagedAsync(int? pageNumber, int pageSize)
        {
            IQueryable<Event> events = from e in _lavidaCoffeeDbContext.Events
                select e;

            pageNumber ??= 1;

            events = events.Skip((pageNumber.Value - 1) * pageSize)
                .Take(pageSize);

            return await events.AsNoTracking().ToListAsync();

        }
        public async Task<IEnumerable<Event>> GetEventsPagedAndSortedAsync(string sortBy, int? pageNumber, int pageSize)
        {
            IQueryable<Event> events = from e in _lavidaCoffeeDbContext.Events select e;

            switch(sortBy)
            {
                case "eventid_descending":
                    events = events.OrderByDescending(e => e.EventId);
                    break;
                case "eventid":
					events = events.OrderBy(e=>e.EventId);
					break;
				case "title_descending":
					events = events.OrderByDescending(e => e.Title);
					break;
				case "title":
					events = events.OrderBy(e => e.Title);
					break;
				case "date_descending":
					events = events.OrderByDescending(e => e.Date);
					break;
				case "date":
					events = events.OrderBy(e => e.Date);
					break;
                default:
                    events = events.OrderBy(e => e.EventId);
                    break;
			}

            pageNumber ??= 1;

            events = events.Skip((pageNumber.Value - 1) * pageSize).Take(pageSize);

            return await events.AsNoTracking().ToListAsync();
        }
    }
}
