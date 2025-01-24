using Microsoft.EntityFrameworkCore;

namespace LavidaCoffee.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly LavidaCoffeeDbContext _lavidaCoffeeDbContext;
        public List<Event> Events { get; set; } = default!;
        public EventRepository(LavidaCoffeeDbContext lavidaCoffeeDbContext)
        {
            _lavidaCoffeeDbContext = lavidaCoffeeDbContext;
        }

        public List<Event> GetEvents()
        {
            var eventList = _lavidaCoffeeDbContext.Events.Include(e => e.EventId).ToList();
            return eventList;
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
    }
}
