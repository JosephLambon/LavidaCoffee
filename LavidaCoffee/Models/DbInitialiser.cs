using System.Drawing;
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
            if (!context.Events.Any())
            {
                context.AddRange
                (
                    new Event { Address="1,Test Street,Worcestershire,WR4 7GH", Date=new DateTime(2000,1,1), Title="Example fun!", ShortDescription="Fun for the family", LongDescription="This taster event is an annual classic. Highly anticipated, highly inclusive.", ImageUrl="#", ThumbnailUrl="#" }
                );
            }

            context.SaveChanges();
        }
    }
}
