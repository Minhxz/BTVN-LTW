# Bài thực hành 3 - Validation (ASP.NET Core MVC)

Không dùng cơ sở dữ liệu: danh sách sinh viên lưu trong bộ nhớ (static List) nên mỗi lần chạy lại app dữ liệu quay về 3 sinh viên mẫu.

## Cách chạy
Yêu cầu: .NET 8 SDK (hoặc Visual Studio 2022 17.8+).

**Visual Studio:** mở file `Lab3Validation.csproj` (File > Open > Project) -> nhấn F5 / Ctrl+F5.

**Dòng lệnh:**
```
cd Lesson11_TH_Lab03
dotnet run
```
Mở trình duyệt: http://localhost:5080 (trang Student/Index) -> bấm "Thêm sinh viên".
Máy cần có Internet (Bootstrap, jQuery tải qua CDN).

Dùng .NET 6/7? Đổi `<TargetFramework>net8.0</TargetFramework>` trong Lab3Validation.csproj thành `net6.0`.

## Các ràng buộc đã làm (Models/Student.cs)
- Nhãn + thông báo lỗi tiếng Việt ([Display], ErrorMessage)
- Mật khẩu >= 8 ký tự, có hoa, thường, số, ký tự đặc biệt
- Họ tên 4 - 100 ký tự
- Email phải có đuôi @gmail.com
- Thêm trường Score (double?), bắt buộc, từ 0.0 đến 10.0
- Ngày sinh từ 01/01/1963 đến 31/12/2005
