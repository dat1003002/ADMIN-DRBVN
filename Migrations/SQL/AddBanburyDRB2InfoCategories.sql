SET IDENTITY_INSERT Categories ON;

INSERT INTO Categories (CategoryId, CategoryName, Description, Tenxuong, CreatedAt)
SELECT v.Id, v.Name, v.Name, N'MS', SYSDATETIME()
FROM (VALUES
    (48, N'Phiếu an toàn hóa chất Banbury DRB2'),
    (49, N'5S Banbury DRB2'),
    (50, N'An Toàn Banbury DRB2'),
    (51, N'Sơ đồ tổ chức Banbury DRB2'),
    (52, N'Thông báo quan trọng Banbury DRB2'),
    (53, N'Tỷ lệ vận hành máy Banbury DRB2'),
    (54, N'Tỷ lệ chuyên cần Hỗ Trợ Sản Xuất Banbury DRB2'),
    (55, N'Tỷ lệ phế thải Hỗ Trợ Sản Xuất Banbury DRB2'),
    (56, N'Năng xuất người/ giờ Banbury DRB2'),
    (57, N'Hình ảnh nguyên liệu Banbury DRB2')
) AS v(Id, Name)
WHERE NOT EXISTS (SELECT 1 FROM Categories c WHERE c.CategoryId = v.Id);

SET IDENTITY_INSERT Categories OFF;
