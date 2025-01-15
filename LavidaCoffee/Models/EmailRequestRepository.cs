using Microsoft.EntityFrameworkCore;

namespace LavidaCoffee.Models
{
    public class EmailRequestRepository : IEmailRequestRepository
    {
        private readonly LavidaCoffeeDbContext _lavidaCoffeeDbContext;

        public EmailRequestRepository(LavidaCoffeeDbContext lavidaCoffeeDbContext)
        {
            _lavidaCoffeeDbContext = lavidaCoffeeDbContext;
        }

        public IEnumerable<EmailRequest> AllEmailRequests
        {
            get
            {
                return _lavidaCoffeeDbContext.EmailRequests.Include(c =>  c.Subject);
            }
        }

        public EmailRequest? GetEmailRequestById(int emailRequestId)
        {
            return _lavidaCoffeeDbContext.EmailRequests.FirstOrDefault(e => e.EmailRequestId == emailRequestId);
        }

    }
}
