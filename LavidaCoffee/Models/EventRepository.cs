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

        public IEnumerable<Event> AllEvents
        {
            get
            {
                var eventList = _lavidaCoffeeDbContext.Events;
                return eventList;
            }
        }

        public Event? GetEventById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var selectedEvent = _lavidaCoffeeDbContext
                .Events
                .FirstOrDefault(e => e.EventId == id);

            if (selectedEvent != null)
            {
                return selectedEvent;
            }

            throw new KeyNotFoundException("Event not found.");
        }

        public void CreateEvent(Event new_event)
        {
            _lavidaCoffeeDbContext.Events.Add(new_event);
            _lavidaCoffeeDbContext.SaveChanges();
        }

        public void DeleteEvent(Event target_event)
        {
            _lavidaCoffeeDbContext.Events.Remove(target_event);
            _lavidaCoffeeDbContext.SaveChanges();
        }

        public void UpdateEvent(Event updated_event)
        {
            _lavidaCoffeeDbContext.Events.Update(updated_event);
            _lavidaCoffeeDbContext.SaveChanges();
        }
    }
}
