
namespace LavidaCoffee.Models
{
    public interface IEventRepository
    {
        List<Event> GetEvents();
        void CreateEvent(Event new_event);
        void DeleteEvent(Event target_event);
    } 
}
