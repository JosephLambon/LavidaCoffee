using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LavidaCoffee.Models
{
    public class LavidaCoffeeDbContext : IdentityDbContext
    {
        public LavidaCoffeeDbContext(DbContextOptions<LavidaCoffeeDbContext> options) : base(options)
        {

        }
        public DbSet<EmailRequest> EmailRequests { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}