using LavidaCoffee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmailRequestRepository, EmailRequestRepository>();

builder.Services.AddDbContext<LavidaCoffeeDbContext>(options =>
{
    options.UseSqlServer(
            builder.Configuration["ConnectionStrings:LavidaCoffeeDbContextConnection"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

DbInitialiser.Seed(app);

app.Run();
