USE QuanLyThuVien;


--***************CẬP NHẬT SỐ LƯỢNG CÁC THAO TÁC+******************

--*****KHO SÁCH KHI NHẬP*********
--CẬP NHẬT SỐ LƯỢNG BẢNG SÁCH KHI LƯU  CHI TIẾT PN 
CREATE or ALTER TRIGGER TRG_SACHNHAP ON CHITIETPN AFTER INSERT AS 
BEGIN
	UPDATE SACH
	SET SACH.SOLUONGHIENTAI = SACH.SOLUONGHIENTAI + (
		SELECT SOLUONGNHAP
		FROM INSERTED
		WHERE MASACH = SACH.MASACH
	)
	FROM SACH
	JOIN INSERTED ON INSERTED.MASACH = SACH.MASACH
END

--BẢNG CUỐN SÁCH KHI LƯU KHI BẢNG CHI TIET PN CÓ MÃ SÁCH VÀ SỐ LƯỢNG.
CREATE OR ALTER TRIGGER TRG_TAO_MA_CUON_SACH
ON CHITIETPN
AFTER INSERT
AS
BEGIN
    DECLARE @MaPN INT;
    DECLARE @MaSach INT;
    DECLARE @SoLuong INT;
    DECLARE @Index INT;
    DECLARE @MaCuon NVARCHAR(10);
    DECLARE @MaChu NVARCHAR(2);
    
    -- Lặp qua từng bản ghi trong bảng inserted
    DECLARE cur CURSOR FOR
    SELECT MAPN, MASACH, SOLUONGNHAP FROM inserted;

    OPEN cur;

    FETCH NEXT FROM cur INTO @MaPN, @MaSach, @SoLuong;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Xác định chữ tương ứng với mã PN
        SET @MaChu = CHAR(64 + @MaPN);  -- A = 1, B = 2, ..., Z = 26

        -- Vòng lặp từ 1 đến số lượng nhập
        SET @Index = 1;
        WHILE @Index <= @SoLuong
        BEGIN
            -- Tạo mã cuốn sách theo định dạng
            SET @MaCuon = CAST(@MaSach AS NVARCHAR(10)) + @MaChu + CAST(@Index AS NVARCHAR(10));

            -- Chèn vào bảng CuonSach
            INSERT INTO CuonSach (MaCuonSach, TinhTrang, MaSach) 
            VALUES (@MaCuon, 0, @MaSach);

            SET @Index = @Index + 1;  -- Tăng chỉ số lên 1
        END;

        FETCH NEXT FROM cur INTO @MaPN, @MaSach, @SoLuong;  -- Lấy bản ghi tiếp theo
    END;

    CLOSE cur;
    DEALLOCATE cur;
END;

--*****SAU KHI MUỢN*********
/* CẬP NHẬT SÁCH TRONG KHO SAU KHI MƯỢN VÀ UPDATE NẾU SÁCH MƯỢN ONL ĐỂ UPDATE THÀNH ĐÃ MƯỢN  */
CREATE OR ALTER  TRIGGER TRG_SACHMUON ON CHITIETPM AFTER INSERT AS 
BEGIN
    DECLARE @Madk INT;

    SELECT TOP 1 @Madk = MADK 
    FROM INSERTED JOIN PHIEUMUON ON INSERTED.MAPM = PHIEUMUON.MAPM;
     
    IF isnull(@Madk,0) = 0
    BEGIN
            PRINT 'Updating SACH';
            -- Nếu Madk = 0, cập nhật kho sách
            UPDATE SACH
            SET SACH.SOLUONGHIENTAI = SACH.SOLUONGHIENTAI - (
                SELECT SOLUONGmuon
                FROM INSERTED
                WHERE MASACH = SACH.MASACH
            )
            FROM SACH
            JOIN INSERTED ON INSERTED.MASACH = SACH.MASACH;
        
    END
	else 
		begin 
			Print 'Update tinhtrangdki = 2'
			UPDATE DkiMuonSach 
			SET TINHTRANG = '2'
			FROM DkiMuonSach 
			where @Madk = DkiMuonSach.MaDK
		end;
END;

/* UPDATE NẾU TÌNH TRẠNG Ở BẢNG CUỐN SÁCH ĐÃ MƯỢN THÀNH CÔNG  */
CREATE OR ALTER TRIGGER TRG_TT_SACHMUONTHANHCONG
ON ChiTietSachMuon 
AFTER INSERT AS
	BEGIN 
		 -- Cập nhật tình trạng của cuốn sách trong bảng CuonSach thành 1 (Đã mượn)
    UPDATE CuonSach
    SET TinhTrang = 1  -- Đã mượn
    FROM CuonSach cs
    INNER JOIN inserted i ON cs.MaCuonSach = i.MaCuonSach;
END;


--*****SAU KHI TRẢ*********
--/* CẬP NHẬT số lượng sách TRONG KHO SAU KHI TRẢ VÀ SÁCH LỖI Ở KHO THANH LÍ*/
CREATE OR ALTER TRIGGER TRG_SACHTRAvaTL ON CHITIETPT AFTER INSERT AS 
BEGIN
    -- Update the current quantity of books in the 'SACH' table after a book is returned
	UPDATE SACH
	SET SACH.SOLUONGHIENTAI = SACH.SOLUONGHIENTAI + (
		SELECT SOLUONGtra
		FROM INSERTED
		WHERE MASACH = SACH.MASACH
	)
	FROM SACH
	JOIN INSERTED ON INSERTED.MASACH = SACH.MASACH;

    -- Update the quantity of books and faulty books in the 'KhoSachThanhLy' table after a book is returned
    DECLARE @MaSach INT;
    DECLARE @soluongLOI INT;

    -- Iterate over the inserted rows
    DECLARE Cursor_SachTra CURSOR FOR
    SELECT MASACH, SOLUONGloi
    FROM INSERTED;

    OPEN Cursor_SachTra;

    FETCH NEXT FROM Cursor_SachTra INTO @MaSach, @soluongLOI;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Check if the book exists in 'KhoSachThanhLy'
        IF EXISTS (SELECT 1 FROM KhoSachThanhLy WHERE masachkho = @MaSach)
        BEGIN
            -- If exists, update the quantity of faulty books
            UPDATE KhoSachThanhLy
            SET soluongkhotl = soluongkhotl + @soluongLOI
            WHERE masachkho = @MaSach;
        END
        ELSE
        BEGIN
            -- If not exists and quantity of faulty books > 0, insert a new row for the book in 'KhoSachThanhLy'
            IF @soluongLOI > 0
            BEGIN
                INSERT INTO KhoSachThanhLy (masachkho, soluongkhotl)
                VALUES (@MaSach, @soluongLOI);
            END;
        END;

        FETCH NEXT FROM Cursor_SachTra INTO @MaSach, @soluongLOI;
    END;

    CLOSE Cursor_SachTra;
    DEALLOCATE Cursor_SachTra;
END;

/* Insert vào bảng chi tiết kho thanh lý sau khi trả có sách lỗi */
CREATE OR ALTER TRIGGER TRG_InsertChiTietKhoThanhLy
ON ChiTietSachTra
AFTER INSERT
AS
BEGIN
    DECLARE @MaSach INT, @MaCuonSach NVARCHAR(10);

    -- Lặp qua các bản ghi vừa được thêm vào bảng ChiTietSachTra
    DECLARE cur CURSOR FOR
    SELECT pt.MaSach, i.MaCuonSach
    FROM inserted i
    JOIN ChiTietPT pt ON i.MaPT = pt.MaPT
    WHERE i.TinhTrang = 2;  -- Chỉ chọn các cuốn sách hư hỏng (TinhTrang = 2)

    OPEN cur;
    FETCH NEXT FROM cur INTO @MaSach, @MaCuonSach;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Kiểm tra xem cuốn sách đã tồn tại trong ChitietKhoThanhLy chưa
        IF NOT EXISTS (SELECT 1 FROM ChitietKhoThanhLy WHERE MaCuonSach = @MaCuonSach)
        BEGIN
            -- Insert nếu chưa tồn tại
            INSERT INTO ChitietKhoThanhLy (MaSachKho, MaCuonSach, VANDE, TINHTRANG)
            VALUES (@MaSach, @MaCuonSach, 2, 0);  -- VANDE = 2 (hư hỏng), TINHTRANG = 0 (chưa thanh lý)
        END

        FETCH NEXT FROM cur INTO @MaSach, @MaCuonSach;
    END;

    CLOSE cur;
    DEALLOCATE cur;
END;



/* UPDATE NẾU TÌNH TRẠNG Ở BẢNG CUỐN SÁCH sau khi trả*/
CREATE OR ALTER  TRIGGER TRG_TT_CUONSACHTRA
ON CHITIETSACHTRA
AFTER INSERT
AS
BEGIN
    -- Cập nhật tình trạng của cuốn sách trong bảng CuonSach thành 0 (CÓ SẴN) khi tình trạng trong ChiTietSachTra là 1 (BÌNH THƯỜNG)
    UPDATE CuonSach
    SET TinhTrang = 0  -- CÓ SẴN
    FROM CuonSach cs
    INNER JOIN inserted i ON cs.MACUONSACH = i.MACUONSACH
    WHERE i.TINHTRANG = 1;--BÌNH THƯỜNG

    -- Cập nhật tình trạng của cuốn sách trong bảng CuonSach thành 2 (HƯ) khi tình trạng trong ChiTietSachTra là 2 (HƯ HỎNG)
    UPDATE CuonSach
    SET TinhTrang = 2  -- HƯ
    FROM CuonSach cs
    INNER JOIN inserted i ON cs.MACUONSACH = i.MACUONSACH
    WHERE i.TINHTRANG = 2;--HƯ

    -- Cập nhật tình trạng của cuốn sách trong bảng CuonSach thành 3 (MẤT) khi tình trạng trong ChiTietSachTra là 3 (MẤT)
    UPDATE CuonSach
    SET TinhTrang = 3  -- MẤT
    FROM CuonSach cs
    INNER JOIN inserted i ON cs.MACUONSACH = i.MACUONSACH
    WHERE i.TINHTRANG = 3;--MẤT
END;


--*****SAU KHI THANH LÍ*********
/* CẬP NHẬT SÁCH TRONG KHO THANH LÍ SAU KHI THANH LÍ  */
CREATE or ALTER TRIGGER TRG_SACHTL ON CHITIETPTL AFTER INSERT AS 
BEGIN
	UPDATE KhoSachThanhLy 
	SET KhoSachThanhLy.SOLUONGKHOTL = KhoSachThanhLy.SOLUONGKHOTL - (
		SELECT Soluongtl
		FROM INSERTED
		WHERE MASACHKHO = KhoSachThanhLy.MASACHKHO
	)
	FROM KhoSachThanhLy 
	JOIN INSERTED ON INSERTED.MASACHKHO = KhoSachThanhLy.MASACHKHO
END

/* CẬP NHẬT TÌNH TRẠNG CHI TIẾT KHO THANH LÍ SAU KHI THANH LÍ*/
CREATE OR ALTER  TRIGGER TinhtrangCTKHOTHANHLI
ON CHITIETSACHTHANHLY 
FOR INSERT AS
BEGIN 
	UPDATE CHITIETKHOTHANHLY 
	SET TINHTRANG = 1
	FROM CHITIETKHOTHANHLY 
	JOIN INSERTED ON INSERTED.MACUONSACH = CHITIETKHOTHANHLY.MACUONSACH
END;


--*****SAU KHI DUYỆT PHIẾU ĐĂNG KÍ ONL*********
/* CẬP NHẬT SÁCH TRONG KHO SAU KHI duyệt hoặc hủy sau khi hết hạn */
CREATE or ALTER TRIGGER TRG_SachMuonOnl ON DkiMuonSach after UPDATE AS 
BEGIN
-- DECLARE @tinhtrang INT;
  DECLARE @tinhtrang_before INT;
    DECLARE @tinhtrang_after INT;

    SELECT TOP 1 @tinhtrang_before = d.tinhtrang, @tinhtrang_after = i.tinhtrang
    FROM DELETED d
    JOIN INSERTED i ON d.madk = i.madk;
    
	--SELECT TOP 1 @tinhtrang_after = tinhtrang 
 --   FROM INSERTED;
	if @tinhtrang_after=1
	begin 
		print @tinhtrang_after;
		UPDATE SACH
		SET SACH.SOLUONGHIENTAI = SACH.SOLUONGHIENTAI - (
			SELECT SOLUONGmuon
			FROM INSERTED JOIN ChiTietDk ON INSERTED.MADK = ChiTietDk.MaDK-- join DkiMuonSach on 
			WHERE ChiTietDk.MASACH = SACH.MASACH AND Tinhtrang = 1
		)
		FROM SACH JOIN ChiTietDk  ON  ChiTietDk.MASACH = SACH.MASACH 
		JOIN INSERTED ON INSERTED.MADK = ChiTietDk.MaDK 
	end
	if @tinhtrang_after =3
	begin
		print @tinhtrang_after;
		UPDATE SACH
		SET SACH.SOLUONGHIENTAI = SACH.SOLUONGHIENTAI + (
			SELECT SOLUONGmuon
			FROM INSERTED JOIN ChiTietDk ON INSERTED.MADK = ChiTietDk.MaDK
			WHERE ChiTietDk.MASACH = SACH.MASACH AND @tinhtrang_after = 3
		)
		FROM SACH JOIN ChiTietDk  ON  ChiTietDk.MASACH = SACH.MASACH 
		JOIN INSERTED ON INSERTED.MADK = ChiTietDk.MaDK 
		join deleted on deleted.MaDK =INSERTED.MADK where   @tinhtrang_before = 1
	end
END




--******************CÁC TRIGGER VỀ TÌNH TRẠNG XÉT DUYỆT*********************
/* CẬP NHẬT TÌNH TRẠNG PHIẾU MƯỢN */
--MẶC ĐỊNH 0 CHƯA TRẢ
--drop TRIGGER SetTinhTrangPM
CREATE OR ALTER  TRIGGER SetTinhTrangPM
ON PHIEUMUON 
FOR INSERT AS
BEGIN 
	UPDATE PHIEUMUON 
	SET TINHTRANG = 0
	FROM PHIEUMUON 
	JOIN INSERTED ON INSERTED.MAPM = PHIEUMUON.MaPM
END;

--CẬP NHẬT TÌNH TRẠNG PM ĐÃ TRẢ=1
CREATE OR ALTER TRIGGER UpdateTinhTrangPhieuMuon
ON chitietpt
AFTER INSERT
AS
BEGIN
-- Kiểm tra số lượng sách trả và số lượng sách mượn
    IF (SELECT COUNT(*) FROM inserted) > 0
		begin 
		   UPDATE PM
			SET Tinhtrang = '1'
			FROM PhieuMuon PM
			JOIN (
				SELECT PM.mapm, count(pm.mapm) as loaisach
				FROM PhieuMuon PM 
				JOIN chitietpm ctpm ON PM.mapm  = ctpm.mapm 
				WHERE PM.Tinhtrang = '0'
				GROUP BY Pm.mapm
				INTERSECT
				SELECT mapm, count(mapm) as soluongls 
				FROM (
					SELECT PM.mapm , ctpm.masach, Soluongmuon
					FROM PhieuMuon PM 
					JOIN chitietpm ctpm ON PM.mapm  = ctpm.mapm 
					WHERE PM.Tinhtrang = '0'
					INTERSECT
					SELECT Pt.mapm,  ctpt.masach, SUM(ISNULL(ctpt.Soluongtra, 0) + ISNULL(ctpt.Soluongloi, 0)+ ISNULL(ctpt.Soluongmat, 0)) AS soluongtra
					FROM phieutra Pt
					JOIN chitietpt ctpt ON Pt.mapt = ctpt.mapt
					LEFT JOIN chitietpm ctpm ON Pt.mapm = ctpm.mapm AND ctpt.masach = ctpm.masach
					GROUP BY Pt.mapm, ctpt.masach
				) AS a
				GROUP BY a.mapm
			) AS CountResult ON PM.mapm = CountResult.mapm
		end
   -- WHERE CountResult.loaisach = CountResult.soluongls;
END;

--/* CẬP NHẬT TÌNH TRẠNG ĐK MƯỢN SÁCH */
--MẶC ĐỊNH =0 CHƯA DUYỆT
CREATE OR ALTER  TRIGGER SetTinhTrangPhieuDK
ON DkiMuonSach 
FOR INSERT AS
BEGIN 
	UPDATE DkiMuonSach 
	SET TINHTRANG = 0
	FROM DkiMuonSach 
	JOIN INSERTED ON INSERTED.MaDK = DkiMuonSach.MaDK
END;




