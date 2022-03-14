using JobApplication.Data;
using JobApplication.Models;
using JobApplication.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

//var host = BuildWebHost(args);
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    try
    {
        var serviceProvider = services.GetRequiredService<IServiceProvider>();
        var configuration = services.GetRequiredService<IConfiguration>();
        Seed.CreateRoles(serviceProvider, configuration).Wait();
    }
    catch (Exception exception)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred while creating roles");
    }

}
//            host.Run();


//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//namespace JobApplication
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            //CreateHostBuilder(args).Build().Run();
//            var host = BuildWebHost(args);
//            InitializeDatabase(host);
//            host.Run();

//        }


//        public static IWebHost BuildWebHost(string[] args) =>
//           WebHost.CreateDefaultBuilder(args)
//           .UseIISIntegration()
//           .UseStartup<Startup>().Build();

//public static IHostBuilder CreateHostBuilder(string[] args) =>
//    Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<Startup>();
//        });

void InitializeDatabase(WebApplicationBuilder host)
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        try
        {
            var serviceProvider = services.GetRequiredService<IServiceProvider>();
            var configuration = services.GetRequiredService<IConfiguration>();
            Seed.CreateRoles(serviceProvider, configuration).Wait();
        }
        catch (Exception exception)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "An error occurred while creating roles");
        }

    }

}
//}

//}

