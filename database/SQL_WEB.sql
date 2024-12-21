﻿--CREATE DATABASE QuanLyThuVien;
GO

USE QuanLyThuVien;
--DROP DATABASE QuanLyThuVien;
CREATE TABLE NhanVien (
	MANV INT PRIMARY KEY IDENTITY NOT NULL ,
	HOTENNV NVARCHAR(50),
	GIOITINH NVARCHAR(50),
	DIACHI NVARCHAR(50),
	NGAYSINH DATE,
	SDT NVARCHAR(50),
	CHUCVU NVARCHAR(50)
);

create TABLE DocGia (
	MADG INT PRIMARY KEY IDENTITY NOT NULL,
	HOTENDG NVARCHAR(50),
	GIOITINH NVARCHAR(50),
	NGAYSINH DATE,
	SDT NVARCHAR(50),
	DIACHI NVARCHAR(50)
);

CREATE TABLE TheDocGia (
	MATHE INT PRIMARY KEY IDENTITY NOT NULL,
	--HANTHE NVARCHAR(50),
	NGAYDK DATE,
	NGAYHH DATE,
	TIENTHE INT,
	MANV INT,
	MADG INT,
	EMAIL NVARCHAR(255),
	FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
	FOREIGN KEY (MADG) REFERENCES DOCGIA (MADG)
);

CREATE TABLE LOGIN_DG (
	SDT NVARCHAR(50) PRIMARY KEY,
	PASSWORD_DG NVARCHAR(255),
	HOTEN NVARCHAR(50),
	EMAIL NVARCHAR(255)
);

CREATE TABLE LOGIN_NV (
	USERNAME_NV NVARCHAR(50) PRIMARY KEY,
	PASSWORD_NV NVARCHAR(MAX),
	MANV INT,
    FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
);

CREATE TABLE NhaCungCap (
	MANCC INT PRIMARY KEY IDENTITY NOT NULL,
	TENNCC NVARCHAR(150),
	DIACHINCC NVARCHAR(100),
	SDTNCC NVARCHAR(50)
);

CREATE TABLE Sach (
	MASACH INT PRIMARY KEY IDENTITY NOT NULL,
	TENSACH NVARCHAR(150),
	THELOAI NVARCHAR(50),
	TACGIA NVARCHAR(50),
	NGONNGU NVARCHAR(50),
	NXB NVARCHAR(100),
	NAMXB INT,
	URL_IMAGE TEXT,
	MOTA NTEXT,
	SOLUONGHIENTAI INT
);

CREATE TABLE CuonSach (
	MACUONSACH NVARCHAR(10) PRIMARY KEY, 
    TINHTRANG INT,--CÓ SẴN=0   ĐÃ MƯỢN = 1. . HƯ =2, MẤT =3 HẾT HẠN =4
    MASACH INT NOT NULL, 
    FOREIGN KEY (MASACH) REFERENCES SACH(MASACH)
);

CREATE TABLE PhieuNhapSach (
	MAPN INT PRIMARY KEY IDENTITY NOT NULL,
	NGAYNHAP DATE,
	MANV INT,
	MANCC INT,
	FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
	FOREIGN KEY (MANCC) REFERENCES NHACUNGCAP (MANCC)
);

CREATE TABLE CHITIETPN(
	MAPN INT,
	MASACH INT,
	GIASACH MONEY,
	SOLUONGNHAP INT,
	FOREIGN KEY (MAPN) REFERENCES PHIEUNHAPSACH (MAPN),
	FOREIGN KEY (MASACH) REFERENCES SACH (MASACH),
	CONSTRAINT CHITIETPN_MAPN_MASACH PRIMARY KEY (MAPN, MASACH)
	);

CREATE TABLE DonViTL (
	MADV INT PRIMARY KEY IDENTITY NOT NULL,
	TENDV NVARCHAR(150),
	DIACHIDV NVARCHAR(100),
	SDTDV NVARCHAR(50)
);

CREATE TABLE PhieuMuon (
	MAPM INT PRIMARY KEY IDENTITY NOT NULL,
	MATHE INT,
	NGAYMUON DATE,
	HANTRA DATE,
	MANV INT,
	TINHTRANG BIT ,
	MADK INT,
	FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
	FOREIGN KEY (MATHE) REFERENCES THEDOCGIA (MATHE)
);

CREATE TABLE ChiTietPM (
	MAPM INT,
	SOLUONGMUON INT,
	FOREIGN KEY (MAPM) REFERENCES PHIEUMUON (MAPM),
	MASACH INT,
	FOREIGN KEY (MASACH) REFERENCES SACH (MASACH),
	CONSTRAINT CHITIETPM_MAPM_MASACH PRIMARY KEY (MAPM, MASACH)
); 


CREATE TABLE ChiTietSachMuon (
    MAPM INT,  -- MÃ PHIẾU MƯỢN (KHÓA NGOẠI TỪ BẢNG PHIEUMUON)
    MACUONSACH NVARCHAR(10),  -- MÃ CUỐN SÁCH CỤ THỂ (1A, 1B,...)
	TINHTRANG bit,  -- TÌNH TRẠNG CỦA CUỐN SÁCH đã mượn hoặc đã trả
    PRIMARY KEY (MAPM, MACUONSACH),
    FOREIGN KEY (MAPM) REFERENCES PHIEUMUON(MAPM),
    FOREIGN KEY (MACUONSACH) REFERENCES CUONSACH(MACUONSACH)
);


CREATE TABLE PhieuTra (
	MAPT INT PRIMARY KEY IDENTITY NOT NULL,
	NGAYTRA DATE,
	MANV INT,
	MATHE INT,
	MAPM INT,
	FOREIGN KEY (MAPM) REFERENCES PHIEUMUON (MAPM),
	FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
	FOREIGN KEY (MATHE) REFERENCES THEDOCGIA (MATHE)
);

CREATE TABLE ChiTietPT (
	MAPT INT,
	MASACH INT,
	SOLUONGTRA INT,
	SOLUONGLOI INT,
	SOLUONGMAT INT,
	PHUTHU MONEY,
	FOREIGN KEY (MAPT) REFERENCES PHIEUTRA (MAPT),
	FOREIGN KEY (MASACH) REFERENCES SACH (MASACH),
	CONSTRAINT CHITIETPT_MAPT_MASACH PRIMARY KEY (MAPT, MASACH)---ID
);

CREATE TABLE ChiTietSachTra (
    MAPT INT,  -- MÃ PHIẾU TRẢ (KHÓA NGOẠI TỪ BẢNG PHIEUTRA)
    MACUONSACH NVARCHAR(10),  -- MÃ CUỐN SÁCH CỤ THỂ
    TINHTRANG INT,  -- TÌNH TRẠNG CỦA CUỐN SÁCH KHI TRẢ (BÌNH THƯỜNG 1, HƯ HỎNG 2, MẤT 3)
    PRIMARY KEY (MAPT, MACUONSACH),
    FOREIGN KEY (MAPT) REFERENCES PHIEUTRA(MAPT),
    FOREIGN KEY (MACUONSACH) REFERENCES CUONSACH(MACUONSACH)
);

CREATE TABLE KhoSachThanhLy (
    MASACHKHO INT PRIMARY KEY, ---SỬA THÀNH MÃ CỦA TỪNG CUỐN HAY GIỮ NGUYÊN NẾU GIỮ NGUYÊN THÌ THIẾU MÃ TỪNG CUỐN 
    SOLUONGKHOTL INT
);

CREATE TABLE ChitietKhoThanhLy (
    MASACHKHO INT,
    MACUONSACH NVARCHAR(10),
    VANDE INT, --sách bị quá hạn hay  2 là hư hỏng
    TINHTRANG INT, --0 là chưa tl, 1 là đã thanh lý
    PRIMARY KEY (MASACHKHO, MACUONSACH),
    FOREIGN KEY (MACUONSACH) REFERENCES CUONSACH(MACUONSACH),
    FOREIGN KEY (MASACHKHO) REFERENCES KHOSACHTHANHLY(MASACHKHO),
    CONSTRAINT UQ_ChitietKhoThanhLy_MaCuonSach UNIQUE (MACUONSACH) -- Thêm ràng buộc UNIQUE
);


CREATE TABLE PhieuThanhLy (
	MAPTL INT PRIMARY KEY IDENTITY NOT NULL,
	NGAYTL DATE,
	MADV INT,
	MANV INT,
	FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV),
	FOREIGN KEY (MADV) REFERENCES DONVITL (MADV)
);

CREATE  TABLE ChiTietPTL (
    MAPTL INT,
	MASACHKHO INT,
	SOLUONGTL INT,
	GIATL MONEY,
    FOREIGN KEY (MAPTL) REFERENCES PHIEUTHANHLY (MAPTL),
	FOREIGN KEY (MASACHKHO) REFERENCES KHOSACHTHANHLY (MASACHKHO), -- THAY VÌ THAM CHIẾU ĐẾN MASACH, THAM CHIẾU ĐẾN ID CỦA KHOSACHTHANHLY
	CONSTRAINT CHITIETPTL_MAPTL_MASACH PRIMARY KEY (MAPTL, MASACHKHO)
);

--ALTER TABLE ChitietKhoThanhLy
CREATE TABLE ChiTietSachThanhLy (
    MAPTL INT,  -- MÃ PHIẾU THANH LÝ (KHÓA NGOẠI TỪ BẢNG PHIEUTHANHLY)
    MACUONSACH NVARCHAR(10),  -- MÃ CUỐN SÁCH CỤ THỂ (1A, 1B,...)
	TINHTRANG INT, --mặc định là hư 1
    PRIMARY KEY (MAPTL, MACUONSACH),  -- KHÓA CHÍNH BAO GỒM MÃ PHIẾU VÀ MÃ CUỐN
    FOREIGN KEY (MAPTL) REFERENCES PHIEUTHANHLY(MAPTL),  -- LIÊN KẾT ĐẾN BẢNG PHIEUTHANHLY
    FOREIGN KEY (MACUONSACH) REFERENCES ChitietKhoThanhLy(MaCuonSach)  -- LIÊN KẾT ĐẾN BẢNG CHITIETKHOTHANHLY

);

CREATE TABLE DkiMuonSach (
	MADK INT PRIMARY KEY IDENTITY NOT NULL,
	SDT NVARCHAR(50),
	NGAYDKMUON DATE,
	NGAYHEN DATE,
	FOREIGN KEY (SDT) REFERENCES LOGIN_DG (SDT),
	TINHTRANG INT --DANG CHỜ XÁC THỰC  0
	--đã duyệt  1
	--đã mượn  2
	--đã hủy 3
);

CREATE TABLE ChiTietDk (
	MADK INT,
	MASACH INT,
	SOLUONGMUON INT,
	FOREIGN KEY (MADK) REFERENCES DKIMUONSACH (MADK),
	FOREIGN KEY (MASACH) REFERENCES SACH (MASACH),
	CONSTRAINT CHITIETDK_MADK_MASACH PRIMARY KEY (MADK, MASACH)
);

CREATE TABLE QuyDinh (
	NamXBMax INT PRIMARY KEY, 
	SosachmuonMax INT,
	SongayMax INT
	
);

CREATE  TABLE ImportSachTemp (
    ID INT IDENTITY(1,1) PRIMARY KEY,       -- Khoá chính tạm thời cho mỗi dòng import
    TENSACH NVARCHAR(150),                 -- Tên sách
    THELOAI NVARCHAR(50),                  -- Thể loại
    TACGIA NVARCHAR(50),                   -- Tác giả
    NGONNGU NVARCHAR(50),                  -- Ngôn ngữ
    NXB NVARCHAR(100),                     -- Nhà xuất bản
    NAMXB INT,                             -- Năm xuất bản
    URL_IMAGE TEXT,                        -- Link ảnh
	GiaSach INT,
    MOTA NTEXT,                            -- Mô tả
    SOLUONG INT,                           -- Số lượng (tạm thời)
    TrangThai NVARCHAR(20),                -- Trạng thái: 'OK' hoặc 'Lỗi'
    MoTaLoi NVARCHAR(255)                  -- Mô tả lỗi (VD: "Trùng mã sách", "Số lượng không hợp lệ")
);

----------------+**************************************************************+----------------
--RANG BUOC NHAN VIEN 
ALTER TABLE NHANVIEN ADD CONSTRAINT CHK_GIOITINH_NV CHECK (GIOITINH IN(N'Nam',N'Nữ'));
ALTER TABLE NHANVIEN ADD CONSTRAINT CHK_SDT_NV   UNIQUE(SDT);
ALTER TABLE NHANVIEN ADD CONSTRAINT CHK_CHUCVU_NV CHECK (CHUCVU IN('ADMIN','QuanLyKho','ThuThu'));
ALTER TABLE NHANVIEN ADD CONSTRAINT CHK_NGAYSINH_NV CHECK (NGAYSINH < GETDATE());


--RANG BUOC DOC GIA
ALTER TABLE DOCGIA ADD CONSTRAINT CHK_GIOITINH_DG CHECK (GIOITINH IN(N'Nam',N'Nữ'));
ALTER TABLE DOCGIA ADD CONSTRAINT CHK_SDT_DG   UNIQUE(SDT);
ALTER TABLE DOCGIA ADD CONSTRAINT CHK_NGAYSINH_DG CHECK (NGAYSINH < GETDATE());

--RANG BUOC THE DOC GIA
ALTER TABLE THEDOCGIA ADD CONSTRAINT CHK_NGAYDK_THEDG CHECK (NGAYDK < NGAYHH);
ALTER TABLE THEDOCGIA ADD CONSTRAINT CHK_TIENTHE_THEDG CHECK (TIENTHE >0 );


--RANG BUOC NCC
ALTER TABLE NHACUNGCAP ADD CONSTRAINT CHK_SDT_NCC   UNIQUE(SDTNCC);

--RANG BUOC PHIEU NHAP SACH
ALTER TABLE PHIEUNHAPSACH ADD CONSTRAINT CHECK_NGAYNHAP_PNS CHECK (NGAYNHAP <= GETDATE());
--ALTER TABLE PHIEUNHAPSACH ADD CONSTRAINT CHECK_TONGSACH_PNS CHECK (TONGSACHNHAP >0);
--ALTER TABLE PHIEUNHAPSACH ADD CONSTRAINT CHECK_TONGTIEN_PNS CHECK (TONGTIEN >0);


--RANG BUOC SACH
ALTER TABLE SACH ADD CONSTRAINT CHK_THELOAI_SACH CHECK (THELOAI IN(N'Truyện ngắn',N'Truyện thiếu nhi',N'Tiểu thuyết', N'Ngôn tình',N'Sách giáo khoa',N'Sách tham khảo', N'Văn học',N'Sách ngoại ngữ',N'Kỹ năng sống'));
ALTER TABLE SACH ADD CONSTRAINT CHK_NGONNGU_SACH CHECK (NGONNGU IN(N'Tiếng anh',N'Tiếng việt',N'Tiếng hàn',N'Tiếng trung',N'Tiếng pháp'));
ALTER TABLE SACH ADD CONSTRAINT CHK_NAMXB_SACH CHECK (NAMXB <= YEAR(GETDATE()));
--ALTER TABLE SACH ADD CONSTRAINT CHK_GIASACH_SACH CHECK (GIASACH >0);
ALTER TABLE SACH ADD CONSTRAINT CHK_SOLUONG_SACH CHECK (SOLUONGHIENTAI >=0);

--RANG BUOC CHI TIET PHIEU NHAP 
ALTER TABLE CHITIETPN ADD CONSTRAINT CHK_SOLUONG_CTPN CHECK (SOLUONGNHAP > 0);
ALTER TABLE CHITIETPN ADD CONSTRAINT CHK_GIASACH_CTPN CHECK (GIASACH >0);

--RANG BUOC DONVITL
ALTER TABLE DONVITL ADD CONSTRAINT CHK_SDT_DVTL   UNIQUE(SDTDV);

--RANG BUOC PHIEU MUON 
ALTER TABLE PHIEUMUON ADD CONSTRAINT CHK_NGAYMUON_PM CHECK (NGAYMUON < HANTRA);
--ALTER TABLE PHIEUMUON ADD CONSTRAINT CHK_PHIEUMUON_Tinhtrang CHECK (Tinhtrang IN(0,1));

--ALTER TABLE PHIEUMUON ADD CONSTRAINT CHK_SOLUONGTONG_PM CHECK (SOLUONGTONG > 0);


--RANG BUOC CHI TIET PHIEU MUON 
ALTER TABLE CHITIETPM ADD CONSTRAINT CHK_SOLUONG_CTPM CHECK (SOLUONGmuon > 0);


--RANG BUOC CHI TIET PHIEU TRA 
ALTER TABLE CHITIETPT ADD CONSTRAINT CHK_SOLUONGT_CTPT CHECK (SOLUONGtra >= 0);
ALTER TABLE CHITIETPT ADD CONSTRAINT CHK_SOLUONGL_CTPT CHECK (SOLUONGloi >= 0);
ALTER TABLE CHITIETPT ADD CONSTRAINT CHK_PHUTHU_CTPT CHECK (PhuThu >= 0);
--ALTER TABLE CHITIETPT ADD CONSTRAINT CHK_SOLUONG_CTPT CHECK (SOLUONGloi+SOLUONGtra = SOLUONGmuon);

--RANG BUOC KHO SACH THANH LY
ALTER TABLE KHOSACHTHANHLY ADD CONSTRAINT CHK_SOLUONG_KHOSACH CHECK (SOLUONGKHOTL >=0);

--RANG BUOC PHIEU THANH LY
ALTER TABLE PHIEUTHANHLY ADD CONSTRAINT CHK_NgayTL_PTL CHECK(NgayTL<= GETDATE());
--ALTER TABLE PHIEUTHANHLY ADD CONSTRAINT CHK_TongSachTL_PTL CHECK(TongSachTL>0);

--RANG BUOC CHI TIET PHIEU THANH LY 
ALTER TABLE CHITIETPTL ADD CONSTRAINT CHK_SOLUONG_CTPTL CHECK (SOLUONGtL > 0);
ALTER TABLE CHITIETPTL ADD CONSTRAINT CHK_GiaTL_CTPTL CHECK (GiaTL > 0);


--RANG BUOC phieu dk
ALTER TABLE DkiMuonSach ADD CONSTRAINT CHK_NGAYMUON_DK CHECK (NgayDKMuon <= NgayHen);
ALTER TABLE DkiMuonSach add CONSTRAINT CHK_TINHTRANG_DK CHECK (Tinhtrang IN (0,1, 2,3));


--ALTER TABLE PHIEUMUON ADD CONSTRAINT CHK_PHIEUMUON_Tinhtrang CHECK (Tinhtrang IN(0,1));

--ALTER TABLE PHIEUMUON ADD CONSTRAINT CHK_SOLUONGTONG_PM CHECK (SOLUONGTONG > 0);


--RANG BUOC CHI TIET PHIEU MUON 
ALTER TABLE CHITIETDK ADD CONSTRAINT CHK_SOLUONG_CTDK CHECK (SOLUONGmuon > 0);

--RANG BUOC login dg
ALTER TABLE Login_DG ADD CONSTRAINT CHK_SDT_LOGIN_DG   UNIQUE(SDT);
ALTER TABLE Login_DG ADD CONSTRAINT CHK_EMAIL_LOGIN_DG   UNIQUE(EMAIL);

--Rang buoc sach gio hang khong am


--******************TẠO VIEW CHECK TỔNG TIEN *********************
--CREATE or alter VIEW PHIEUNHAP_VIEW 
--AS
--	SELECT PN.MAPN, NgayNhap, MaNV, MaNCC, SUM(ABS(S.GIASACH*SOLUONGNHAP)) AS N'TỔNG TIỀN', SUM(SOLUONGNHAP) AS N'TỔNG SÁCH'
--	FROM PHIEUNHAPSACH PN JOIN CHITIETPN S ON PN.MAPN= S.MAPN JOIN SACH ON SACH.MaSach = S.MaSACH
--	GROUP BY PN.MAPN, NgayNhap, MaNV, MaNCC;
		

--CREATE or alter VIEW PHIEUTra_VIEW 
--AS
--	SELECT pt.mapt,MaPM, MaThe, NgayTra, MaNV, SUM(PhuThu) AS N'Tổng tiền xử lý'
--	FROM PhieuTra Pt JOIN CHITIETPt ctpt ON Pt.MAPt= ctpt.MAPt JOIN SACH ON SACH.MaSach = ctpt.MaSACH
--	GROUP BY pt.mapt,MaPM, MaThe, NgayTra, MaNV;


--CREATE or alter VIEW PHIEUThanhLy_VIEW 
--AS
--	SELECT ptl.maptl, Madv, NgayTL, MaNV, SUM(GiaTL*Soluongtl) AS N'Tổng tiền thanh lý'
--	FROM PhieuThanhLy Ptl JOIN CHITIETPtl ctptl ON Ptl.MAPtl= ctptl.MAPtl JOIN SACH ON SACH.MaSach = ctptl.MaSACHkho
--	GROUP BY ptl.maptl, Madv, NgayTL, MaNV;


--CREATE or alter VIEW DocGia_VIEW 
--AS
--	SELECT docgia.MaDG, HoTenDG, sdt,manv, SUM(TienThe) AS N'Tổng tiền khách hàng'
--	FROM DocGia join TheDocGia on DocGia.MaDG = TheDocGia.MaDG
--	GROUP BY docgia.MaDG, HoTenDG, sdt,manv;





----	-----------------******************************-------------------
--CREATE  PROCEDURE pro_UpdateTinhTrangHuyDkiMuon
--AS
--BEGIN
--    -- Thực hiện cập nhật tình trạng của bảng
--    UPDATE DkiMuonSach
--    SET Tinhtrang = '3'
--    WHERE NgayHen <= DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), -1) and Tinhtrang in(0,1);
--END


--CREATE TABLE BackupStoredProcedures (
--    BackupID INT IDENTITY(1,1) PRIMARY KEY,
--    ProcName NVARCHAR(128),
--    BackupDate DATETIME,
--    ProcDefinition NVARCHAR(MAX)
--);




--insert into PhieuNhapSach ( NgayNhap,  MaNV, MaNCC) values ( '2023-10-15',  3,  1);
--insert into Sach ( TenSach, TheLoai, TacGia, NgonNgu, NXB, NamXB, GiaSach, SoLuong, MaPN) values (N'Thỏ Bảy Màu',N'Truyện thiếu nhi',N'HUỲNH THÁI NGỌC',N'Tiếng việt',N'Dân Trí', 2023, '55000', 20,4);
--insert into Sach ( TenSach, TheLoai, TacGia, NgonNgu, NXB, NamXB, GiaSach, SoLuong, MaPN) values (N'Thần Đồng Đất Việt 2 - Trí Nhớ Siêu Phàm',N'Truyện thiếu nhi',N'Nhiều Tác Giả',N'Tiếng việt',N'NXB Đại Học Sư Phạm', 2023, '100000', 30,4);
--insert into Sach ( TenSach, TheLoai, TacGia, NgonNgu, NXB, NamXB, GiaSach, SoLuong, MaPN) values (N'Mùa Hè CỦA HỒ LY',N'TIỂU THUYẾT',N'Nguyễn Nhật Ánh',N'Tiếng việt',N'NXB Trẻ', 2023, '50000', 20,4);

----------------+**************************************************************+----------------


---- Them Phieu thanh ly
--INSERT INTO PHIEUTHANHLY (MADV, NGAYTL,  MANV) VALUES (1, '2024-08-25',  3);


---- Them Chi tiet phieu thanh ly 
--INSERT INTO CHITIETPTL (MAPTL, MASACHKHO, SOLUONGTL, GIATL) VALUES (1, 17, 2, 67500);
--INSERT INTO CHITIETPTL (MAPTL, MASACHKHO, SOLUONGTL, GIATL) VALUES (1 ,15, 1,33500);