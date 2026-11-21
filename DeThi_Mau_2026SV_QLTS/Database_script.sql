-- =============================================================
-- Đề thi mẫu - Lập trình Web (ASP.NET Core MVC, .NET 10)
-- Chủ đề: Quản lý tài sản doanh nghiệp
-- Cơ sở dữ liệu: QLTaiSan  (SQL Server)
-- Chạy toàn bộ script này trong SQL Server Management Studio.
-- =============================================================
USE [master]
GO
IF DB_ID(N'QLTaiSan') IS NOT NULL
BEGIN
    ALTER DATABASE [QLTaiSan] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [QLTaiSan];
END
GO
CREATE DATABASE [QLTaiSan]
GO
USE [QLTaiSan]
GO

-- Bảng loại tài sản
CREATE TABLE [dbo].[LoaiTaiSan](
    [MaLoai]  [int] IDENTITY(1,1) NOT NULL,
    [TenLoai] [nvarchar](50) NOT NULL,
    CONSTRAINT [PK_LoaiTaiSan] PRIMARY KEY CLUSTERED ([MaLoai] ASC)
)
GO

-- Bảng tài sản (NguyenGia tính theo triệu đồng)
CREATE TABLE [dbo].[TaiSan](
    [MaTS]      [int] IDENTITY(1,1) NOT NULL,
    [MaLoai]    [int] NOT NULL,
    [TenTS]     [nvarchar](100) NOT NULL,
    [NguyenGia] [numeric](18, 0) NULL,
    [Anh]       [nvarchar](100) NULL,
    CONSTRAINT [PK_TaiSan] PRIMARY KEY CLUSTERED ([MaTS] ASC)
)
GO
ALTER TABLE [dbo].[TaiSan] WITH CHECK ADD CONSTRAINT [FK_TaiSan_LoaiTaiSan]
    FOREIGN KEY([MaLoai]) REFERENCES [dbo].[LoaiTaiSan] ([MaLoai])
GO

-- Dữ liệu mẫu
SET IDENTITY_INSERT [dbo].[LoaiTaiSan] ON
INSERT [dbo].[LoaiTaiSan] ([MaLoai], [TenLoai]) VALUES (1, N'Thiết bị CNTT')
INSERT [dbo].[LoaiTaiSan] ([MaLoai], [TenLoai]) VALUES (2, N'Thiết bị văn phòng')
INSERT [dbo].[LoaiTaiSan] ([MaLoai], [TenLoai]) VALUES (3, N'Nội thất')
INSERT [dbo].[LoaiTaiSan] ([MaLoai], [TenLoai]) VALUES (4, N'Phương tiện vận tải')
INSERT [dbo].[LoaiTaiSan] ([MaLoai], [TenLoai]) VALUES (5, N'Máy móc sản xuất')
SET IDENTITY_INSERT [dbo].[LoaiTaiSan] OFF
GO

SET IDENTITY_INSERT [dbo].[TaiSan] ON
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (1,  1, N'Laptop Dell Latitude 5440',        28,   N'Img_1.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (2,  1, N'Máy chủ Dell PowerEdge R450',     95,   N'Img_2.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (3,  1, N'Switch Cisco Catalyst 48 cổng',    45,   N'Img_3.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (4,  1, N'Màn hình LG 27 inch',               6,   N'Img_1.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (5,  2, N'Máy chiếu Epson EB-X49',           15,   N'Img_4.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (6,  2, N'Máy photocopy Ricoh MP 3055',      62,   N'Img_5.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (7,  2, N'Máy hủy tài liệu Silicon',          8,   N'Img_5.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (8,  2, N'Điều hòa Daikin 18000 BTU',        19,   N'Img_4.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (9,  3, N'Bàn họp 12 chỗ Hòa Phát',          22,   N'Img_6.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (10, 3, N'Ghế xoay văn phòng',                3,   N'Img_6.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (11, 3, N'Tủ hồ sơ sắt 4 ngăn',             12,   N'Img_6.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (12, 4, N'Ô tô Toyota Innova 2.0',          820,   N'Img_7.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (13, 4, N'Xe tải Hyundai 1,5 tấn',          560,   N'Img_7.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (14, 5, N'Máy CNC phay 3 trục',             1250,   N'Img_8.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (15, 5, N'Máy nén khí trục vít 15kW',       180,   N'Img_8.jpg')
INSERT [dbo].[TaiSan] ([MaTS], [MaLoai], [TenTS], [NguyenGia], [Anh]) VALUES (16, 1, N'Tài sản chưa định giá',           NULL,   N'Img_2.jpg')
SET IDENTITY_INSERT [dbo].[TaiSan] OFF
GO
