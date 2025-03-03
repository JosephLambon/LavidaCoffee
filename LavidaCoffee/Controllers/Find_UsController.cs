using LavidaCoffee.Models;
using LavidaCoffee.ViewModels;
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
	        Find_UsIndexViewModel model = new()
	        {
		        Events = (await _eventRepository.GetUpcomingEventsAsync()).ToList()
	        };
			return View(model);
        }

		public async Task<IActionResult> EventDetails(int id)
		{
			Find_UsEventDetailsViewModel model = new()
			{
				Event = await _eventRepository.GetEventByIdAsync(id)
			};
			if (model.Event == null)
			{
				return NotFound();
			}
			return View(model);
		}
    }
}
