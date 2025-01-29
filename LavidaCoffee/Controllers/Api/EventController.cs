using System.Text.Json.Nodes;
using LavidaCoffee.Models;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffee.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly IEventRepository _eventRepository;
		public EventController(IEventRepository eventRepository) {
			_eventRepository = eventRepository;
		}
		[HttpGet]
		public IActionResult AllEvents()
		{
			var allEvents = _eventRepository.AllEvents.Where(e => e.Title != null);
			return Ok(allEvents);
		}
		[HttpPost]
		public IActionResult UpcomingEvents()
		{
			IEnumerable<Event> allEvents = _eventRepository.AllEvents.Where(e => e.Date > DateTime.Now);
			return new JsonResult(allEvents);
		}
	}
}
