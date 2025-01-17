using LavidaCoffee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace LavidaCoffee
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("LavidaCoffeeDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LavidaCoffeeDbContextConnection' not found.");
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IEmailRequestRepository, EmailRequestRepository>();

            builder.Services.AddDbContext<LavidaCoffeeDbContext>(options =>
            {
                options.UseSqlServer(
                        builder.Configuration["ConnectionStrings:LavidaCoffeeDbContextConnection"]);
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LavidaCoffeeDbContext>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseAuthorization();
            app.UseAntiforgery();
            app.MapRazorPages();

            // Seeding initial data into system, so always created regardless of server etc.
            DbInitialiser.Seed(app);

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin" };

                foreach (var role in roles)
                {
                    // If any role does not yet exist, create it
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string email = "admin@lavida.uk";
                string password = "Admin1,";


                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;
                    user.EmailConfirmed = true;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            app.Run();
        }
    }
}

