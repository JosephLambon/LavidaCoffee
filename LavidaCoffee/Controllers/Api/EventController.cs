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
		public async Task<IActionResult> AllEvents()
		{
			try
			{
				var allEvents = await _eventRepository.GetAllEventsAsync();
				return Ok(allEvents);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpPost]
		public async Task<IActionResult> UpcomingEvents()
		{
			try
			{
				IEnumerable<Event> allEvents = await _eventRepository.GetUpcomingEventsAsync();
				return new JsonResult(allEvents);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
			
		}
	}
}
