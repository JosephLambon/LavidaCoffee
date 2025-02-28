
namespace LavidaCoffee.Models
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task CreateEventAsync(Event new_event);
        Task DeleteEventAsync(Event target_event);
        Task UpdateEventAsync(Event updated_event);
    } 
}
