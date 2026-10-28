# Bài thực hành 4 - Entity Framework (ASP.NET Core MVC)

```
Lesson11_TH_Lab04/
├── Lab4.sln
├── Database/School.sql     <- script tạo database + dữ liệu mẫu
├── MyWebApp/               <- Phần I : Code First  (Model -> Database) + CRUD Learner
└── DBFirstApp/             <- Phần II: Database First (Database -> Model) + CRUD Learners
```

## 1. Chuẩn bị
1. Cài **.NET 8 SDK** (hoặc Visual Studio 2022 17.8+ có workload ASP.NET).
2. Cài **SQL Server** (Express/Developer), bật **SQL Server Authentication**, enable tài khoản **sa**,
   đặt mật khẩu cho sa rồi **restart SQL Server service**.
3. Mở `appsettings.json` của **cả 2 project** và sửa chuỗi kết nối cho đúng máy của bạn:
   ```
   Data Source=(local);Initial Catalog=School;Persist Security Info=True;User ID=sa;Password=MẬT_KHẨU_CỦA_BẠN;TrustServerCertificate=True
   ```
   Dùng LocalDB (không cần sa) thì thay bằng:
   ```
   Server=(localdb)\MSSQLLocalDB;Database=School;Trusted_Connection=True;TrustServerCertificate=True
   ```
   (Đề bài lưu chuỗi kết nối trong Secrets.json; ở đây để trong appsettings.json cho đơn giản, hoạt động tương đương.)

## 2. Tạo database - chọn 1 trong 2 cách
- **Cách A (Code First, đúng đề bài):** không cần làm gì, chạy `MyWebApp` lần đầu sẽ tự tạo database `School`,
  các bảng và dữ liệu mẫu (DbInitializer).
- **Cách B (script SQL):** mở `Database/School.sql` bằng SSMS / Azure Data Studio -> Execute.
  Script tạo database, bảng, khóa ngoại và dữ liệu mẫu giống hệt Code First, chạy lại nhiều lần vẫn an toàn.

## 3. Chạy
**Visual Studio:** mở `Lab4.sln` -> chuột phải project muốn chạy -> *Set as Startup Project* -> F5.
**Dòng lệnh:**
```
cd MyWebApp   &&  dotnet run      ->  http://localhost:5100   (Learner/Index)
cd DBFirstApp &&  dotnet run      ->  http://localhost:5200   (Learners/Index)
```
`DBFirstApp` đọc database `School` có sẵn nên phải tạo database trước (chạy MyWebApp 1 lần hoặc chạy School.sql).

Kiểm tra dữ liệu: mở SSMS -> database School -> 4 bảng Major, Learner, Course, Enrollment.

## 4. Chức năng MyWebApp (CRUD bảng Learner)
- List: `/Learner` - Create: nút "Create New Student" - Edit / Delete: link trên từng dòng.
- Learner 1 và 2 đã có Enrollment nên bấm Delete sẽ báo "This learner has some enrollments, can't delete!"
  (đúng như đề). Hãy Create learner mới rồi Delete để thấy màn hình xác nhận xóa.

## 5. DBFirstApp
Các file trong `DBFirstApp/Models` tương đương kết quả lệnh scaffold của đề bài. Muốn tự sinh lại bằng lệnh
(Package Manager Console, Default project: DBFirstApp):
```
Scaffold-DbContext "Data Source=(local);Initial Catalog=School;User ID=sa;Password=...;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -Project DBFirstApp -OutputDir Models -Force
```
hoặc dòng lệnh:
```
dotnet tool install --global dotnet-ef
dotnet ef dbcontext scaffold "<connection string>" Microsoft.EntityFrameworkCore.SqlServer -o Models --force
```
`LearnersController` + views (Index/Details/Create/Edit/Delete) tương đương bước *Add > Controller > MVC Controller with views, using Entity Framework*.

Khác biệt cần so sánh theo đề: Code First dùng tên thuộc tính `LearnerID`, controller `LearnerController` tự viết,
còn Database First dùng `LearnerId`, `LearnersController` sinh tự động (Delete không kiểm tra Enrollment,
vì khóa ngoại cascade nên xóa Learner sẽ xóa luôn các Enrollment của nó).

## 6. Ghi chú
- Dự án dùng .NET 8 + EF Core 8 (đề dùng 6.0.x đã hết hỗ trợ). Muốn dùng .NET 6: đổi `TargetFramework` thành `net6.0`
  và `Version="8.0.*"` thành `6.0.*` trong 2 file .csproj.
- Cần Internet khi chạy (Bootstrap, jQuery tải từ CDN).
- Lỗi "Login failed for user 'sa'" -> sai mật khẩu hoặc chưa bật SQL Authentication / chưa restart SQL Server.
- Lỗi chứng chỉ SSL khi kết nối -> giữ `TrustServerCertificate=True` trong chuỗi kết nối.
