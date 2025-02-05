using LavidaCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffee.Controllers
{
    public class Find_UsController : Controller
    {
		private readonly IEventRepository _eventRepository;
		public Find_UsController(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}
        public IActionResult Index()
        {
			IEnumerable<Event> upcomingEvents = _eventRepository.AllEvents.Where(e => e.Date > DateTime.Today).OrderBy(e=>e.Date);
			return View(upcomingEvents);
        }

		public IActionResult EventDetails(int id)
		{
			var selectedEvent = _eventRepository.GetEventById(id);
			if (selectedEvent == null)
			{
				return NotFound();
			}
			return View(selectedEvent);
		}
    }
}
