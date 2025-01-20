using System.IO.Pipelines;
using LavidaCoffee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using SQLitePCL;

namespace LavidaCoffee.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailRequestController : ControllerBase
	{
		private readonly IEmailRequestRepository _emailRequestRepository;

		public EmailRequestController(IEmailRequestRepository emailRequestRepository)
		{
			_emailRequestRepository = emailRequestRepository;
		}
		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult requestsForCurrentPage([FromBody] int page)
		{
			IEnumerable<EmailRequest> requests = new List<EmailRequest>();

			if(int.IsPositive(page))
			{
				requests = _emailRequestRepository.requestsForCurrentPage(page);
			}

			return new JsonResult(requests);
		}
	}
}
