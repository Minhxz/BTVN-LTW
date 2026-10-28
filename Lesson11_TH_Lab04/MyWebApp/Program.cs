using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký SchoolContext là một DbContext của ứng dụng
// (chuỗi kết nối "SchoolContext" nằm trong appsettings.json hoặc Secrets.json)
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Khởi tạo database + dữ liệu mẫu khi chạy lần đầu
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Learner/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Learner}/{action=Index}/{id?}");

app.Run();
