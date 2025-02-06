
namespace LavidaCoffee.Models
{
    public interface IEventRepository
    {
        IEnumerable<Event> AllEvents { get; }
        Event? GetEventById(int id);
        void CreateEvent(Event new_event);
        void DeleteEvent(Event target_event);
        void UpdateEvent(Event updated_event);
    } 
}
