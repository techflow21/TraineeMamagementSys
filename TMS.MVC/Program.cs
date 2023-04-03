using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TMS.BLL.Implementation;
using TMS.BLL.Interfaces;
using TMS.DAL.Context;
using TMS.DAL.Entities;
using TMS.DAL.Interfaces;
using TMS.DAL.Repository;

namespace TMS.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                //var connectString = builder.Configuration.GetConnectionString("TraineeMgtDbConnection");
                var connectionString = builder.Configuration.GetSection("ConnectionString")["DefaultConnection"];
                options.UseSqlServer(connectionString);

            });

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

            builder.Services.AddScoped<ICareerPathService, CareerPathService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();

            builder.Services.AddScoped<ITraineeService, TraineeService>();


            builder.Services.AddAutoMapper(Assembly.Load("TMS.BLL"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //await SeedData.EnsurePopulatedAsync(app);

            await app.RunAsync();
        }
    }
}