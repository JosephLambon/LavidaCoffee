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

        public async Task<IEnumerable<EmailRequest>> GetAllEmailRequestsAsync()
        {
            return await _lavidaCoffeeDbContext.EmailRequests.AsNoTracking().OrderBy(e=> e.EmailRequestId).ToListAsync();
        }
        public async Task<EmailRequest?> GetEmailRequestByIdAsync(int emailRequestId)
        {
            return await _lavidaCoffeeDbContext.EmailRequests
				.Include(e => e.Email)
				.FirstOrDefaultAsync(e => e.EmailRequestId == emailRequestId);
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
