using LavidaCoffee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LavidaCoffee.Controllers
{
	public class AdminController : Controller
	{
		private readonly IEmailRequestRepository _emailRequestRepository;

		public AdminController(IEmailRequestRepository emailRequestRepository)
		{
			_emailRequestRepository = emailRequestRepository;
		}

		

		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			List<EmailRequest> emailRequests = _emailRequestRepository.AllEmailRequests();
			emailRequests = emailRequests;
			return View(emailRequests);
		}


	}
}
