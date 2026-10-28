/* =====================================================================
   BÀI THỰC HÀNH 4 - Script tạo database School (SQL Server)
   Cấu trúc tương đương với database mà EF Core Code First sinh ra
   (MyWebApp), nên dùng chung được cho cả MyWebApp và DBFirstApp.

   Cách chạy: mở bằng SSMS / Azure Data Studio, kết nối vào SQL Server
   rồi nhấn Execute (F5). Script có thể chạy lại nhiều lần.
   ===================================================================== */

IF DB_ID(N'School') IS NULL
    CREATE DATABASE School;
GO

USE School;
GO

/* ---------- Bảng ---------- */
IF OBJECT_ID(N'dbo.Major', N'U') IS NULL
CREATE TABLE dbo.Major (
    MajorID   INT IDENTITY(1,1) NOT NULL,
    MajorName NVARCHAR(MAX)     NOT NULL,
    CONSTRAINT PK_Major PRIMARY KEY (MajorID)
);
GO

IF OBJECT_ID(N'dbo.Course', N'U') IS NULL
CREATE TABLE dbo.Course (
    CourseID INT           NOT NULL,          -- không tự tăng (DatabaseGeneratedOption.None)
    Title    NVARCHAR(MAX) NOT NULL,
    Credits  INT           NOT NULL,
    CONSTRAINT PK_Course PRIMARY KEY (CourseID)
);
GO

IF OBJECT_ID(N'dbo.Learner', N'U') IS NULL
CREATE TABLE dbo.Learner (
    LearnerID      INT IDENTITY(1,1) NOT NULL,
    LastName       NVARCHAR(MAX)     NOT NULL,
    FirstMidName   NVARCHAR(MAX)     NOT NULL,
    EnrollmentDate DATETIME2         NOT NULL,
    MajorID        INT               NOT NULL,
    CONSTRAINT PK_Learner PRIMARY KEY (LearnerID),
    CONSTRAINT FK_Learner_Major_MajorID FOREIGN KEY (MajorID)
        REFERENCES dbo.Major (MajorID) ON DELETE CASCADE
);
GO

IF OBJECT_ID(N'dbo.Enrollment', N'U') IS NULL
CREATE TABLE dbo.Enrollment (
    EnrollmentID INT IDENTITY(1,1) NOT NULL,
    CourseID     INT  NOT NULL,
    LearnerID    INT  NOT NULL,
    Grade        REAL NOT NULL,
    CONSTRAINT PK_Enrollment PRIMARY KEY (EnrollmentID),
    CONSTRAINT FK_Enrollment_Course_CourseID FOREIGN KEY (CourseID)
        REFERENCES dbo.Course (CourseID) ON DELETE CASCADE,
    CONSTRAINT FK_Enrollment_Learner_LearnerID FOREIGN KEY (LearnerID)
        REFERENCES dbo.Learner (LearnerID) ON DELETE CASCADE
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Learner_MajorID')
    CREATE INDEX IX_Learner_MajorID ON dbo.Learner (MajorID);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Enrollment_CourseID')
    CREATE INDEX IX_Enrollment_CourseID ON dbo.Enrollment (CourseID);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Enrollment_LearnerID')
    CREATE INDEX IX_Enrollment_LearnerID ON dbo.Enrollment (LearnerID);
GO

/* ---------- Dữ liệu mẫu (giống DbInitializer.cs) ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Major)
BEGIN
    INSERT INTO dbo.Major (MajorName) VALUES (N'IT'), (N'Economics'), (N'Mathematics');

    INSERT INTO dbo.Learner (FirstMidName, LastName, EnrollmentDate, MajorID) VALUES
        (N'Carson',   N'Alexander', '2005-09-01', 1),
        (N'Meredith', N'Alonso',    '2002-09-01', 2);

    INSERT INTO dbo.Course (CourseID, Title, Credits) VALUES
        (1050, N'Chemistry',      3),
        (4022, N'Microeconomics', 3),
        (4041, N'Macroeconomics', 3);

    INSERT INTO dbo.Enrollment (LearnerID, CourseID, Grade) VALUES
        (1, 1050, 5.5),
        (1, 4022, 7.5),
        (2, 1050, 3.5),
        (2, 4041, 7.0);
END
GO

/* Kiểm tra nhanh */
SELECT * FROM dbo.Major;
SELECT * FROM dbo.Learner;
SELECT * FROM dbo.Course;
SELECT * FROM dbo.Enrollment;
GO

/* ---------- (Tùy chọn) Xóa hẳn database để làm lại từ đầu ----------
USE master;
ALTER DATABASE School SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE School;
------------------------------------------------------------------- */
