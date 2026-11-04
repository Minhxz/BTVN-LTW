using Microsoft.EntityFrameworkCore;
using XmLesson12_Lab5.Data;

namespace XmLesson12_Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SchoolContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DbInitializer.Initialize(services);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Learner}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
