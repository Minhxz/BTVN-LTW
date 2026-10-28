using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Thêm MVC + Việt hóa các thông báo lỗi mặc định của Model Binding
builder.Services.AddControllersWithViews(options =>
{
    var p = options.ModelBindingMessageProvider;
    p.SetValueMustBeANumberAccessor(field => $"{field} phải là số thực (dùng dấu chấm, ví dụ 8.5)");
    p.SetAttemptedValueIsInvalidAccessor((value, field) => $"Giá trị '{value}' không hợp lệ cho {field}");
    p.SetValueIsInvalidAccessor(value => $"Giá trị '{value}' không hợp lệ");
    p.SetMissingBindRequiredValueAccessor(field => $"Thiếu giá trị cho {field}");
});

var app = builder.Build();

// Cố định culture en-US để số thực dùng dấu "." và ngày không phụ thuộc cấu hình máy
var culture = new CultureInfo("en-US");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = new List<CultureInfo> { culture },
    SupportedUICultures = new List<CultureInfo> { culture }
});

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();
