
namespace LavidaCoffee.Models
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        void CreateEvent(Event new_event);
        void DeleteEvent(Event target_event);
        void UpdateEvent(Event updated_event);
    } 
}
