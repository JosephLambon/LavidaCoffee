
namespace LavidaCoffee.Models
{
    public interface IEventRepository
    {
        IEnumerable<Event> AllEvents { get; }
        void CreateEvent(Event new_event);
        void DeleteEvent(Event target_event);
    } 
}
