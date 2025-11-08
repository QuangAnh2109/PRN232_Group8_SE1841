USE [PRN232_Group8_SE1841]
GO

SET IDENTITY_INSERT [Centers] ON;
GO

INSERT INTO [Centers] ([Id], [Name], [ManagerId], [Address], [Email], [PhoneNumber], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(1, N'FPT Hà Nội', 1, N'8 Tôn Thất Thuyết, Mỹ Đình, Hà Nội', N'hanoi@fpt.edu.vn', N'0241234567', GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(2, N'FPT Đà Nẵng', 2, N'578 Nguyễn Hữu Thọ, Đà Nẵng', N'danang@fpt.edu.vn', N'0236234567', GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(3, N'FPT Hồ Chí Minh', 3, N'Lô E2a-7, Đường D1, Long Thạnh Mỹ, Quận 9, TP HCM', N'hochiminh@fpt.edu.vn', N'0283456789', GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(4, N'FPT Cần Thơ', 4, N'600 Nguyễn Văn Cừ, Cần Thơ', N'cantho@fpt.edu.vn', N'0292567890', GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(5, N'FPT Quy Nhơn', 5, N'Khu CNTT Tập trung, Quy Nhơn', N'quynhon@fpt.edu.vn', N'0256678901', GETDATE(), GETDATE(), 0, 0, 1, 1, 0);
GO

SET IDENTITY_INSERT [Centers] OFF;
GO

-- =============================================
-- 2. INSERT USERS
-- =============================================
PRINT 'Inserting Users...'
GO

SET IDENTITY_INSERT [Users] ON;
GO

-- Password cho tất cả users: "Password123!" (đã được hash bằng BCrypt)
-- Hash này là: $2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q

-- Managers (RoleId = 2)
INSERT INTO [Users] ([Id], [Username], [FullName], [Email], [PhoneNumber], [PasswordHash], [CenterId], [LastModifiedTime], [RoleId], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(1, N'manager.hn', N'Nguyễn Văn Quản', N'nguyen.van.quan@fpt.edu.vn', N'0912345678', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 2, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(2, N'manager.dn', N'Trần Thị Minh', N'tran.thi.minh@fpt.edu.vn', N'0923456789', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 2, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(3, N'manager.hcm', N'Lê Văn Cường', N'le.van.cuong@fpt.edu.vn', N'0934567890', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 2, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(4, N'manager.ct', N'Phạm Thị Hoa', N'pham.thi.hoa@fpt.edu.vn', N'0945678901', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 4, GETDATE(), 2, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(5, N'manager.qn', N'Hoàng Văn Nam', N'hoang.van.nam@fpt.edu.vn', N'0956789012', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 5, GETDATE(), 2, GETDATE(), GETDATE(), 0, 0, 1, 1, 0);
GO

-- Teachers (RoleId = 3)
INSERT INTO [Users] ([Id], [Username], [FullName], [Email], [PhoneNumber], [PasswordHash], [CenterId], [LastModifiedTime], [RoleId], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(6, N'teacher.hn1', N'Nguyễn Thị Lan', N'nguyen.thi.lan@fpt.edu.vn', N'0961234567', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 3, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(7, N'teacher.hn2', N'Trần Văn Đức', N'tran.van.duc@fpt.edu.vn', N'0972345678', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 3, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(8, N'teacher.dn1', N'Lê Thị Mai', N'le.thi.mai@fpt.edu.vn', N'0983456789', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 3, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(9, N'teacher.hcm1', N'Phạm Văn Hùng', N'pham.van.hung@fpt.edu.vn', N'0994567890', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 3, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(10, N'teacher.hcm2', N'Hoàng Thị Thu', N'hoang.thi.thu@fpt.edu.vn', N'0905678901', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 3, GETDATE(), GETDATE(), 0, 0, 1, 1, 0);
GO

-- Students (RoleId = 4) - 30 students
INSERT INTO [Users] ([Id], [Username], [FullName], [Email], [PhoneNumber], [PasswordHash], [CenterId], [LastModifiedTime], [RoleId], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
-- Hanoi Students
(11, N'student.hn001', N'Bùi Văn An', N'bui.van.an@student.fpt.edu.vn', N'0916789012', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(12, N'student.hn002', N'Đặng Thị Bình', N'dang.thi.binh@student.fpt.edu.vn', N'0927890123', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(13, N'student.hn003', N'Đỗ Văn Cường', N'do.van.cuong@student.fpt.edu.vn', N'0938901234', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(14, N'student.hn004', N'Dương Thị Diễm', N'duong.thi.diem@student.fpt.edu.vn', N'0949012345', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(15, N'student.hn005', N'Ngô Văn Em', N'ngo.van.em@student.fpt.edu.vn', N'0950123456', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(16, N'student.hn006', N'Vũ Thị Phương', N'vu.thi.phuong@student.fpt.edu.vn', N'0961234567', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(17, N'student.hn007', N'Lý Văn Giang', N'ly.van.giang@student.fpt.edu.vn', N'0972345678', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(18, N'student.hn008', N'Mai Thị Hương', N'mai.thi.huong@student.fpt.edu.vn', N'0983456789', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(19, N'student.hn009', N'Tô Văn Khoa', N'to.van.khoa@student.fpt.edu.vn', N'0994567890', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(20, N'student.hn010', N'Hồ Thị Linh', N'ho.thi.linh@student.fpt.edu.vn', N'0905678901', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 1, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),

-- Da Nang Students
(21, N'student.dn001', N'Cao Văn Minh', N'cao.van.minh@student.fpt.edu.vn', N'0916789012', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(22, N'student.dn002', N'Đinh Thị Nga', N'dinh.thi.nga@student.fpt.edu.vn', N'0927890123', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(23, N'student.dn003', N'Đoàn Văn Ông', N'doan.van.ong@student.fpt.edu.vn', N'0938901234', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(24, N'student.dn004', N'Phan Thị Phúc', N'phan.thi.phuc@student.fpt.edu.vn', N'0949012345', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(25, N'student.dn005', N'Trương Văn Quang', N'truong.van.quang@student.fpt.edu.vn', N'0950123456', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(26, N'student.dn006', N'Võ Thị Như', N'vo.thi.nhu@student.fpt.edu.vn', N'0961234567', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(27, N'student.dn007', N'Lương Văn Sơn', N'luong.van.son@student.fpt.edu.vn', N'0972345678', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(28, N'student.dn008', N'Từ Thị Tâm', N'tu.thi.tam@student.fpt.edu.vn', N'0983456789', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(29, N'student.dn009', N'Huỳnh Văn Uy', N'huynh.van.uy@student.fpt.edu.vn', N'0994567890', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(30, N'student.dn010', N'Âu Thị Vân', N'au.thi.van@student.fpt.edu.vn', N'0905678901', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 2, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),

-- Ho Chi Minh Students
(31, N'student.hcm001', N'Thái Văn Xuân', N'thai.van.xuan@student.fpt.edu.vn', N'0916789012', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(32, N'student.hcm002', N'Ông Thị Yến', N'ong.thi.yen@student.fpt.edu.vn', N'0927890123', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(33, N'student.hcm003', N'Hà Văn Bách', N'ha.van.bach@student.fpt.edu.vn', N'0938901234', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(34, N'student.hcm004', N'Kim Thị Châu', N'kim.thi.chau@student.fpt.edu.vn', N'0949012345', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(35, N'student.hcm005', N'La Văn Dũng', N'la.van.dung@student.fpt.edu.vn', N'0950123456', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(36, N'student.hcm006', N'Mã Thị Giang', N'ma.thi.giang@student.fpt.edu.vn', N'0961234567', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(37, N'student.hcm007', N'Mạc Văn Hải', N'mac.van.hai@student.fpt.edu.vn', N'0972345678', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(38, N'student.hcm008', N'Nghiêm Thị Khánh', N'nghiem.thi.khanh@student.fpt.edu.vn', N'0983456789', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(39, N'student.hcm009', N'Ninh Văn Lâm', N'ninh.van.lam@student.fpt.edu.vn', N'0994567890', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0),
(40, N'student.hcm010', N'Ứng Thị My', N'ung.thi.my@student.fpt.edu.vn', N'0905678901', N'$2a$12$ugIjTONZvr5gkLPldMiF7eA.eWOsWj/1o5V/2N86Qnoiuqv/siq/q', 3, GETDATE(), 4, GETDATE(), GETDATE(), 0, 0, 1, 1, 0);
GO

SET IDENTITY_INSERT [Users] OFF;
GO

-- =============================================
-- 3. INSERT CLASSES (Lớp học)
-- =============================================
PRINT 'Inserting Classes...'
GO

SET IDENTITY_INSERT [Classes] ON;
GO

INSERT INTO [Classes] ([Id], [Name], [CenterId], [StartDate], [EndDate], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
-- Hanoi Classes
(1, N'SE1841', 1, '2024-09-01', '2025-01-15', GETDATE(), GETDATE(), 1, 1, 1, 1, 0),
(2, N'SE1842', 1, '2024-09-01', '2025-01-15', GETDATE(), GETDATE(), 1, 1, 1, 1, 0),
(3, N'SE1843', 1, '2024-11-01', '2025-03-15', GETDATE(), GETDATE(), 1, 1, 1, 1, 0),

-- Da Nang Classes
(4, N'SE1851', 2, '2024-09-01', '2025-01-15', GETDATE(), GETDATE(), 2, 2, 1, 1, 0),
(5, N'SE1852', 2, '2024-11-01', '2025-03-15', GETDATE(), GETDATE(), 2, 2, 1, 1, 0),

-- Ho Chi Minh Classes
(6, N'SE1861', 3, '2024-09-01', '2025-01-15', GETDATE(), GETDATE(), 3, 3, 1, 1, 0),
(7, N'SE1862', 3, '2024-09-01', '2025-01-15', GETDATE(), GETDATE(), 3, 3, 1, 1, 0),
(8, N'SE1863', 3, '2024-11-01', '2025-03-15', GETDATE(), GETDATE(), 3, 3, 1, 1, 0);
GO

SET IDENTITY_INSERT [Classes] OFF;
GO

-- =============================================
-- 4. INSERT CLASS STUDENTS (Học sinh trong lớp)
-- =============================================
PRINT 'Inserting Class Students...'
GO

SET IDENTITY_INSERT [ClassStudents] ON;
GO

-- Class SE1841 (Hanoi) - 10 students
INSERT INTO [ClassStudents] ([Id], [StudentId], [ClassId], [JoinedAt], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(1, 11, 1, '2024-09-01', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(2, 12, 1, '2024-09-01', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(3, 13, 1, '2024-09-01', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(4, 14, 1, '2024-09-01', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(5, 15, 1, '2024-09-01', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(6, 16, 1, '2024-09-02', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(7, 17, 1, '2024-09-02', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(8, 18, 1, '2024-09-03', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(9, 19, 1, '2024-09-03', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(10, 20, 1, '2024-09-03', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),

-- Class SE1842 (Hanoi) - 5 students
(11, 11, 2, '2024-09-01', GETDATE(), GETDATE(), 7, 7, 1, 1, 0),
(12, 13, 2, '2024-09-01', GETDATE(), GETDATE(), 7, 7, 1, 1, 0),
(13, 15, 2, '2024-09-01', GETDATE(), GETDATE(), 7, 7, 1, 1, 0),
(14, 17, 2, '2024-09-01', GETDATE(), GETDATE(), 7, 7, 1, 1, 0),
(15, 19, 2, '2024-09-01', GETDATE(), GETDATE(), 7, 7, 1, 1, 0),

-- Class SE1851 (Da Nang) - 10 students
(16, 21, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(17, 22, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(18, 23, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(19, 24, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(20, 25, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(21, 26, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(22, 27, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(23, 28, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(24, 29, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(25, 30, 4, '2024-09-01', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),

-- Class SE1861 (Ho Chi Minh) - 10 students
(26, 31, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(27, 32, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(28, 33, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(29, 34, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(30, 35, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(31, 36, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(32, 37, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(33, 38, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(34, 39, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(35, 40, 6, '2024-09-01', GETDATE(), GETDATE(), 9, 9, 1, 1, 0);
GO

SET IDENTITY_INSERT [ClassStudents] OFF;
GO

-- =============================================
-- 5. INSERT TIMESHEETS (Lịch học)
-- =============================================
PRINT 'Inserting Timesheets...'
GO

SET IDENTITY_INSERT [Timesheets] ON;
GO

-- Timesheets for Class SE1841 (3 sessions)
INSERT INTO [Timesheets] ([Id], [ClassId], [StartTime], [EndTime], [IsOnline], [Title], [Description], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(1, 1, '2024-11-01 08:00:00', '2024-11-01 11:00:00', 0, N'Introduction to C# Programming', N'Basic concepts and syntax', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(2, 1, '2024-11-04 08:00:00', '2024-11-04 11:00:00', 0, N'Object-Oriented Programming', N'Classes, Objects, Inheritance', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(3, 1, '2024-11-06 08:00:00', '2024-11-06 11:00:00', 1, N'ASP.NET Core Basics', N'Web API development', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),
(4, 1, '2024-11-08 08:00:00', '2024-11-08 11:00:00', 0, N'Entity Framework Core', N'Database operations with EF Core', GETDATE(), GETDATE(), 6, 6, 1, 1, 0),

-- Timesheets for Class SE1851 (3 sessions)
(5, 4, '2024-11-02 13:00:00', '2024-11-02 16:00:00', 0, N'Introduction to Web Development', N'HTML, CSS, JavaScript basics', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(6, 4, '2024-11-05 13:00:00', '2024-11-05 16:00:00', 1, N'React Fundamentals', N'Components, Props, State', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),
(7, 4, '2024-11-07 13:00:00', '2024-11-07 16:00:00', 0, N'React Hooks and Context', N'useState, useEffect, Context API', GETDATE(), GETDATE(), 8, 8, 1, 1, 0),

-- Timesheets for Class SE1861 (3 sessions)
(8, 6, '2024-11-01 13:00:00', '2024-11-01 16:00:00', 0, N'Database Design', N'Normalization and ER diagrams', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(9, 6, '2024-11-04 13:00:00', '2024-11-04 16:00:00', 0, N'SQL Fundamentals', N'SELECT, JOIN, Aggregate functions', GETDATE(), GETDATE(), 9, 9, 1, 1, 0),
(10, 6, '2024-11-06 13:00:00', '2024-11-06 16:00:00', 1, N'Advanced SQL', N'Stored procedures, Triggers', GETDATE(), GETDATE(), 9, 9, 1, 1, 0);
GO

SET IDENTITY_INSERT [Timesheets] OFF;
GO

-- =============================================
-- 6. INSERT ATTENDANCE (Điểm danh)
-- =============================================
PRINT 'Inserting Attendance Records...'
GO

SET IDENTITY_INSERT [Attendance] ON;
GO

DECLARE @AttendanceId INT = 1;

-- Attendance for Timesheet 1 (Class SE1841, Session 1) - 10 students
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 11, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 12, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 13, 0, N'Sick leave', GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 14, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 15, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 16, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 17, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 18, 0, N'Late 30 minutes', GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 19, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 1, 20, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;

-- Attendance for Timesheet 2 (Class SE1841, Session 2)
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 11, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 12, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 13, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 14, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 15, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 16, 0, N'Family emergency', GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 17, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 18, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 19, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 2, 20, 1, NULL, GETDATE(), GETDATE(), 6, 6, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;

-- Attendance for Timesheet 5 (Class SE1851, Session 1) - 10 students from Da Nang
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 21, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 22, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 23, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 24, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 25, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 26, 0, N'No excuse', GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 27, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 28, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 29, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 5, 30, 1, NULL, GETDATE(), GETDATE(), 8, 8, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;

-- Attendance for Timesheet 8 (Class SE1861, Session 1) - 10 students from HCM
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 31, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 32, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 33, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 34, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 35, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 36, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 37, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 38, 0, N'Medical appointment', GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 39, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0); SET @AttendanceId = @AttendanceId + 1;
INSERT INTO [Attendance] ([Id], [TimesheetId], [StudentId], [IsAttenddance], [Note], [CreatedAt], [UpdatedAt], [CreatedBy], [UpdatedBy], [RecordNumber], [IsActive], [IsDeleted])
VALUES 
(@AttendanceId, 8, 40, 1, NULL, GETDATE(), GETDATE(), 9, 9, 1, 1, 0);
GO

SET IDENTITY_INSERT [Attendance] OFF;
GO

-- =============================================
-- VERIFICATION QUERIES
-- =============================================
PRINT '==================================================='
PRINT 'Data Import Summary'
PRINT '==================================================='

SELECT 'Centers' AS [Table], COUNT(*) AS [Total Records] FROM [Centers]
UNION ALL
SELECT 'Roles', COUNT(*) FROM [Roles]
UNION ALL
SELECT 'Users', COUNT(*) FROM [Users]
UNION ALL
SELECT 'Classes', COUNT(*) FROM [Classes]
UNION ALL
SELECT 'ClassStudents', COUNT(*) FROM [ClassStudents]
UNION ALL
SELECT 'Timesheets', COUNT(*) FROM [Timesheets]
UNION ALL
SELECT 'Attendance', COUNT(*) FROM [Attendance]
UNION ALL
SELECT 'Tokens', COUNT(*) FROM [Tokens];

PRINT ''
PRINT '==================================================='
PRINT 'Sample Data by Role'
PRINT '==================================================='

SELECT 
    r.Name AS RoleName,
    COUNT(u.Id) AS UserCount
FROM Roles r
LEFT JOIN Users u ON r.Id = u.RoleId
GROUP BY r.Name
ORDER BY r.Id;

PRINT ''
PRINT 'Fake data import completed successfully!'
PRINT 'Default password for all users: Password123!'
PRINT '==================================================='
GO

