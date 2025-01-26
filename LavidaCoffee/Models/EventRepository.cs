using Microsoft.EntityFrameworkCore;

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
