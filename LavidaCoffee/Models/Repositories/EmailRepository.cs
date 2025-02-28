using System.IO.Pipelines;
using Microsoft.EntityFrameworkCore;

namespace LavidaCoffee.Models
{
    public class EmailRepository : IEmailRepository
    {
        private readonly LavidaCoffeeDbContext _lavidaCoffeeDbContext;

        public List<Email> EmailRequests { get; set; } = default!;

        public EmailRepository(LavidaCoffeeDbContext lavidaCoffeeDbContext)
        {
            _lavidaCoffeeDbContext = lavidaCoffeeDbContext;
        }

        public async Task<IEnumerable<Email>> GetAllEmailRequestsAsync()
        {
            return await _lavidaCoffeeDbContext.Emails.AsNoTracking().OrderBy(e=> e.EmailId).ToListAsync();
        }
        public async Task<Email?> GetEmailRequestByIdAsync(int emailRequestId)
        {
            return await _lavidaCoffeeDbContext.Emails
				.Include(e => e.EmailId)
				.FirstOrDefaultAsync(e => e.EmailId == emailRequestId);
        }

        public void CreateEmailRequest(Email email)
        {
            _lavidaCoffeeDbContext.Emails.Add(email);
            _lavidaCoffeeDbContext.SaveChanges();
		}

		public IEnumerable<Email> requestsForCurrentPage(int page)
		{
            return _lavidaCoffeeDbContext.Emails.
                OrderBy(e => e.EmailId)
                .Skip((page-1)*10)
                .Take(10)
                .ToList();
		}

	}
}
