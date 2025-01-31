using LavidaCoffee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LavidaCoffee.ViewModels;

namespace LavidaCoffee.Controllers
{
	public class AdminController : Controller
	{
		private readonly IEmailRequestRepository _emailRequestRepository;
		private readonly IEventRepository _eventRepository;

		public AdminController(IEmailRequestRepository emailRequestRepository, IEventRepository eventRepository)
		{
			_emailRequestRepository = emailRequestRepository;
			_eventRepository = eventRepository;
		}


		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			List<EmailRequest> emailRequests = _emailRequestRepository.AllEmailRequests();
			IEnumerable<Event> upcomingEvents = _eventRepository.AllEvents.Where(e => e.Date > DateTime.Today);

			return View(new AdminViewModel(upcomingEvents, emailRequests));
		}

		[HttpPost]
		public IActionResult AddEvent([FromForm] AdminViewModel model)
		{
			if (ModelState.IsValid)
			{
				Event newEvent = new Event
				{
					Address = model.Address,
					Date = model.Date,
					Title = model.Title,
					ShortDescription = model.ShortDescription,
					LongDescription = model.LongDescription,
					ImageUrl = model.ImageUrl,
					ThumbnailUrl = model.ThumbnailUrl
				};
				_eventRepository.CreateEvent(newEvent);
				return RedirectToAction("Index");
			}
			return View(model);
		}
	}
}