using System.Text.Json.Nodes;
using LavidaCoffee.Models;
using LavidaCoffee.Models.Utilities;
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
				IEnumerable<Event> upcomingEvents = await _eventRepository.GetUpcomingEventsAsync();
				return new JsonResult(upcomingEvents);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		private int pageSize = 10;
		[HttpGet("getpaged/{pageNumber}")]
		public async Task<IActionResult> IndexPaging(int? pageNumber)
		{
			var events = await _eventRepository.GetEventsPagedAsync(pageNumber, pageSize);
			pageNumber ??= 1;
			var count = await _eventRepository.GetAllEventsCountAsync();
			return new JsonResult(new PagedList<Event>(events.ToList(), count, pageNumber.Value, pageSize));
		}
	}
}
