USE QuanLyThuVien;

-- THEM NHAN VIEN
INSERT INTO NHANVIEN ( HOTENNV, GIOITINH, DIACHI, NGAYSINH, SDT, CHUCVU) VALUES ( N'Võ Gia Huy',		'Nam', N'Bình Định',  '1989-07-02', '0767163999', 'Admin');
INSERT INTO NHANVIEN ( HOTENNV, GIOITINH, DIACHI, NGAYSINH, SDT, CHUCVU) VALUES ( N'Nguyễn Thị Kim Liên',N'Nữ',N'Bình Thuận', '2000-10-15', '0319267184', 'ThuThu');
INSERT INTO NHANVIEN ( HOTENNV, GIOITINH, DIACHI, NGAYSINH, SDT, CHUCVU) VALUES ( N'Nguyễn Thành Luân', 'Nam', N'Bình Định',  '2003-06-20', '0231525416', 'QuanLyKho');
INSERT INTO NHANVIEN ( HOTENNV, GIOITINH, DIACHI, NGAYSINH, SDT, CHUCVU) VALUES ( N'ALuân',				'Nam', N'Bình Định',  '2003-06-20', '1234567892', 'Admin');


-- THEM DOC GIA
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Nguyễn Văn A',N'Nam', '1996-07-02', '0981724637', N'Gò Vấp');    --1
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Phạm Minh B', N'Nam', '2004-12-07', '0466155193', N'Thủ Đức');   --2
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Đinh Thị C',  N'Nữ',  '2006-11-01', '0596965300', N'Bình Thạnh');--3
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Đặng Thùy D', N'Nữ',  '1993-10-24', '0305728822', N'Gò Vấp');    --4
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Võ Thị E',	   N'Nữ',  '2004-01-19', '0251184897', N'Quận 12');	  --5
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Phan Ngọc F', N'Nữ',  '2004-01-11', '0396629886', N'Quận 1');	  --6
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Đặng Quang G',N'Nam', '2005-09-06', '0745717832', N'Thủ Đức');   --7
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Huỳnh Văn H', N'Nam', '1995-10-08', '0317809370', N'Thủ Đức');   --8
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Cao Hoàng I', N'Nam', '2003-09-05', '0638103599', N'Thủ Đức');	  --9
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Đỗ Thị J',    N'Nữ',  '2003-09-06', '0727180418', N'Quận 2');    --10

INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Cao Hoàng K', N'Nam', '2003-09-15', '0638103555', N'Thủ Đức');--11
INSERT INTO DOCGIA ( HOTENDG, GIOITINH, NGAYSINH, SDT, DIACHI) VALUES (N'Đỗ Thị L',	   N'Nữ',  '2003-04-26', '0727180442', N'Quận 3'); --12


-- THEM THE DOC GIA
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-02-22', '2024-05-22', '90000',  2, 1, 'Nguyenvana123@gmail.com');--1
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-04-08', '2024-12-08', '240000', 2, 2, 'Phamminhb@gmail.com');	--2
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-10-12', '2025-10-12', '360000', 2, 3, 'Dinhthic123@gmail.com');	--3
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-02-20', '2024-07-20', '150000', 2, 4, 'Dangthuyd@gmail.com');    --4
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-05-15', '2024-11-15', '180000', 2, 5, 'Vothie@gmail.com');		--5
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-02-01', '2024-07-01', '150000', 2, 6, 'Phanngocf@gmail.com');	--6
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-04-22', '2024-10-22', '180000',  2, 7, 'Dangquangg@gmail.com');	--7
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-06-02', '2024-12-02', '180000',  2, 8, 'Huynhvanh@gmail.com');	--8
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-01-10', '2025-01-10', '360000', 2, 9, 'Caohoangi@gmail.com');	--9
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-05-25', '2024-11-25', '180000', 2, 10,'Dothij@gmail.com');		--10
															 													  
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-05-19', '2025-12-19', '210000', 2, 11,'Caohoangk@gmail.com');    --11
INSERT INTO THEDOCGIA ( NGAYDK, NGAYHH, TIENTHE, MANV, MADG, EMAIL) VALUES ( '2024-08-04', '2025-08-04', '360000', 2, 12,'Dothil@gmail.com');		--12


--THEM LOGIN DG
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0981724637', '0981724637', N'Nguyễn Văn A',   'Nguyenvana123@gmail.com');    --1
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0466155193', '0466155193', N'Phạm Minh B',    'Phamminhb123@gmail.com');		--2
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0964965332', '0964965332', N'Nguyễn Văn Bách','Nguyenvanbach123@gmail.com'); --3
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0969728877', '0969728877', N'Trần Uyển Ân',   'Tranuyenan123@gmail.com');	--4
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0471184811', '0471184811', N'Hải Đường',      'Haiduong123@gmail.com');		--5
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0727180418', '0727180418', N'Đỗ Thị J',       'Dothij123@gmail.com');		--6
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0638103599', '0638103599', N'Cao Hoàng I',    'Caohoangi123@gmail.com');		--7
INSERT INTO LOGIN_DG (SDT, PASSWORD_DG, HOTEN, EMAIL) VALUES ( '0471184852', '0471184852', N'Hải Đường',      'Vanngocanh123@gmail.com');	--8

--THEM LOGIN NV
INSERT INTO LOGIN_NV (USERNAME_NV, PASSWORD_NV, MANV) VALUES ('0767163999',	'0767163999',1); 
INSERT INTO LOGIN_NV (USERNAME_NV, PASSWORD_NV, MANV) VALUES ('0319267184',	'0319267184',2);
INSERT INTO LOGIN_NV (USERNAME_NV, PASSWORD_NV, MANV) VALUES ('0231525416',	'0231525416',3);
INSERT INTO LOGIN_NV (USERNAME_NV, PASSWORD_NV, MANV) VALUES ('123',		'123',		 4);


-- THEM NHACUNGCAP
INSERT INTO NHACUNGCAP ( TENNCC, DIACHINCC, SDTNCC) VALUES (N'Nhà Xuất Bản Kim Đồng',						 N'Hồ gươm, Hà Nội',					   '0243942633'); --1
INSERT INTO NHACUNGCAP ( TENNCC, DIACHINCC, SDTNCC) VALUES (N'CÔNG TY CỔ PHẦN SÁCH VÀ VĂN HÓA PHẨM MIỀN NAM',N'Quận Phú Nhuận, Thành phố Hồ Chí Minh', '0961719819'); --2
INSERT INTO NHACUNGCAP ( TENNCC, DIACHINCC, SDTNCC) VALUES (N'CÔNG TY SÁCH DÂN TRÍ',						 N'Quận Phú Nhuận, Thành phố Hồ Chí Minh', '0862751674'); --3
INSERT INTO NHACUNGCAP ( TENNCC, DIACHINCC, SDTNCC) VALUES (N'Nhà sách Nhã Nam',							 N'Đống Đa, Hà Nội',					   '0246259345'); --4
																																									  

-- Them Sach
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Thỏ Bảy Màu Và Những Người Nghĩ Nó Là Bạn',N'Truyện thiếu nhi',N'HUỲNH THÁI NGỌC',N'Tiếng việt',N'Dân Trí', 2023, '~\img_web\Tho_bay_mau.jpg',N'Thỏ Bảy Màu là fanpage sở hữu hơn 2,6tr lượt thích trên mạng xã hội. 
Với hình tượng nhân vật thú vị cùng phong cách sáng tạo độc đáo, Thỏ bảy màu vẫn luôn là thu hút được số lượng lớn người quan tâm thể hiện qua nhiều bài viết với hàng chục nghìn lượt like và share.
Thỏ Bảy Màu là một nhân vật hư cấu chẳng còn xa lạ gì với anh em dùng mạng xã hội với slogan “Nghe lời Thỏ, kiếp này coi như bỏ!”.
Thỏ Bảy Màu đơn giản chỉ là một con thỏ trắng với sự dở hơi, ngang ngược nhưng đáng yêu vô cùng tận. Nó luôn nghĩ rằng mình không có cuộc sống và không có bạn bè.
Tuy nhiên, Thỏ lại chẳng bao giờ thấy cô đơn vì đến cô đơn cũng bỏ nó mà đi.
Cuốn sách là những mẩu chuyện nhỏ được ghi lại bằng tranh xoay quanh Thỏ Bảy Màu và những người nghĩ nó là bạn. 
Những mẩu chuyện được truyền tải rất “teen” đậm chất hài hước, châm biếm qua sự sáng tạo không kém phần “mặn mà”
của tác giả càng trở nên độc đáo và thu hút.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Thần Đồng Đất Việt 2 - Trí Nhớ Siêu Phàm',N'Truyện thiếu nhi',N'Nhiều Tác Giả',N'Tiếng việt',N'NXB Đại Học Sư Phạm', 2023, '~\img_web\ThanDongDatViet2_TNSP.jpg','Trí Nhớ Siêu Phàm Chỉ vô tình xem qua 1 lần sổ nợ của quán ăn Tám Tiền ở làng Phan Thị. 
Bằng trí nhớ siêu phàm, Trạng Tí đã giúp chủ quán thoát khỏi họa phá sản! 
Thông qua câu chuyện người xem sẽ làm quen với vị Bảng nhãn Lê Quý Đôn - nhà bác học lỗi lạc của văn hóa Việt Nam!', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Mùa Hè Không Tên',N'Tiểu thuyết',N'Nguyễn Nhật Ánh',N'Tiếng việt',N'NXB Trẻ', 2023,'~\img_web\MuaHeKhongTen.jpg',N'“Mùa hè không tên” là truyện dài mới nhất của nhà văn Nguyễn Nhật Ánh, với những câu chuyện tuổi thơ với vô số trò tinh nghịch, 
những thoáng thinh thích hồi hộp cùng vô vàn kỷ niệm. Để rồi khi những tháng ngày trong sáng của tình bạn dần qua, bọn nhỏ trong mỗi gia đình bình dị lớn lên cùng chứng kiến những giây phút cảm động của câu chuyện tình thân,
nỗi khát khao hạnh phúc êm đềm, cùng bỡ ngỡ bước vào tuổi lớn nhiều yêu thương mang cả màu va vấp.
Mùa hè năm ấy của cậu bé Khang không chỉ toàn chuyện leo cây hái trái và qua lại với con Nhàn hồn hậu đáng yêu ưa nuôi bọn cá dị tật, mà có Tí, có Chỉnh, rồi Túc, Đính… phải đối mặt với những thử thách của số phận. Nhưng vì sao là “mùa hè không tên”?
“Đó là mùa hè thật đặc biệt với tôi. Sau mùa hè đó, cuộc sống của tôi đã thay đổi mãi mãi.
Vì vậy tôi muốn đặt cho nó một cái tên để nó không giống với những mùa hè khác trong đời tôi mỗi khi tôi nhớ về. Tôi định gọi nó là mùa hè chia tay, mùa hè ưu tư, mùa hè định mệnh, 
hay sến sẩm một chút là mùa hè có mây tím bay nhưng rồi tôi thấy không cái tên nào thật sự phù hợp. Cuối cùng, tôi nghĩ nếu cần phải có một cái tên thì tôi sẽ đặt tên cho nó là mùa hè không tên.
Ờ, mùa hè đặc biệt của tôi cần gì phải khoác một cái tên riêng khi mà mỗi lần đầu óc tôi quay ngược về thời kỳ đó, tôi luôn thấy lòng đầy xáo trộn. 
Nó đã khắc lên số phận tôi những dấu vết không thể phai mờ - như vết chàm mà con người ta phải mang theo cho đến tận cuối đời.” (Trích)
Nhà văn Nguyễn Nhật Ánh vốn nổi tiếng qua nhiều thế hệ bạn đọc với nhiều tác phẩm đi vào lòng người. 
Với tác phẩm này, ông vẫn luôn giữ thông điệp khơi dậy khao khát sống đẹp, sống tử tế nơi người đọc.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Đất Rừng Phương Nam',N'Văn học',N'Đoàn Giỏi',N'Tiếng việt',N'Kim Đồng', 2023, '~\img_web\DatRungPhuongNam.jpg',N'Cuộc đời lưu lạc của chú bé An qua những miền đất rừng phương Nam thời kì đầu cuộc kháng chiến chống Pháp. 
Một vùng đất trù phú, đa dạng, kì vĩ với những kênh rạch, tôm cá, chim chóc, muông thú, lúa gạo... và cây cối, rừng già. Trong thế giới đó có những con người vô cùng nhân hậu như cha mẹ nuôi của bé An, 
như cậu bé Cò, chú Võ Tòng... cùng những người anh em giàu lòng yêu quê hương, đất nước. 
Cuộc sống tự do và cuộc đời phóng khoáng cởi mở đã để lại ấn tượng sâu sắc trong tâm khảm người đọc nhiều thế hệ suốt những năm tháng qua',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Ông Già Và Biển Cả',N'Văn học',N'Lê Huy Bắc dịch',N'Tiếng việt',N'NXB Văn học', 2022, '~\img_web\OngGiaVaBienCa.jpg',N'Ông Già Và Biển Cả là một trong những tác phẩm nổi tiếng nhất của Hemingway, sử dụng nguyên lý “tảng băng trôi”
khi kể về một cuộc săn đuổi con cá kiếm khổng lồ của ông lão đánh cá Santiago, 
người đã cố gắng chiến đấu trong ba ngày đêm vật lộn trên biển vùng Giếng Lớn khi ông câu được nó. Sang đến ngày thứ ba, ông dùng lao đâm chết được con cá, 
buộc nó vào mạn thuyền và lôi về nhưng đàn cá mập đánh hơi thấy đã lăn xả tới, ông lại đem hết sức tàn chống chọi với lũ cá mập, phóng lao, 
thậm chí cả mái chèo để đánh. Ông giết được nhiều con, đuổi được chúng đi, nhưng cuối cùng khi nhìn đến con cá kiếm của mình thì nó đã bị rỉa hết thịt chỉ còn trơ lại một bộ xương khổng lồ. 
Tác phẩm là bản anh hùng ca ca ngợi sức lao động và khát vọng của con người.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Destination B1 Grammar And Vocabulary with Answer Key',N'Sách ngoại ngữ',N'Malcome Mann, Steve Taylore-Knowles',N'Tiếng anh', N'Hồng Đức', 2023, '~\img_web\Destination_B1_GrammarAndVocabularywithAnswerKey.jpg',N'Bộ sách cung cấp từ vựng và ngữ pháp tiếng Anh cần thiết nhất dành cho người học đang có ý định thi các kỳ thi ở Level B1, B2, C1, C2 theo Khung tham chiếu châu Âu và mong muốn cải thiện năng lực tiếng Anh của bản thân.
Trong mỗi cuốn sách sẽ bao gồm:
- Từ vựng, ngữ pháp theo từng trình độ
- Các bài review và bài test theo lộ trình
- Hệ thống bài tập đa dạng, luyện tập sâu
- Các bài tự kiểm tra, đánh giá mức độ hiểu kiến thức', 0); 
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Giáo Trình Chuẩn HSK 1 ',N'Sách ngoại ngữ',N'Khương Lệ Bình',N'Tiếng trung',N'NXB Tổng Hợp TPHCM', 2022, '~\img_web\GiaoTrinhChuanHSK1.jpg',N'Được chia thành 6 cấp độ với tổng cộng 18 cuốn, Giáo trình chuẩn HSK có những đặc điểm nổi bật sau:
• Kết hợp thi cử và giảng dạy: Được biên soạn phù hợp với nội dung, hình thức cũng như các cấp độ của đề thi HSK thật, bộ sách này có thể được sử dụng đồng thời cho cả hai mục đích là giảng dạy tiếng Trung Quốc và luyện thi HSK.
• Bố cục chặt chẽ và khoa học: Các điểm ngữ pháp được giải thích cặn kẽ, phần ngữ âm và chữ Hán được trình bày từ đơn giản đến phức tạp theo từng cấp độ.
• Đề tài quen thuộc, nhiều tình huống thực tế: Bài học được thiết kế không quá dài và đề cập đến nhiều tình huống (có file MP3 kèm theo), giúp bạn rèn luyện các kỹ năng ngôn ngữ và tránh cảm giác căng thẳng trong lúc học. 
• Cách viết thú vị: Bằng cách viết sinh động kèm nhiều hình ảnh minh họa, tác giả bộ sách chỉ cho bạn thấy học tiếng Trung Quốc không hề khô khan, nhàm chán.
Với nhiều ưu điểm nổi bật như vừa nêu, Giáo trình chuẩn HSK không chỉ là 
tài liệu giảng dạy hữu ích ở các trung tâm dạy tiếng Trung Quốc mà còn rất thích hợp với những người muốn tự học ngôn ngữ này.',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'450 Nouveaux Exercices - Vocabulaire Niveau débutant',N'Sách ngoại ngữ',N'Thierry Gallier',N'Tiếng pháp',N'NXB Tổng Hợp TPHCM',2011, '~\img_web\450NouveauxExercicesVocabulaireNiveaudebutant.jpg',N'450 Nouveaux Exercices - Vocabulaire Niveau débutant
', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Sinh Học 11 (Chân Trời Sáng Tạo)',N'Sách giáo khoa',N'Nhiều Tác Giả',N'Tiếng việt',N'Giáo Dục Việt Nam', 2023, '~\img_web\SinhHoc11(CTST).jpg','Để chuẩn bị cho năm học mới 2023 - 2024, thì Nhà xuất bản Giáo dục Việt Nam đã chính thức công bố Bộ SGK Lớp 11
- Chân trời sáng tạo tới các giáo viên, học sinh, bậc phụ huynh và các trường học hiện nay. Toàn bộ nội dung trong bộ sách này, sẽ được cập nhật nội dung hoàn toàn mới nhất dành cho học sinh cũng như thầy cô giáo.
Bộ sách Chân trời sáng tạo hàm ẩn ý nghĩa về sự rộng mở của một thế giới tri thức, sự vô hạn của kiến thức khoa học và công nghệ, 
sự bao la của thế giới nghệ thuật và hướng đến những giá trị tinh thần tốt đẹp của nhân loại.',0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Tiếng anh Lớp 3 - Family And Friends (National Edition) - Workbook (2023)',N'Sách giáo khoa',N'Trần Cao Bội Ngọc', N'Tiếng anh',N'Giáo Dục Việt Nam', 2023,'~\img_web\TAL3_FamilyAndFriends(National Edition)_Workbook(2023).jpg',N'Sách Tiếng Anh 3 Family and Friends National Edition gồm 12 đơn vị bài học và bài mở đầu "Starter". 
Mỗi bài học gồm 06 "Lessons": Words, Grammar, Song, Phonics, và hai phần Skills Time. Sử dụng dữ liệu gốc từ các bộ sách tiếng Anh của Nhà xuất bản Đại học Oxford -
từ nhữ mang tính ứng dụng cao.',  0);



INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Toán 4 - Tập 1 (Chân Trời Sáng Tạo)',N'Sách giáo khoa',N'Nhiều Tác Giả',N'Tiếng việt',N'Giáo Dục Việt Nam', 2023,  '~\img_web\Toan4_Tap1(CTST).jpg',N'Để chuẩn bị cho năm học mới 2023 - 2024, 
thì Nhà xuất bản Giáo dục Việt Nam đã chính thức công bố Bộ SGK Lớp 4 - 
Chân trời sáng tạo tới các giáo viên, học sinh, bậc phụ huynh và các trường học hiện nay. Toàn bộ nội dung trong bộ sách này, sẽ được cập nhật nội dung hoàn toàn mới nhất dành cho học sinh cũng như thầy cô giáo. 
Bộ sách Chân trời sáng tạo hàm ẩn ý nghĩa về sự rộng mở của một thế giới tri thức, 
sự vô hạn của kiến thức khoa học và công nghệ, sự bao la của thế giới nghệ thuật và hướng đến những giá trị tinh thần tốt đẹp của nhân loại.',0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Công Nghệ 8 (Chân Trời Sáng Tạo)',N'Sách giáo khoa',N'Nhiều Tác Giả',N'Tiếng việt',N'Giáo Dục Việt Nam', 2023, '~\img_web\CN8(CTST).jpg',N'Để chuẩn bị cho năm học mới 2023 - 2024, 
thì Nhà xuất bản Giáo dục Việt Nam đã chính thức công bố Bộ SGK Lớp 8 - 
Chân trời sáng tạo tới các giáo viên, học sinh, bậc phụ huynh và các trường học hiện nay. Toàn bộ nội dung trong bộ sách này, 
sẽ được cập nhật nội dung hoàn toàn mới nhất dành cho học sinh cũng như thầy cô giáo. Bộ sách Chân trời sáng tạo hàm ẩn ý nghĩa về sự rộng mở của một thế giới tri thức, sự vô hạn của kiến thức khoa học và công nghệ, 
sự bao la của thế giới nghệ thuật và hướng đến những giá trị tinh thần tốt đẹp của nhân loại.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Thay Đổi Cuộc Sống Với Nhân Số Học',N'Kỹ năng sống',N'Lê Đỗ Quỳnh Hương',N'Tiếng việt',N'NXB Tổng Hợp TPHCM', 2020, '~\img_web\ThayDoiCSVoiNhanSoHoc.jpg',N'Cuốn sách Thay đổi cuộc sống với Nhân số học là tác phẩm được chị Lê Đỗ Quỳnh Hương phát triển từ tác phẩm gốc
“The Complete Book of Numerology” của tiến sỹ David A. Phillips, 
khiến bộ môn Nhân số học khởi nguồn từ nhà toán học Pythagoras trở nên gần gũi, dễ hiểu hơn với độc giả Việt Nam.',0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Đứa Trẻ Hiểu Chuyện Thường Không Có Kẹo Ăn',N'Kỹ năng sống',N'Nguyên Anh',N'Tiếng việt',N'Văn học', 2022,'~\img_web\DuaTreHieuChuyenThuongKhongCoKeoAn.jpg',N'“Đứa trẻ hiểu chuyện thường không có kẹo ăn” –
Cuốn sách dành cho những thời thơ ấu đầy vết thương.',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Cây Cam Ngọt Của Tôi',N'Tiểu thuyết',N'José Mauro de Vasconcelos',N'Tiếng việt',N'NXB Hội Nhà Văn', 2020,'~\img_web\CayCamNgotCuaToi.jpg',N'“Vị chua chát của cái nghèo hòa trộn với vị ngọt ngào khi 
khám phá ra những điều khiến cuộc đời này đáng sống... một tác phẩm kinh điển của Brazil.” - Booklist
“Một cách nhìn cuộc sống gần như hoàn chỉnh từ con mắt trẻ thơ… có sức mạnh sưởi ấm và làm tan nát cõi lòng, dù người đọc ở lứa tuổi nào.” - The National',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Nhà Giả Kim',N'Tiểu thuyết',N'Paulo Coelho',N'Tiếng việt',N'NXB Hội Nhà Văn', 2020,'~\img_web\NhaGiaKim.jpg',N'“Nhưng nhà luyện kim đan không quan tâm mấy đến những điều ấy. 
Ông đã từng thấy nhiều người đến rồi đi, trong khi ốc đảo và sa mạc vẫn là ốc đảo và sa mạc. Ông đã thấy vua chúa và kẻ ăn xin đi qua biển cát này, 
cái biển cát thường xuyên thay hình đổi dạng vì gió thổi nhưng vẫn mãi mãi là biển cát mà ông đã biết từ thuở nhỏ. Tuy vậy, tự đáy lòng mình, ông không thể không cảm thấy vui trước hạnh phúc của mỗi người lữ khách,
sau bao ngày chỉ có cát vàng với trời xanh nay được thấy chà là xanh tươi hiện ra trước mắt. 
‘Có thể Thượng đế tạo ra sa mạc chỉ để cho con người biết quý trọng cây chà là,’ ông nghĩ.”
- Trích Nhà giả kim',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Cho Tôi Xin Một Vé Đi Tuổi Thơ',N'Tiểu thuyết',N'Nguyễn Nhật Ánh',N'Tiếng việt',N'	NXB Trẻ', 2023, '~\img_web\ChoToiXin1VeDiTuoiTho.jpg',N'Truyện Cho tôi xin một vé đi tuổi thơ là sáng tác mới nhất của nhà văn Nguyễn Nhật Ánh. 
Nhà văn mời người đọc lên chuyến tàu quay ngược trở lại thăm tuổi thơ và tình bạn dễ thương của 4 bạn nhỏ. Những trò chơi dễ thương thời bé, tính cách thật thà, thẳng thắn một cách thông minh và dại dột,
những ước mơ tự do trong lòng… khiến cuốn sách có thể làm các bậc phụ huynh lo lắng rồi thở phào. Không chỉ thích hợp với người đọc trẻ, 
cuốn sách còn có thể hấp dẫn và thực sự có ích cho người lớn trong quan hệ với con mình.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Tam Quốc Diễn Nghĩa',N'Văn học',N'La Quán Trung',N'Tiếng việt',N'NXB Văn học', 2020,'~\img_web\TamQuocDienNghia.jpg',N'Tam Quốc diễn nghĩa là pho tiểu thuyết lịch sử ưu tú của nền văn học cổ Trung Quốc được độc giả khắp thế giới yêu thích, say mê. Ở nước ta trước đây,
Tam Quốc diễn nghĩa đã được dịch ra nhiều bản, trong số đó bản của cụ cử Phan Kế Bính được hoan nghênh hơn cả. Tiếc rằng bản dịch này dựa theo nguyên bản Tam Quốc diễn nghĩa cũ, trong đó có những điểm không được chính xác. Trong bản in tác phẩm này của NXB Phổ Thông năm 1959, 
cụ phó bảng Bùi Kỷ đã được mời tham gia hiệu đính bằng cách đem đối chiếu với bộ Tam quốc diễn nghĩa của Nhân dân văn học xã xuất bản năm 1958. Kỷ niệm 50 năm NXB Phổ thông lần đầu ra mắt bộ Tam quốc diễn nghĩa 13 tập (1959-2009),
Công ty văn hóa Đông A đã chính thức phát hành lại bộ sách quý này dành cho bạn đọc chơi sách và mê truyện Tam Quốc diễn nghĩa.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Nobita Và Vùng Đất Lý Tưởng Trên Bầu Trời',N'Truyện thiếu nhi',N'Fujiko F Fujio',N'Tiếng việt',N'Kim Đồng',2020, '~\img_web\NobitaVaVungDatLyTuongTrenBauTroi.jpg',N'Câu chuyện bắt đầu khi Nobita tìm thấy một hòn đảo hình lưỡi liềm trên trời mây. Ở nơi đó, tất cả đều hoàn hảo… đến mức cậu nhóc mê ngủ ngày như Nobita cũng có thể trở thành một thần đồng toán học,
một siêu sao thể thao! Doraemon và nhóm bạn đã cùng sử dụng một món bảo bối độc đáo chưa từng xuất hiện trước đây để đến với vương quốc tuyệt vời này. Cùng với những người bạn ở đây, đặc biệt là chàng robot mèo Sonya, cả hội đã có chuyến hành trình tới vương quốc trên mây tuyệt vời…
cho đến khi những bí mật đằng sau vùng đất lý tưởng này được hé lộ!', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Con Chim Xanh Biếc Bay Về',N'Tiểu thuyết',N'Nguyễn Nhật Ánh',N'Tiếng việt',N'	NXB Trẻ', 2021, '~\img_web\ConChimXanhBiecBayVe.jpg',N'Con Chim Xanh Biếc Bay Về
Không giống như Những tác phẩm trước đây lấy bối cảnh vùng quê miền Trung đầy ắp những hoài niệm tuổi thơ dung dị, trong trẻo với các nhân vật ở độ tuổi dậy thì, trong quyển sách mới lần này nhà văn Nguyễn Nhật Ánh lấy bối cảnh chính là Sài Gòn -
Thành phố Hồ Chí Minh nơi tác giả sinh sống (như là một sự đền đáp ân tình với mảnh đất miền Nam). Các nhân vật chính trong truyện cũng “lớn” hơn, với những câu chuyện mưu sinh lập nghiệp lắm gian nan thử thách của các sinh viên trẻ đầy hoài bão.
Tất nhiên không thể thiếu những câu chuyện tình cảm động, kịch tính và bất ngờ khiến bạn đọc ngẩn ngơ, cười ra nước mắt. 
Và như trong mọi tác phẩm Nguyễn Nhật Ánh, sự tử tế và tinh thần hướng thượng vẫn là điểm nhấn quan trọng trong quyển sách mới này.
Như một cuốn phim “trinh thám tình yêu”, Con chim xanh biếc bay về dẫn bạn đi hết từ bất ngờ này đến tò mò suy đoán khác, để kết thúc bằng một nỗi hân hoan vô bờ sau bao phen hồi hộp nghi kỵ đến khó thở.
Bạn sẽ theo phe sinh viên-nhân viên với những câu thơ dịu dàng và đáo để, hay phe ông chủ với những kỹ năng kinh doanh khởi nghiệp? 
Và hãy đoán thử, điều gì khiến bạn có thể cảm động đến rưng rưng trong cuộc sống giữa Sài Gòn bộn bề?
Lâu lắm mới có hình ảnh thành phố rộn ràng trong tác phẩm của Nguyễn Nhật Ánh - điều hấp dẫn khác thường của Con chim xanh biếc bay về.
Chính vì thế mà cuốn sách chỉ có một cách đọc thôi: một mạch từ đầu đến cuối!', 0);



INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Cảnh Ngộ',N'Tiểu thuyết',N'Minato Kanae',N'Tiếng việt',N'NXB Hà Nội', 2020,'~\img_web\CanhNgo.jpg',N'"Tác phẩm mới của Minato Kanae (Tác giả cuốn Bestseller Thú Tội)
Cùng chia sẻ cảnh ngộ được trại trẻ mồ côi nhận nuôi từ khi vừa lọt lòng mẹ, Takakura Yoko – phu nhân một chính trị gia, và nữ nhà báo Aida Harumi trở thành đôi bạn thân gắn bó với nhau sâu sắc.
Bước ngoặt xảy ra khi cuốn sách tranh Ruy băng trên trời xanh mà Yoko vẽ tặng con trai Yuta dựa trên câu chuyện đời của Harumi bất ngờ đoạt giải thưởng lớn tầm cỡ và trở thành sách bán chạy. Một thời gian ngắn sau đó, Yuta mất tích.
Lá thư đe dọa gửi về văn phòng Takakura có đoạn: 
“Nếu muốn thằng bé trở về bình an vô sự, hãy công khai sự thật cho mọi người biết”. “Sự thật” đó rốt cuộc là gì? Và ai là hung thủ đã bắt cóc Yuta?',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Thiếu Nữ (Shoujo)',N'Tiểu thuyết',N'Minato Kanae',N'Tiếng việt',N'Hà Nội', 2022,'~\img_web\ThieuNu(Shoujo).jpg',N'Tôi muốn nhìn thấy xác chết... Tôi muốn chứng kiến cái chết... Tôi muốn giác ngộ về cái chết...
Sau khi cùng nghe lời kể có phần hơi khoe khoang về việc đã được chứng kiến cái chết của người bạn thân nhất từ một học sinh chuyển trường, Yuki muốn nhìn thấy khoảnh khắc một người chết đi chứ không phải một cái xác; còn Atsuko từng nghĩ đến việc tự tử, thì cho rằng khi nhìn thấy một xác chết, cô có thể giác ngộ về cái chết và trở nên mạnh mẽ hơn. Thế là hai cô gái lần lượt đi làm tình nguyện viên ở viện dưỡng lão và 
khoa Nhi trong bệnh viện mà không nói cho người kia biết, hòng mong được chứng kiến ​​giây phút lâm chung của một ai đó.
Thiếu nữ mô tả kỳ nghỉ hè chấn động của các nữ sinh lớp 11 muốn khám phá cái chết. 
Câu chuyện thực sự gây lay động bởi những trang viết đầy hồi hộp và xúc động về gia đình, tình bạn, tình yêu.',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Bi Xanh Và Bông Tím',N'Truyện thiếu nhi',N'Tần Văn Quân',N'Tiếng việt',N'NXB Lao Động', 2018, '~\img_web\BiXanhVaBongTim.jpg',N'Câu chuyện về Bi Xanh và Bông Tím được lấy cảm hứng từ chính câu chuyện tuổi thơ của tôi.
Hồi còn bé, tôi rất hay lui tới một một ngôi nhà cũ kỹ trên đường Nanchang, thành phố Thượng Hải. Ngôi nhà cũ tới mức đã trở thành nơi cư ngụ cho cả dơi và mèo hoang. Những con mèo đó đến từ khắp nơi, chúng mang nhiều hình dạng khác nhau, từ màu lông cho đến chiếc đuôi. Có lúc chúng khá hòa thuận và vui vẻ,
nhưng cũng có lúc chúng cãi lộn rồi đánh lẫn nhau. Thường thường, thế nào cũng có một con mèo bị hất ra khỏi cái “hang” đó.
Tôi đã dành rất nhiều thời gian để quan sát lũ mèo phân chia lãnh thổ. Mèo là một loài động vật dễ thương, nhưng chúng cũng có những cảm xúc như giận dữ, sợ hãi, khích động… Tôi rất ghét phải chứng kiến sự ích kỉ và mù quáng của chúng. 
Đôi lúc, tôi tự cho mình là một nữ cảnh sát đến giải cứu và mang những con mèo cô đơn về nhà.
Dù là già hay trẻ, thì ích kỷ vẫn là một thuộc tính cố hữu của con người, họ chỉ yêu bản thân mình. Nếu yêu quý ai hoặc cái gì, họ sẽ coi người đó hoặc thứ ấy như một bông hoa tuyệt đẹp. 
Còn với những người hoặc vật mà họ không thích, họ sẽ coi như một cái gai khó chịu.
Đó chính là ý tưởng gốc rễ của câu chuyện này. Tôi hi vọng, những độc giả nhỏ tuổi của mình cũng sẽ công tâm và tốt bụng. Hãy luôn coi mọi người và mọi vật như những bông hoa đẹp thay vì những cái gai. Nhãn quan tích cực sẽ đem đến cho ta hạnh phúc, 
còn nhãn quan tiêu cực sẽ chỉ khiến ta thành người hẹp hòi, làm cuộc sống hòa thuận êm ấm của ta bị phá vỡ.
Năm tháng qua đi, cô bé thích làm cảnh sát bênh vực cho lũ mèo năm xưa đã trưởng thành, giờ cô là một bà bảo mẫu thích kể chuyện cho các em nhỏ.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Truyện Tranh Việt Nam - Sọ Dừa',N'Truyện thiếu nhi',N'Vũ Thị Hồng',N'Tiếng việt',N'Hà Nội', 2020, '~\img_web\TruyenTranhVN_SoDua.jpg',N'Việt Nam có kho tàng truyện cổ tích phong phú. Từ xưa đến nay, đã có biết bao thế hệ người Việt lớn lên cùng cổ tích. Những câu chuyện diệu kì, 
thấm đẫm tâm hồn dân tộc ấy cứ tự nhiên đi vào tuổi thơ, lớn lên cùng năm tháng và trở thành hành trang trong suốt cuộc đời.
Trong bộ sách Kho Tàng Truyện Cổ Tích Việt Nam bé sẽ được gặp các nhân vật cổ tích như: Thạch Sanh, Sọ Dừa, công chúa....Tất cả sẽ làm giàu thêm trí tưởng tượng vốn rất phong phú của bé và giúp các em thêm yêu, thêm tin vào cổ tích.
Bộ truyện với những hình minh họa sinh động, bố cục sinh động giúp bé dễ dàng tiếp cận với nội dung. 
Các bức vẽ phù hợp với nội dung truyện kể và bản sắc dân tộc của mỗi vùng miền, đem đến sự hấp dẫn lớn nơi người đọc, đặc biệt là trẻ thơ
Bà mẹ nghèo sau khi uống nước ở một cái gáo dừa trở về thì có mang. Đến ngày trở dạ, bà sinh ra một đứa con kì hình dị dạng: tròn lông lốc, có đủ mồm miệng, mắt mũi… nhưng không có cổ và tay chân… Bà mẹ thương tình vẫn gắng nuôi con, 
cho đến một ngày kia, đứa bé lớn lên, nhận việc đi chăn dê cho nhà phú ông… Rồi lại đến một ngày, đứa bé ấy thành quan Trạng…
Những câu chuyện dân gian nuôi dưỡng tâm hồn các em, giúp các em biết học điều hay lẽ phải, yêu cái thiện, ghét cái xấu và trân trọng truyền thống cha ông. Bộ sách Tranh truyện dân gian Việt Nam là món quà ý nghĩa với những câu chuyện được tuyển chọn và biên soạn kĩ lưỡng. 
Phần tranh vẽ minh họa sinh động, gần gũi giúp các em dễ dàng hơn trong việc tiếp cận và ghi nhớ câu chuyện.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Thương',N'Văn học',N'	Nhiều Tác Giả',N'Tiếng việt',N'NXB Phụ Nữ Việt Nam', 2021, '~\img_web\Thuong.jpg',N'"Em nói em từ bỏ
Sao em lại đau lòng?
Em nói em từ bỏ
Sao em còn trông mong?"
Được viết bởi các tác giả đến từ Group Thìa Đầy Thơ - nơi hội tụ thế hệ trẻ yêu thơ và làm thơ, "Thương" là một tập thơ rất tình, ngẫu hứng, và đầy sáng tạo. Ở “Thương ”
không có bóng dáng của một nhân vật nhất định, nhưng mang lại cho người đọc đầy đủ tất cả cảm xúc về tình yêu, tuổi trẻ và cuộc đời.
Thực sự không khó để tìm được sự đồng điệu tâm hồn với những dòng thơ ấy. Sự đồng cảm trong giai điệu mà “Thương” phủ lên đôi môi của độc giả có chút nhẹ nhàng, bâng quơ, gần gũi, dễ đọc, dễ  đánh thức sự lãng mạn cùng những tâm tư chưa từng tỏ bày cùng ai. 
Đó là thứ thơ phóng khoáng, trẻ trung đầy sức sống, đôi khi da diết, đôi khi lửng lơ, phảng phất đủ loại thăng trầm được biểu đạt theo một cách hết sức dễ chịu. Bạn hẳn sẽ thấy chính mình trong đó. 
Đó là những ấm áp len lỏi của tình cảm gia đình, là ngọt ngào hạnh phúc của tình yêu, là những phút giây chậm lại để sống. Là nhớ thương chờ đợi, là giận hờn vu vơ, 
là lo sợ được mất và cả những tổn thương, những lần tự chữa lành.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Chuyện Kể Rằng Có Nàng Và Tôi',N'Văn học',N'Nhiều Tác Giả',N'Tiếng việt',N'	NXB Phụ Nữ Việt Nam', 2022,'~\img_web\ChuyenKeRangCoNangVaToi.jpg',N'Đối với những người trẻ được sống như ý không phải lúc nào cũng dễ dàng, 
đặc biệt với những người đã phải trải qua một quãng thời gian khó khăn rồi mới có thể tìm được con người thật của mình, là chính mình. Những câu chuyện tình của họ có nhiều dang dở vì những mặc cảm, rào cản, khao khát được làm điều mình muốn, gắn bó với người mình yêu thương cả đời là các mong ước nhỏ trong lòng. 
Để rồi khi không thể giãi bày cùng ai, họ mang những điều thầm kín thổi vào những vần thơ nơi chỉ có những “câu chuyện về nàng và tôi”.
“Chuyện kể rằng có nàng và tôi” là cuốn sách nhỏ với những áng thơ nhẹ nhàng, lãng mạn thể hiện mối giao hòa đẹp đẽ trong tâm hồn những người con gái.
Tình yêu của họ vượt trên tất thảy mọi định kiến, chỉ còn lại là những cảm xúc dạt dào, vô tận. 
Trong những câu thơ đôi khi họ là những người lãng du cô đơn bước chân qua đám đông tranh cãi ồn ào và luôn khao khát tìm kiếm hạnh phúc.',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Dear, Darling ',N'Ngôn tình',N'Hiên',N'Tiếng việt',N'Phụ Nữ Việt Nam', 2022, '~\img_web\Dear_Darling.jpg',N'“Dear, darling
Nếu tình yêu có hạn sử dụng,
Em mong là dùng được trăm năm.” 
Quay trở lại sau 2 năm vắng bóng, “Dear darling” của Hiên gửi tặng bạn những lời thủ thỉ rất đỗi dịu dàng, rất đỗi ngọt ngào và cũng rất đỗi thơ qua 200 trang viết.
Cuốn sách kết nối những tâm hồn cùng rung cảm trước cái đẹp và có chung tần số hạnh phúc, yêu thương.
Hiên ký thác tất cả niềm yêu sống, những ngày mạnh mẽ đi qua nỗi buồn và vẹn tròn tình cảm của mình lên mỗi câu chữ.
Nếu bạn không hài lòng về thế giới này, có lẽ những lời nhắn nhủ trong “Dear, darling” sẽ đánh thức hạt mầm yêu thương đang bị phủ màn trong trái tim của bạn:
- “Cuộc đời vô thường, thời gian có hạn. Thêm một lần gặp gỡ, cũng chính là bớt một lần gặp gỡ. Mong chúng ta trân trọng mọi khoảnh khắc cuộc đời mình.”
- “Chúng ta chỉ có một cuộc đời,
Nếu có thể, đừng lãng phí nhau…”
Đời người là một bản nhạc. Bản nhạc dù vui đến mấy cũng sẽ luôn có nốt thăng và nốt trầm. Có lúc cao, có lúc thấp. Có lúc nhanh, có lúc chậm. 
Vậy nên, chỉ cần bạn không từ bỏ giữa chừng, thì rồi ngày vui sẽ lại đến, nắng lại về, nhất định sẽ gặp được bình an.
Khép lại những trang cuối của “Dear, darling”, hy vọng bạn mãi mãi tự do yêu thương, 
trong những đêm đầu hạ thắp lên một chuyện tình dịu dàng đáng nhớ, hát vang khúc tình ca để những tháng năm thanh xuân của mình thêm rực rỡ, căng tràn.
Mong rằng “Dear, darling” sẽ là lời an ủi dịu dàng nhất dành cho bạn.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Tổng Ôn Ngữ Pháp Tiếng anh (Tái Bản 2023)',N'Sách tham khảo',N'Trang Anh',N'Tiếng việt',N'NXB Hồng Đức', 2023, '~\img_web\TongOnNguPhapTiengAnh(TB2023).jpg',N'Tips Học Sách Tiếng Anh Cô Trang Anh: Tổng Ôn Ngữ Pháp Hiệu Quả:
Bước 1: Kích hoạt mã ID phía cuối cuốn sách.
Bước 2: Xem bài giảng lý thuyết trực tuyến đính kèm trên hệ thống qua mã ID.
Bước 3: Xem ví dụ và bài tập minh họa trong bài giảng Tiếng Anh cô Trang Anh.
Bước 4: Thực hành làm bài tập trong sách Tổng ôn ngữ pháp.
Bước 5: Tra cứu đáp án theo chuyên đề ngữ pháp hoặc từng câu hỏi. Để lại bình luận nếu cần hỗ trợ giải đáp.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Dám Nghĩ Lại',N'Kỹ năng sống',N'Adam Grant',N'Tiếng việt',N'Dân Trí',2023, '~\img_web\DamNghiLai.jpg',N'Mười hai trong số mười lăm thành viên đội cứu hỏa đã tử nạn trong đám cháy gần đỉnh Mann Gulch vào năm 1949. 
Hai trong số ba người sống sót là nhờ có thể lực tốt nên kịp chạy thoát khỏi đám cháy; người còn lại, Wagner Dodge, đã thoát khỏi lưỡi hái tử thần bằng tư duy linh hoạt của mình.
Những đồng đội của Wagner Dodge mất mạng vì đã hành động theo những kỹ năng và hiểu biết đã ăn sâu trong tiềm thức của họ. Dodge thì khác, anh không tìm cách dập lửa theo kiến thức tích lũy được, 
mà nhanh chóng nhận định tình hình và tạo ra một lối thoát hiểm bằng cách đốt trụi đám cỏ trước mặt, chặn đứng nguồn bắt lửa của đám cháy phía sau. 
Tưởng rằng đó là hành động điên rồ, nhưng Dodge đã thoát chết nhờ kịp thời tái tư duy.
Táitưduy, theo Adam Grant, là suy nghĩ lại, cân nhắc lại quan điểm, định kiến, thậm chí là kiến thức của bản thân, cũng có thể là suy nghĩ thoát khỏi lối mòn tư duy. 
Cũng theo ông, để chinh phục kỹ năng này, bạn cần quên đi những gì đã học, đồng thời thiết lập và duy trì vònglặptáitưduy.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Truyện Tranh Trạng Quỷnh - Tập 402: Ông Tôm Bà Tép',N'Truyện thiếu nhi',N'Kim Khánh',N'Tiếng việt',N'NXB Kim Đồng',2020, '~\img_web\TrangQuynh_402_OngTomBaTep.jpg',N'Trạng Quỷnh là một bộ truyện tranh thiếu nhi nhiều tập của Việt Nam,
tập truyện đầu tiên mang tên Sao sáng xứ Thanh được Nhà xuất bản Đồng Nai phát hành giữa tháng 6 năm 2003.
Ban đầu tác phẩm được đặt là Trạng Quỳnh (từ tập 1 đến tập 24), còn từ tập 25 trở đi thì đặt tên là Trạng Quỷnh.
Tác phẩm được thực hiện bởi tác giả Kim Khánh. 
Truyện lấy bối cảnh vào thời chúa Nguyễn, dưới thời chúa Nguyễn Phúc Khoát, nhưng những sự kiện xảy ra trong truyện không trùng lặp với những sự kiện xảy ra trên thực tế. Tác phẩm này ban đầu kể lại về cuộc đời của Trạng Quỳnh -
một người có tính cách trào phúng dân gian Việt Nam. Trong truyện này, Trạng Quỳnh vốn thông minh từ trong bụng mẹ.
Trước khi cậu sinh ra, một lần bà mẹ ra ao giặt đồ, bỗng nhìn thấy một chú vịt, bà mẹ liền ngâm câu thơ, và lập tức có tiếng đối đáp lại trong bụng vịt.
Bà cho rằng đó là điềm lạ, nghĩ rằng bà sẽ sinh ra một quý tử, hiểu biết hơn người, sẽ là người có tiếng tăm.
Thời gian trôi qua, bà hạ sinh một bé trai, tư dung thông minh lạ thường, đặt tên là Quỳnh.', 0);
															
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'199 Mấy Hồi Ấy Làm Gì?',N'Truyện thiếu nhi',N'Trang Neko, X Lan',N'Tiếng việt',N'NXB Kim Đồng', 2020, '~\img_web\199MayHoiAyLamGi.jpg',N'Nếu nhắc chuyện “ngày xưa”, chắc hẳn ai cũng có cả tá chuyện để kể, để vui và để nhớ. Đó là lí do cuốn sách “199 mấy Hồi ấy làm gì?” nằm trên tay bạn.
Hi vọng “cỗ máy thời gian” nhỏ bé này sẽ giúp độc giả sống lại một sẽ giúp độc giả sống lại một vài khoảnh khắc đã qua đi trong chốc lát, để nhớ về thời “huy hoàng” của mỗi người lớn đã từng một thời là trẻ con…
Tác giả lời: Trang Neko
Tổng hòa tính cách của cung Ma Kết cứng đầu và nhóm máu A bảo thủ.
Sách đã xuất bản: “Nắng về phía ây”.
Họa sĩ minh họa: X.Lan
Dốt văn nên hay vẽ. Đồng tác giả cuốn “Mẹ Yêu Ai Nhất?”', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Đám Trẻ Ở Đại Dương Đen',N'Truyện ngắn',N'Châu Sa Đáy Mắt',N'Tiếng việt',N'Thế Giới', 2023, '~\img_web\DamTreODaiDuongDen.jpg',N'“nỗi buồn không rõ hình thù
ta cho nó dáng, ta thu vào lòng
ta ôm mà chẳng đề phòng
một ngày nó lớn chất chồng tâm can”
“kẻ sống muốn đời cạn
người chết muốn hồi sinh
trần gian bi hài nhỉ?
ta còn muốn bỏ mình?”
Đám trẻ ở đại dương đen là lời độc thoại và đối thoại của những đứa trẻ ở đại dương đen, nơi từng lớp sóng của nỗi buồn và tuyệt vọng không ngừng cuộn trào, lúc âm ỉ, khi dữ dội. Những đứa trẻ ấy phải vật lộn trong những góc tối tâm lý, 
với sự u uất đè nén từ tổn thương khi không được sinh ra trong một gia đình toàn vẹn, ấm êm, khi phải mang trên đôi vai non dại những gánh nặng không tưởng.
Song song đó cũng là quá trình tự chữa lành vô cùng khó khăn của đám trẻ, cố gắng vươn mình ra khỏi đại dương đen, tìm cho mình một ánh sáng. 
Và chính những sự nỗ lực xoa dịu chính mình đó đã hóa thành những câu từ trong cuốn sách này, bất kể đau đớn thế nào.', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'90% Trẻ Thông Minh Nhờ Cách Trò Chuyện Đúng Đắn Của Cha Mẹ (Tái Bản 2019)',N'Kỹ năng sống',N'Urako Kanamori',N'Tiếng việt',N'	NXB Kim Đồng', 2019,'~\img_web\90%TreThongMinh.jpg',N'Về bản chất, mỗi đứa trẻ đều mang trong mình một “sức mạnh” tuyệt vời. Nhưng trước hết, chúng ta phải tin tưởng vào sức mạnh ấy đã! 
Khi được tin cậy, “sức mạnh” bên trong trẻ sẽ được nuôi dưỡng một cách tự nhiên.
Cuốn sách này sẽ giới thiệu cách trò chuyện giúp khai phá sức mạnh ấy từ nhiều góc độ.
Chắc chắn không chỉ các con mà ngay cả chính các bậc phụ huynh cũng sẽ thay đổi. Cuộc sống sẽ lại một lần nữa trở nên thật tuyệt vời.
Cuốn sách này sẽ giúp mở rộng tiềm năng của trẻ tới vô hạn!',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Hài Hước Một Chút Thế Giới Sẽ Khác Đi (Tái Bản 2021)',N'Kỹ năng sống',N'Lưu Chấn Hồng',N'Tiếng việt',N'NXB Thanh Niên', 2021,  '~\img_web\HaiHuocMotChutTheGioiSeKhacDi.jpg',N'Trong xã hội hiện đại, 
giao tiếp là chìa khóa giải quyết mọi vấn đề. Vậy làm thế nào để giao tiếp hiệu quả? Đó là biết vận dụng một cách tinh tế sự hài hước vào lời nói và tư duy, 
hài hước có thể giúp giúp chúng ta thiết lập được mạng lưới quan hệ rộng rãi. Tuy nhiên, hài hước không phải là một năng lực bẩm sinh, muốn có được “nghệ thuật giúp bạn thành công” này, 
bạn phải trải qua quá tình bồi dưỡng và rèn luyện bản thân.
Hài hước một chút, thế giới sẽ khác đi - cuốn sách với nội dung phong phú mà sâu sắc này sẽ giúp các bạn có được cái nhìn rõ nét hơn về tính hài hước dưới các góc độ,
phương diện đánh giá khác nhau, cũng như có thêm kĩ năng vận dụng sự hài hước vào cuộc sống hàng ngày.',0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Mỗi Lần Vấp Ngã Là Một Lần Trưởng Thành',N'Kỹ năng sống',N'Liêu Trí Phong',N'Tiếng việt',N'NXB Thanh Niên', 2019,'~\img_web\MoiLanVapNgaLaMotLanTruongThanh.jpg',N'Trong quá trình này, bạn sẽ khó tránh khỏi vấp ngã, sẽ trải qua nhiều lần đau thương, 
và sẽ có những lúc bạn cảm thấy vô cùng mỏi mệt, nhưng hãy tin rằng, chỉ có những người đã từng đau thương thì mới trở nên vững vàng hơn.
Mỗi chúng ta từ nhỏ đến lớn, dù ít dù nhiều đều đã từng trải qua những lúc cảm thấy đau khổ, đây chính là trở ngại mà chúng ta thường nói tới. 
Tôi tin rằng sẽ không một người nào dám khẳng định cuộc đời của họ chẳng bao giờ gặp trở ngại. Đó chính là cuộc sống.
Nhưng mong bạn suy nghĩ kĩ, mỗi khi bạn gặp phải khó khăn hoặc vấp ngã, ngoài việc cảm thấy đau khổ về mặt tinh thần, bạn còn học được điều gì? Tôi dám khẳng định rằng bạn đã có được kinh nghiệm, 
và khi đã lĩnh hội, bạn sẽ không vấp ngã lại nơi bạn đã từng ngã. Đây chính là sự trưởng thành.
Đặc biệt, khi bạn rời xa vòng tay che chở, bao bọc của cha mẹ và nhà trường, bước chân vào xã hội, 
bạn sẽ gặp phải rất nhiều trở ngại và nhận ra xã hội này vốn không hề đơn giản như bạn tưởng tượng! Bạn cũng sẽ nhận ra khi bạn đau buồn hay gặp phải khó khăn, sẽ không có ai để ý đến sự tủi thân của bạn và cũng chẳng quan tâm đến sự bất lực của bạn.
Thậm chí bạn cũng không muốn kể cho cha mẹ nghe vì sợ họ phải lo lắng cho mình, bạn chỉ có thể tự mình giải quyết, tự mình gánh chịu.',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Tiếng anh 1 - Global Success - Sách Bài Tập (2023)',N'Sách giáo khoa',N'Nhiều Tác Giả',N'Tiếng việt',N'NXB Giáo Dục Việt Nam', 2022,  '~\img_web\TA1_GlobalSuccess_SBT.jpg',N'Bộ sách Tiếng Anh do Nhà xuất bản Giáo dục Việt Nam biên soạn 
theo chương trình thí điểm Tiếng Anh Tiểu học do Bộ GD ban hành, 
với sự hợp tác chặt chẽ về chuyên môn và kĩ thuật của Nhà xuất bản Macmillan(MPC). Sách được biên soạn theo đường hướng giao tiếp, giúp học sinh bước đầu hình thành và phát triển năng lực giao tiếp bằng tiếng anh, 
thông qua bốn kĩ năng nghe, nói, đọc, viết, trong đó ưu tiên hai kĩ năng nghe và nói.',0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Lịch Sử Và Địa Lí 6 (Kết Nối Tri Thức) (2023)',N'Sách giáo khoa',N'Nhiều Tác Giả',N'Tiếng việt',N'Giáo Dục Việt Nam', 2023, '~\img_web\LichSuVaDiaLi6.jpg',N'Lịch Sử Và Địa Lí 6 (Kết Nối Tri Thức) (2023)', 0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Chưa Kịp Lớn Đã Trưởng Thành (Tái Bản 2023)',N'Tiểu thuyết',N'Tớ Là Mây',N'Tiếng việt',N'Dân Trí', 2023,  '~\img_web\ChuaKipLonDaTruongThanh.jpg',N'Chúng ta của hiện tại, đều chưa kịp lớn đã phải trưởng thành.
Lúc còn nhỏ có thể khóc cười tuỳ ý. Trưởng thành rồi mới biết hành động cũng cần nhìn sắc mặt người khác.
Lúc còn nhỏ có thể sống vô tư, vô lo. Trưởng thành rồi mới biết nếu chậm chân hơn người khác, chắc chắn sẽ bị đào thải bất cứ lúc nào.
Lúc còn nhỏ có thể khao khát, mơ mộng. Trưởng thành rồi mới biết thế giới ngoài kia thực sự rất tàn khốc.
Chúng ta đều giống nhau, lúc còn nhỏ đều khao khát trưởng thành. Trưởng thành rồi lại loay hoay học cách trở thành người lớn.
Nếu bạn đang trải qua giai đoạn lạc lõng và cô đơn như vậy, hãy để “Chưa kịp lớn đã phải trưởng thành” làm một người bạn ở bên, 
xoa dịu tổn thương và gửi tặng bạn đôi điều khích lệ. Mỗi trang sách đều là một lá thư nhắn nhủ bạn về sự đặc biệt của bản thân, 
về những nỗ lực hoàn thiện không ngừng nghỉ trong thế giới của người trưởng thành.',0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'999 Lá Thư Gửi Cho Chính Mình - Những Lá Thư Ấn Tượng Nhất',N'Truyện ngắn',N'Miêu Công Tử',N'Tiếng việt',N'Thanh Niên',2023,'~\img_web\999LaThuGuiChoChinhMinh.jpg',N'Bạn có đang cảm thấy bị “quá tải” với cuộc sống hiện tại không?
Hay là đang loay hoay chữa lành giữa những bộn bề không thể gác lại, chỉ trực chờ để cuốn mình đi?
Đã bao lâu rồi bạn chưa ngồi xuống viết những dòng nhật ký, đã bao lâu rồi bạn chưa tự gửi cho mình một lá thư viết ra những điều bạn mong muốn, viết ra những ước mơ còn dang dở, 
viết ra những khó khăn bạn đã hoặc đang phải trải qua và cũng không quên động viên, cổ vũ chính mình của hiện tại,
nhắn nhủ đến chính mình của tương lai…',  0);
INSERT INTO SACH ( TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONGHIENTAI) 
VALUES (N'Cùng Bạn Trưởng Thành',N'Truyện ngắn',N'Ying Shu',N'Tiếng trung',N'Dân Trí',2022, '~\img_web\CungBanTruongThanh.jpg',N'Nếu bạn đang tìm kiếm một người bạn đồng hành trong việc học tập ngoại ngữ và phát triển bản thân thì cuốn sách 
“Cùng bạn trưởng thành” chắc chắn là cuốn sách dành cho bạn. Đúng như tên gọi của nó, cuốn sách sẽ là người bạn sát cánh bên bạn mỗi ngày, ngoài ra còn truyền tải cảm hứng và thông điệp sống tích cực thông qua những trích dẫn ngắn song ngữ Trung - Việt, 
qua đó bạn có thể vừa trau dồi thêm kiến thức mới, vừa làm mới thế giới nội tâm của bản thân.
Với ngoại hình nhỏ gọn và vô cùng xinh xắn, bạn cũng có thể dễ dàng sách mang theo bên mình để cuốn sách trở thành bạn đồng hành không thể thiếu trong cuộc sống và có thể thưởng thức bất cứ lúc nào bạn rảnh rỗi.
“Mỗi một việc mà bạn đang cố gắng làm, chắc chắn sẽ đơm hoa kết trái vào những ngày tháng sau này” - Hi vọng đây sẽ là cuốn sách “kim chỉ nam” giúp bạn ngày một hoàn thiện bản thân,
mạnh mẽ trưởng thành, làm một phiên bản chính mình hoàn hảo nhất.', 0);


--Them Phieu nhap sach 
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-01-15', 3, 1);  --1
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-02-15', 3, 2);  --2
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-03-15', 3, 3);  --3
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-04-15', 3, 4);  --4
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-05-20', 3, 2);  --5
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-05-22', 3, 2);  --6
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-05-30', 3, 2);  --7
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-06-03', 3, 1);  --8
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-06-06', 3, 2);  --9
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-06-14', 3, 4);  --10

INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-07-02', 3, 2);  --11
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-07-25', 3, 2);  --12
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-07-30', 3, 1);  --13
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-08-06', 3, 2);  --14
INSERT INTO PHIEUNHAPSACH ( NGAYNHAP,  MANV, MANCC) VALUES ( '2024-08-22', 3, 4);  --15
																					 

--THEM CHITIET PN
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (1, 1, 100000, 20)     
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (1, 2, 70000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (1, 3, 50000,  15)
INSERT INTO CHITIETPN (MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (1, 4, 20000,  35)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (1, 5, 56000,  10)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (1, 6, 96000,  20)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 7, 70000,   15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 8, 96000,   5)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 9, 53000,   25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 10, 60000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 11, 55000,  10)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 12, 120000, 25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (2, 13, 86000,  18)
																  				  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 14, 45000,  12)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 15, 67000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 16, 51000,  30)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 17, 135000, 23)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 18, 36000,  25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 19, 99000,  15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (3, 20, 23000,  20)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 21, 120000, 12)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 22, 66000,  15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 23, 45000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 24, 32000,  23)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 25, 48000,  25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 26, 32000,  35)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (4, 27, 55000,  10)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (5, 28, 90000,  25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (5, 29, 35000,  15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (5, 30, 45000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (5, 31, 110000, 20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (5, 32, 96000,  35)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (5, 33, 47000,  25)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 34, 63000,  16)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 35, 85000,  30)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 36, 123000, 15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 37, 99000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 38, 39000,  25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 39, 55000,  15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (6, 40, 20000,  20)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (7, 1, 100000,  10)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (7, 2, 70000,   20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (7, 7, 70000,   17)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (8, 15, 67000,  25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (8, 20, 23000,  15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (8, 02, 70000,  20)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (9, 21, 120000, 20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (9, 33, 47000,  20)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (10, 9, 53000,  16)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (10, 40, 20000, 30)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (10, 3, 50000,  15)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (11, 2, 70000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (11, 13, 86000, 15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (11, 25, 48000, 10)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (12, 20, 23000, 10)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (12, 30, 45000, 25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (12, 35, 85000, 18)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (13, 14, 45000,  30)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (13, 21, 120000, 15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (13, 36, 123000, 23)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (13, 38, 39000,  25)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (14, 17, 135000, 20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (14, 34, 63000,  15)
																  
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (15, 1, 100000,  25)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (15, 27, 55000,  15)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (15, 38, 39000,  20)
INSERT INTO CHITIETPN( MAPN, MASACH, GIASACH, SOLUONGNHAP) VALUES (15, 39, 55000,  25)
																  								

-- Them don vi thanh ly 
INSERT INTO DONVITL ( TENDV, DIACHIDV, SDTDV) VALUES (N'Sách Xưa',   N'Quận 10, Thành phố Hồ Chí Minh', '0903663733'); --1
INSERT INTO DONVITL ( TENDV, DIACHIDV, SDTDV) VALUES (N'Bá Tân Sách',N'Quận 3, Thành phố Hồ Chí Minh',  '0962936310'); --2


-- Them Phieu muon
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 1, '2024-03-25', '2024-04-24', 2, 0, 0);   --1
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 2, '2024-04-08', '2024-05-08', 2, 0, 0);   --2
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 6, '2024-03-07', '2024-04-06', 2, 0, 0);   --3
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 4, '2024-03-30', '2024-04-29', 2, 0, 0);   --4
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 3, '2024-10-12', '2024-11-11', 2, 0, 0);   --5
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 5, '2024-05-15', '2024-06-14', 2, 0, 0);   --6
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 10,'2024-05-20', '2024-06-19', 2, 0, 0);   --7
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 2, '2024-05-21', '2024-06-20', 2, 0, 0);   --8
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 9, '2024-05-21', '2024-06-20', 2, 0, 0);   --9
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 11,'2024-05-25', '2024-06-24', 2, 0, 0);   --10 

INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 8, '2024-09-02', '2024-10-02', 2, 0, 0);   --11
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 7, '2024-09-02', '2024-10-02', 2, 0, 0);   --12
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 12, '2024-09-15', '2024-10-15', 2, 0, 0);   --13
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 7, '2024-09-15', '2024-10-15', 2, 0, 0);   --14
INSERT INTO PHIEUMUON ( MATHE, NGAYMUON, HANTRA, MANV, TINHTRANG, MADK) VALUES ( 8, '2024-09-20', '2024-10-20', 2, 0, 0);   --15


-- Them chi tiet phieu muon
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 1, 02, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 1, 20, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 1, 06, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 1, 17, 1);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 2, 17, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 2, 10, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 3, 10, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 3, 11, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 3, 12, 1);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 4, 15, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 4, 11, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 4, 19, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 5, 13, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 5, 22, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 5, 01, 1);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 6, 16, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 6, 18, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 7, 21, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 7, 03, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 8, 02, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 8, 15, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 8, 30, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 9, 24, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 9, 08, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 10, 02, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 10, 04, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 10, 32, 1);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 11, 01, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 11, 04, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 12, 02, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 12, 04, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 13, 15, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 13, 21, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 13, 23, 1);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 14, 18, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 14, 19, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 14, 20, 2);

INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 15, 31, 2);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 15, 22, 1);
INSERT INTO CHITIETPM ( MAPM, MASACH, SOLUONGMUON) VALUES ( 15, 28, 1);


-- Them Phieu tra 
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (1, 1,  '2024-05-04', 2);--trễ 10 ngày	   --1
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (2, 2,  '2024-04-15', 2);--trễ 0 ngày	   --2
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (3, 6,  '2024-04-18', 2);--trễ 12 ngày    --3
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (7, 10, '2024-06-18', 2);--trễ 0 ngày	   --4
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (5, 3,  '2024-10-30', 2);--trễ 0 ngày	   --5
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (4, 4,  '2024-04-30', 2);--trễ 1 ngày	   --6
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (6, 5,  '2024-06-20', 2);--trễ 6 ngày	   --7
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (8, 2,  '2024-06-27', 2);--trễ 7 ngày	   --8																				  
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (9, 9,  '2024-06-05', 2);--trễ 5 ngày	   --9
INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (10, 11,'2024-06-20', 2);--trễ 0 ngày	   --10

INSERT INTO PHIEUTRA (MAPM, MATHE, NGAYTRA, MANV) VALUES (8, 2, '2024-07-20', 2); --trễ 30 ngày	   --11


-- Them Chi tiet phieu tra
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 1, 2, 1, 1, 0, 90000);
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 1, 20, 0, 0, 1, 140000);
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 1, 6, 1, 0, 0, 30000);
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 1, 17, 0, 1, 0, 70500);
 
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 2, 17, 0, 1, 0, 40500);
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 2, 10, 1, 0, 1, 120000);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 3, 10, 0, 1, 0, 66000);	
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 3, 11, 0, 1, 1, 173500);	
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 3, 12, 0, 1, 0, 96000);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 4, 3, 0, 1, 0, 61500);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 5, 22, 1, 0, 0, 0);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 6, 15, 1, 0, 0, 3000);	

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 7, 18, 1, 0, 0, 18000);
INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 7, 16, 0, 1, 0, 43500);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 8, 15, 1, 1, 0, 54750);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 9, 8, 0, 1, 0, 63000);

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 10, 4, 1, 0, 0, 0);	

INSERT INTO CHITIETPT (MAPT, MASACH, SOLUONGTRA,SOLUONGLOI, SOLUONGMAT, PHUTHU) VALUES ( 11, 30, 1, 0, 0, 90000);


-- Them Phieu thanh ly
INSERT INTO PHIEUTHANHLY (MADV, NGAYTL,  MANV) VALUES (1, '2024-08-25',  3);


-- Them Chi tiet phieu thanh ly 
INSERT INTO CHITIETPTL (MAPTL, MASACHKHO, SOLUONGTL, GIATL) VALUES (1, 17, 2, 67500);
INSERT INTO CHITIETPTL (MAPTL, MASACHKHO, SOLUONGTL, GIATL) VALUES (1 ,15, 1,33500);



--THEM PHIEU DKI MUON SACH
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0981724637', '2024-04-25', '2024-05-02');    --1
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0466155193', '2024-04-20', '2024-04-27');	--2
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0964965332', '2024-04-27', '2024-05-04');	--3
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0981724637', '2024-05-02', '2024-05-09');	--4
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0471184811', '2024-05-07', '2024-05-14');	--5
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0969728877', '2024-05-07', '2024-05-14');	--6
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0471184811', '2024-05-27', '2024-06-04');	--7
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0727180418', '2024-06-01', '2024-06-08');	--8
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0638103599', '2024-06-02', '2024-06-09');	--9
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0471184852', '2024-07-09', '2024-07-16');	--10

INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0727180418', '2024-09-26', '2024-10-03');	--11
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0638103599', '2024-09-26', '2024-10-03');	--12
INSERT INTO DKIMUONSACH (SDT, NGAYDKMUON, NGAYHEN) VALUES('0471184852', '2024-09-25', '2024-10-02');	--13

-- THEM CHI TIET DKI
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 1, 2, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 1, 17, 1);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 1, 6, 1);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 2, 15, 1);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 2, 10, 1);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 3, 10, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 3, 11, 1);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 4, 15, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 5, 15, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 5, 38, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 6, 10, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 6, 19, 1);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 7, 15, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 8, 2, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 9, 26, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 9, 27, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 10, 31, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 10, 33, 2);


INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 11, 3, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 12, 29, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 12, 23, 2);

INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 13, 31, 2);
INSERT INTO CHITIETDK ( MADK, MASACH, SOLUONGMUON) VALUES ( 13, 39, 2);


update DkiMuonSach set tinhtrang = 3 where MaDK in(1,2,3,4,5,6,7,8,9,10)

--update DkiMuonSach set tinhtrang = 3 where MaDK in(12)


--insert into PhieuMuon ( MaThe, NgayMuon, HanTra, MaNV, Tinhtrang, MaDK) values ( 9,'2024-08-17', '2024-09-16', 2, 0, 12);   --16

---- Them chi tiet phieu muon
--insert into ChiTietPM ( MaPM, MaSach, Soluongmuon) values ( 16, 2, 2);
--insert into ChiTietPM ( MaPM, MaSach, Soluongmuon) values ( 16, 27, 2);

--UPDATE SACH SET SOLUONGHIENTAI =72 WHERE MASACH = 2
--UPDATE SACH SET SOLUONGHIENTAI =23 WHERE MASACH = 27

--Them CuonSach
--CÓ TRIGGER

--THÊM BẢNG CHI TIẾT SÁCH MƯỢN
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 1, '2A1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 1, '2A2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 1, '20C1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 1, '6A1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 1, '17C1');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 2, '17C2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 2, '10B1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 2, '10B2');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 3, '10B3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 3, '11B1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 3, '11B2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 3, '12B1');
														  
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 4, '15C1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 4, '11B3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 4, '19C1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 4, '19C2');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 5, '13B1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 5, '13B2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 5, '22D1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 5, '1A1');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 6, '16C1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 6, '16C2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 6, '18C1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 6, '18C2');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 7, '21D1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 7, '3A1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 7, '3A2');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 8, '2A3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 8, '15C2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 8, '15C3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 8, '30E1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 8, '30E2');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 9, '24D1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 9, '8B1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 9, '8B2');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 10, '2A4');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 10, '2A5');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 10, '4A1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 10, '4A2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 10, '32E1');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 11, '1A2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 11, '4A3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 11, '4A4');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 12, '2A6');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 12, '2A7');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 12, '4A5');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 12, '4A6');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 13, '15C4');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 13, '21D2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 13, '21D3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 13, '23D1');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 14, '18C3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 14, '19C3');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 14, '20C2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 14, '20C3');

INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 15, '31E1');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 15, '31E2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 15, '22D2');
INSERT INTO CHITIETSACHMUON ( MAPM, MACUONSACH ) VALUES ( 15, '28E1');


--Thêm bảng chi tiết sách trả
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (1, '2A1', 1);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (1, '2A2', 2);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (1, '20C1', 3);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (1, '6A1', 1);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (1, '17C1', 2);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (2, '17C2', 2);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (2, '10B1', 1);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (2, '10B2', 3);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (3, '10B3', 2);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (3, '11B1', 2);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (3, '11B2', 3);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (3, '12B1', 2);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (4, '3A1', 2);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (5, '22D1', 1);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (6, '15C1', 1);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (7, '16C1', 2);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (7, '18C1', 1);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (8, '15C2', 1);
INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (8, '15C3', 2);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (9, '8B1', 2);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (10, '4A1', 1);

INSERT INTO CHITIETSACHTRA (MAPT, MACUONSACH, TINHTRANG) VALUES (11, '30E1', 1);


--THÊM BẢNG CHI TIẾT SÁCH THANH LÍ
INSERT INTO CHITIETSACHTHANHLY (MAPTL, MACUONSACH) VALUES (1, '17C1');
INSERT INTO CHITIETSACHTHANHLY (MAPTL, MACUONSACH) VALUES (1, '17C2');
INSERT INTO CHITIETSACHTHANHLY (MAPTL, MACUONSACH) VALUES (1, '15C3');




--insert into PhieuMuon ( MaThe, NgayMuon, HanTra, MaNV, Tinhtrang, MaDK) values ( 9,'2024-06-09', '2024-07-09', 2, 0, 9);   --13

---- Them chi tiet phieu muon
--insert into ChiTietPM ( MaPM, MaSach, Soluongmuon) values ( 13, 1, 2);
--insert into ChiTietPM ( MaPM, MaSach, Soluongmuon) values ( 13, 27, 2);

--UPDATE SACH SET SOLUONGHIENTAI =8 WHERE MASACH = 27
--UPDATE SACH SET SOLUONGHIENTAI =33 WHERE MASACH = 26

--insert into DkiMuonSach (SDT, NgayDKMuon, NgayHen) values('0727180418', '2024-06-09', '2024-06-16');	--14
--insert into DkiMuonSach (SDT, NgayDKMuon, NgayHen) values('0727180418', '2024-06-09', '2024-06-16');	--15
--insert into DkiMuonSach (SDT, NgayDKMuon, NgayHen) values('0727180418', '2024-06-09', '2024-06-16');	--16
--insert into DkiMuonSach (SDT, NgayDKMuon, NgayHen) values('0727180418', '2024-06-09', '2024-06-16');	--17


--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 14, 3, 2);

--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 15, 29, 2);
--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 15, 23, 2);

--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 16, 31, 2);
--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 16, 39, 2)

--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 17, 1, 2);
--insert into ChiTietDK ( MaDK, MaSach, Soluongmuon) values ( 17, 2, 2);
