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
		public IActionResult AddEvent(DateTime Date, string Title, string ShortDescription, string LongDescription, string Address, string ImageUrl, string ThumnailUrl)
		{
			Event newEvent = new Event { Address = Address, Date = Date, Title = Title, ShortDescription = ShortDescription, LongDescription = LongDescription, ImageUrl = ImageUrl, ThumbnailUrl = ThumnailUrl };
			_eventRepository.CreateEvent(newEvent);
			return RedirectToAction("Index");
		}
	}
}