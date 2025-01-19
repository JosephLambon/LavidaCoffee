using System.IO.Pipelines;
using Microsoft.EntityFrameworkCore;

namespace LavidaCoffee.Models
{
    public class EmailRequestRepository : IEmailRequestRepository
    {
        private readonly LavidaCoffeeDbContext _lavidaCoffeeDbContext;

        public List<EmailRequest> EmailRequests { get; set; } = default!;

        public EmailRequestRepository(LavidaCoffeeDbContext lavidaCoffeeDbContext)
        {
            _lavidaCoffeeDbContext = lavidaCoffeeDbContext;
        }

        public List<EmailRequest> AllEmailRequests()
        {
            return _lavidaCoffeeDbContext.EmailRequests.Include(e => e.Email).ToList();
        }
            
        public EmailRequest? GetEmailRequestById(int emailRequestId)
        {
			var emailRequest = _lavidaCoffeeDbContext.EmailRequests
		.Include(e => e.Email)
		.FirstOrDefault(e => e.EmailRequestId == emailRequestId);

			return emailRequest;
        }

        public void CreateEmailRequest(EmailRequest emailRequest)
        {
            _lavidaCoffeeDbContext.EmailRequests.Add(emailRequest);
            _lavidaCoffeeDbContext.SaveChanges();
		}

		public IEnumerable<EmailRequest> requestsForCurrentPage(int page)
		{


            return _lavidaCoffeeDbContext.EmailRequests.
                Include(e => e.Email)
                .Skip((page-1)*10)
                .Take(10)
                .ToList();
		}

	}
}
