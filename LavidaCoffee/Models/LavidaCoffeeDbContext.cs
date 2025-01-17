using Microsoft.EntityFrameworkCore;

namespace LavidaCoffee.Models
{
    public class LavidaCoffeeDbContext : DbContext
    {
        public LavidaCoffeeDbContext(DbContextOptions<LavidaCoffeeDbContext> options) : base(options)
        {

        }

        public DbSet<EmailRequest> EmailRequests { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}