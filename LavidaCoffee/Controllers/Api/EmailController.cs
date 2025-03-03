using System.IO.Pipelines;
using LavidaCoffee.Models;
using LavidaCoffee.Models.Utilities;
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
		[HttpGet("get/{page}")]
		public async Task<IActionResult> Get(int page = 1)
		{
			IEnumerable<Email> emailRequests = await _emailRepository.GetAllEmailsAsync();
			int total = emailRequests.Count();
			int pageSize = 10;
			var numberPages = (int)Math.Ceiling((decimal)total / pageSize);
			var requestsPerPage = emailRequests
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			return Ok(requestsPerPage);
		}

		private int pageSize = 10;
		[HttpPost("indexpaging/{pageNumber}")]
		public async Task<IActionResult> IndexPaging(int? pageNumber)
		{
			var emails = await _emailRepository.GetEmailsPagedAsync(pageNumber, pageSize);
			pageNumber ??= 1;
			var count = await _emailRepository.GetAllEmailsCountAsync();
			return new JsonResult(new PagedList<Email>(emails.ToList(), count, pageNumber.Value, pageSize));
		}
	}
}
