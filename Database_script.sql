-- =============================================================
-- Đề thi mẫu 2026 - Lập trình Web (ASP.NET Core MVC)
-- Cơ sở dữ liệu: QLNhaSach  (SQL Server)
-- Chạy toàn bộ script này trong SQL Server Management Studio.
-- =============================================================
USE [master]
GO
IF DB_ID(N'QLNhaSach') IS NOT NULL
BEGIN
    ALTER DATABASE [QLNhaSach] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [QLNhaSach];
END
GO
CREATE DATABASE [QLNhaSach]
GO
USE [QLNhaSach]
GO

-- Bảng thể loại sách
CREATE TABLE [dbo].[TheLoai](
    [MaTL]  [int] IDENTITY(1,1) NOT NULL,
    [TenTL] [nvarchar](50) NOT NULL,
    CONSTRAINT [PK_TheLoai] PRIMARY KEY CLUSTERED ([MaTL] ASC)
)
GO

-- Bảng sách (Gia tính theo nghìn đồng)
CREATE TABLE [dbo].[Sach](
    [MaSach]  [int] IDENTITY(1,1) NOT NULL,
    [MaTL]    [int] NOT NULL,
    [TenSach] [nvarchar](100) NOT NULL,
    [Gia]     [numeric](18, 0) NULL,
    [Anh]     [nvarchar](100) NULL,
    CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED ([MaSach] ASC)
)
GO
ALTER TABLE [dbo].[Sach] WITH CHECK ADD CONSTRAINT [FK_Sach_TheLoai]
    FOREIGN KEY([MaTL]) REFERENCES [dbo].[TheLoai] ([MaTL])
GO

-- Dữ liệu mẫu
SET IDENTITY_INSERT [dbo].[TheLoai] ON
INSERT [dbo].[TheLoai] ([MaTL], [TenTL]) VALUES (1, N'Công nghệ thông tin')
INSERT [dbo].[TheLoai] ([MaTL], [TenTL]) VALUES (2, N'Kinh tế')
INSERT [dbo].[TheLoai] ([MaTL], [TenTL]) VALUES (3, N'Văn học')
INSERT [dbo].[TheLoai] ([MaTL], [TenTL]) VALUES (4, N'Thiếu nhi')
INSERT [dbo].[TheLoai] ([MaTL], [TenTL]) VALUES (5, N'Ngoại ngữ')
SET IDENTITY_INSERT [dbo].[TheLoai] OFF
GO

SET IDENTITY_INSERT [dbo].[Sach] ON
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (1,  1, N'Lập trình C# căn bản',        120, N'Img_1.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (2,  1, N'Thiết kế web hiện đại',      150, N'Img_2.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (3,  1, N'Cơ sở dữ liệu SQL Server',   135, N'Img_3.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (4,  1, N'Nhập môn lập trình',          70, N'Img_1.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (5,  2, N'Nguyên lý kinh tế học',      210, N'Img_4.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (6,  2, N'Quản trị doanh nghiệp',      180, N'Img_5.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (7,  2, N'Marketing căn bản',          99, N'Img_5.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (8,  3, N'Tuyển tập truyện ngắn',       85, N'Img_6.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (9,  3, N'Thơ và đời sống',           110, N'Img_6.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (10, 4, N'Truyện cổ tích cho bé',      60, N'Img_7.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (11, 4, N'Bé học chữ cái',             45, N'Img_7.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (12, 5, N'Tiếng Anh giao tiếp',        95, N'Img_8.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (13, 5, N'Luyện thi IELTS',           160, N'Img_8.jpg')
INSERT [dbo].[Sach] ([MaSach], [MaTL], [TenSach], [Gia], [Anh]) VALUES (14, 1, N'Sách chưa có giá',         NULL, N'Img_2.jpg')
SET IDENTITY_INSERT [dbo].[Sach] OFF
GO
