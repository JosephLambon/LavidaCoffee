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
	public class EmailController : ControllerBase
	{
		private readonly IEmailRepository _emailRepository;

		public EmailController(IEmailRepository emailRepository)
		{
			_emailRepository = emailRepository;
		}
		// [Authorize(Roles = "Admin")]
		[HttpGet("{page}")]
		public async Task<IActionResult> Get(int page = 1)
		{
			IEnumerable<Email> emailRequests = await _emailRepository.GetAllEmailRequestsAsync();
			int total = emailRequests.Count();
			int pageSize = 10;
			var numberPages = (int)Math.Ceiling((decimal)total / pageSize);
			var requestsPerPage = emailRequests
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			return Ok(requestsPerPage);
		}

		// [Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult requestsForCurrentPage([FromBody] int page)
		{
			IEnumerable<Email> requests = new List<Email>();

			if(int.IsPositive(page))
			{
				requests = _emailRepository.requestsForCurrentPage(page);
			}

			return new JsonResult(requests);
		}
	}
}
