using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace LavidaCoffee.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly LavidaCoffeeDbContext _lavidaCoffeeDbContext;
        private IMemoryCache _memoryCache;
        private const string allEventsCacheKey = "AllEvents";
        private const string upcomingEventsCacheKey = "UpcomingEvents";
        private MemoryCacheEntryOptions cacheEntryOptions =
            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        public EventRepository(LavidaCoffeeDbContext lavidaCoffeeDbContext, IMemoryCache memoryCache)
        {
            _lavidaCoffeeDbContext = lavidaCoffeeDbContext;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            List<Event> allEvents = null;

            if (!_memoryCache.TryGetValue(allEventsCacheKey, out allEvents))
            {
                allEvents = await _lavidaCoffeeDbContext.Events
                    .AsNoTracking()
                    .OrderBy(e => e.EventId)
                    .ToListAsync();
                
                _memoryCache.Set(allEventsCacheKey, allEvents, cacheEntryOptions);
            }
            
            return allEvents;    
        }
        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            List<Event> upcomingEvents = null;

            if (!_memoryCache.TryGetValue(upcomingEventsCacheKey, out upcomingEvents))
            {
                upcomingEvents = await _lavidaCoffeeDbContext.Events
                    .AsNoTracking()
                    .Where(e=> e.Date > DateTime.Now)
                    .OrderBy(e => e.Date)
                    .ToListAsync();
                
                _memoryCache.Set(upcomingEventsCacheKey, upcomingEvents, cacheEntryOptions);
            }

            return upcomingEvents;
        }
        public async Task<Event?> GetEventByIdAsync(int id)
        {
            Event selectedEvent = null;
            
            if (id >= 0)
            {
                string cacheKey = "EventDetails_" + id;
                
                if(!_memoryCache.TryGetValue(cacheKey, out selectedEvent))
                {
                    selectedEvent = await _lavidaCoffeeDbContext
                        .Events
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.EventId == id);

                    _memoryCache.Set(cacheKey, selectedEvent, cacheEntryOptions);
                }
            }
            
            return selectedEvent;
        }

        public async Task CreateEventAsync(Event new_event)
        {
            await _lavidaCoffeeDbContext.Events.AddAsync(new_event);
            _memoryCache.Remove(allEventsCacheKey);
            _memoryCache.Remove(upcomingEventsCacheKey);
            await _lavidaCoffeeDbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Event target_event)
        {
            string cacheKey = "EventDetails_" + target_event.EventId;
            _lavidaCoffeeDbContext.Events.Remove(target_event);
            _memoryCache.Remove(cacheKey);
            _memoryCache.Remove(allEventsCacheKey);
            _memoryCache.Remove(upcomingEventsCacheKey);
            await _lavidaCoffeeDbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event updated_event)
        {
            string cacheKey = "EventDetails_" + updated_event.EventId;
            _lavidaCoffeeDbContext.Events.Update(updated_event);
            _memoryCache.Remove(cacheKey);
            _memoryCache.Remove(allEventsCacheKey);
            _memoryCache.Remove(upcomingEventsCacheKey);
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
