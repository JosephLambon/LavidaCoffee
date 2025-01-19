using System.IO.Pipelines;
using LavidaCoffee.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace LavidaCoffee.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IEmailRequestRepository _emailRequestRepository;

		public AdminController(IEmailRequestRepository emailRequestRepository)
		{
			_emailRequestRepository = emailRequestRepository;
		}

		[HttpGet("{page}")]
		public IActionResult Get(int page = 1)
		{
			List<EmailRequest> emailRequests = _emailRequestRepository.AllEmailRequests();
			int total = emailRequests.Count;
			int pageSize = 10;
			var numberPages = (int)Math.Ceiling((decimal)total / pageSize);
			var requestsPerPage = emailRequests
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			return Ok(requestsPerPage);
		}

		[HttpPost]
		public IActionResult requestsForCurrentPage([FromBody] int page)
		{
			IEnumerable<EmailRequest> requests = new List<EmailRequest>();

			if(int.IsPositive(page))
			{
				requests = _emailRequestRepository.requestsForCurrentPage(page);
			}

			foreach(var req in requests)
			{
				Console.WriteLine("Request #" + req.EmailRequestId);
				Console.WriteLine("email: " + req.Email);
			};

			return new JsonResult(requests);
		}
	}
}
