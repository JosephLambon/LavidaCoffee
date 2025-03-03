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

        public async Task<IEnumerable<Email>> GetAllEmailsAsync()
        {
            return await _lavidaCoffeeDbContext.Emails.AsNoTracking().OrderBy(e=> e.EmailId).ToListAsync();
        }
        public async Task<Email?> GetEmailByIdAsync(int emailRequestId)
        {
            return await _lavidaCoffeeDbContext.Emails
				.Include(e => e.EmailId)
				.FirstOrDefaultAsync(e => e.EmailId == emailRequestId);
        }

        public async Task CreateEmailAsync(Email email)
        {
            await _lavidaCoffeeDbContext.Emails.AddAsync(email);
            await _lavidaCoffeeDbContext.SaveChangesAsync();
		}

        public async Task<int> GetAllEmailsCountAsync()
        {
            return await _lavidaCoffeeDbContext.Emails.AsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<Email>> GetEmailsPagedAsync(int? pageNumber, int pageSize)
        {
            IQueryable<Email> emails = from e in _lavidaCoffeeDbContext.Emails 
                select e;
                
            pageNumber ??= 1;

            emails = emails.Skip((pageNumber.Value - 1) * pageSize)
                .Take(pageSize);
            
            return await emails.AsNoTracking().ToListAsync();
        }
	}
}
