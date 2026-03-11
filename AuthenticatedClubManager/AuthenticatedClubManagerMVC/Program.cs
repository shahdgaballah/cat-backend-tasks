using Microsoft.EntityFrameworkCore;
using AuthenticatedClubManagerMVC.Data;
using AuthenticatedClubManagerMVC.Services.Implementation;
using AuthenticatedClubManagerMVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace AuthenticatedClubManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(
                options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

            );
            //add identity efcore
            builder.Services.AddIdentity<IdentityUser, IdentityRole>( opt=>
             {
                //password settings
                 opt.Password.RequireDigit = true;
                 opt.Password.RequireLowercase = true;
                 opt.Password.RequireUppercase = true;
                 opt.Password.RequireNonAlphanumeric = true;
                 opt.Password.RequiredLength = 8;

                 //lockout settings
                 opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 
                 opt.Lockout.MaxFailedAccessAttempts = 5;
                 
                 opt.Lockout.AllowedForNewUsers=true;

                 //user settings
                 opt.User.RequireUniqueEmail = true;

                 //sign in settings
                 opt.SignIn.RequireConfirmedEmail = false;
             }
             ).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IFileService, FileService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
