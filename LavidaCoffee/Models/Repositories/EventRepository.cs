using System.Net;
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
            return await _lavidaCoffeeDbContext.Events
                .AsNoTracking()
                .Where(e=> e.Date > DateTime.Today)
                .OrderBy(e => e.EventId)
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
    }
}
