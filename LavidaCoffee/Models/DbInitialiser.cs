using System.IO.Pipelines;

namespace LavidaCoffee.Models
{
    public class DbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            LavidaCoffeeDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<LavidaCoffeeDbContext>();

            if (!context.Emails.Any())
            {
                context.AddRange
                (
                    new Email { Subject="This is my subject", Body = "This is my body", CustomerEmail="joe.lambon25@gmail.com" }
                );
            }

            context.SaveChanges();
        }
    }
}
