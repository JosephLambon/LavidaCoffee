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
        public async Task<IActionResult> Index()
        {
			var upcomingEvents = await _eventRepository.GetAllEventsAsync();
			return View(upcomingEvents);
        }

		public async Task<IActionResult> EventDetails(int id)
		{
			var selectedEvent = await _eventRepository.GetEventByIdAsync(id);
			if (selectedEvent == null)
			{
				return NotFound();
			}
			return View(selectedEvent);
		}
    }
}
