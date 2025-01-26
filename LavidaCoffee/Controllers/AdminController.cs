using LavidaCoffee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
			return View(emailRequests);
		}


	}
}
