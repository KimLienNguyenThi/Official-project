using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class QuanLyThuVienContext : DbContext
{
    public QuanLyThuVienContext()
    {
    }

    public QuanLyThuVienContext(DbContextOptions<QuanLyThuVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDk> ChiTietDks { get; set; }

    public virtual DbSet<ChiTietPm> ChiTietPms { get; set; }

    public virtual DbSet<ChiTietPt> ChiTietPts { get; set; }

    public virtual DbSet<ChiTietPtl> ChiTietPtls { get; set; }

    public virtual DbSet<ChiTietSachMuon> ChiTietSachMuons { get; set; }

    public virtual DbSet<ChiTietSachThanhLy> ChiTietSachThanhLies { get; set; }

    public virtual DbSet<ChiTietSachTra> ChiTietSachTras { get; set; }

    public virtual DbSet<ChitietKhoThanhLy> ChitietKhoThanhLies { get; set; }

    public virtual DbSet<Chitietpn> Chitietpns { get; set; }

    public virtual DbSet<CuonSach> CuonSaches { get; set; }

    public virtual DbSet<DkiMuonSach> DkiMuonSaches { get; set; }

    public virtual DbSet<DocGium> DocGia { get; set; }

    public virtual DbSet<DonViTl> DonViTls { get; set; }

    public virtual DbSet<KhoSachThanhLy> KhoSachThanhLies { get; set; }

    public virtual DbSet<LoginDg> LoginDgs { get; set; }

    public virtual DbSet<LoginNv> LoginNvs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<PhieuNhapSach> PhieuNhapSaches { get; set; }

    public virtual DbSet<PhieuThanhLy> PhieuThanhLies { get; set; }

    public virtual DbSet<PhieuTra> PhieuTras { get; set; }

    public virtual DbSet<QuyDinh> QuyDinhs { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TheDocGium> TheDocGia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KT;Initial Catalog=QuanLyThuVien;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDk>(entity =>
        {
            entity.HasKey(e => new { e.Madk, e.Masach }).HasName("CHITIETDK_MADK_MASACH");

            entity.ToTable("ChiTietDk");

            entity.Property(e => e.Madk).HasColumnName("MADK");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Soluongmuon).HasColumnName("SOLUONGMUON");

            entity.HasOne(d => d.MadkNavigation).WithMany(p => p.ChiTietDks)
                .HasForeignKey(d => d.Madk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDk__MADK__01142BA1");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.ChiTietDks)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDk__MASAC__02084FDA");
        });

        modelBuilder.Entity<ChiTietPm>(entity =>
        {
            entity.HasKey(e => new { e.Mapm, e.Masach }).HasName("CHITIETPM_MAPM_MASACH");

            entity.ToTable("ChiTietPM", tb => tb.HasTrigger("TRG_SACHMUON"));

            entity.Property(e => e.Mapm).HasColumnName("MAPM");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Soluongmuon).HasColumnName("SOLUONGMUON");

            entity.HasOne(d => d.MapmNavigation).WithMany(p => p.ChiTietPms)
                .HasForeignKey(d => d.Mapm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPM__MAPM__5812160E");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.ChiTietPms)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPM__MASAC__59063A47");
        });

        modelBuilder.Entity<ChiTietPt>(entity =>
        {
            entity.HasKey(e => new { e.Mapt, e.Masach }).HasName("CHITIETPT_MAPT_MASACH");

            entity.ToTable("ChiTietPT", tb =>
                {
                    tb.HasTrigger("TRG_SACHTRAvaTL");
                    tb.HasTrigger("UpdateTinhTrangPhieuMuon");
                });

            entity.Property(e => e.Mapt).HasColumnName("MAPT");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Phuthu)
                .HasColumnType("money")
                .HasColumnName("PHUTHU");
            entity.Property(e => e.Soluongloi).HasColumnName("SOLUONGLOI");
            entity.Property(e => e.Soluongmat).HasColumnName("SOLUONGMAT");
            entity.Property(e => e.Soluongtra).HasColumnName("SOLUONGTRA");

            entity.HasOne(d => d.MaptNavigation).WithMany(p => p.ChiTietPts)
                .HasForeignKey(d => d.Mapt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPT__MAPT__6477ECF3");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.ChiTietPts)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPT__MASAC__656C112C");
        });

        modelBuilder.Entity<ChiTietPtl>(entity =>
        {
            entity.HasKey(e => new { e.Maptl, e.Masachkho }).HasName("CHITIETPTL_MAPTL_MASACH");

            entity.ToTable("ChiTietPTL", tb => tb.HasTrigger("TRG_SACHTL"));

            entity.Property(e => e.Maptl).HasColumnName("MAPTL");
            entity.Property(e => e.Masachkho).HasColumnName("MASACHKHO");
            entity.Property(e => e.Giatl)
                .HasColumnType("money")
                .HasColumnName("GIATL");
            entity.Property(e => e.Soluongtl).HasColumnName("SOLUONGTL");

            entity.HasOne(d => d.MaptlNavigation).WithMany(p => p.ChiTietPtls)
                .HasForeignKey(d => d.Maptl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPT__MAPTL__76969D2E");

            entity.HasOne(d => d.MasachkhoNavigation).WithMany(p => p.ChiTietPtls)
                .HasForeignKey(d => d.Masachkho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPT__MASAC__778AC167");
        });

        modelBuilder.Entity<ChiTietSachMuon>(entity =>
        {
            entity.HasKey(e => new { e.Mapm, e.Macuonsach }).HasName("PK__ChiTietS__8C4ADDE64E97DA59");

            entity.ToTable("ChiTietSachMuon", tb => tb.HasTrigger("TRG_TT_SACHMUONTHANHCONG"));

            entity.Property(e => e.Mapm).HasColumnName("MAPM");
            entity.Property(e => e.Macuonsach)
                .HasMaxLength(10)
                .HasColumnName("MACUONSACH");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

            entity.HasOne(d => d.MacuonsachNavigation).WithMany(p => p.ChiTietSachMuons)
                .HasForeignKey(d => d.Macuonsach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSa__MACUO__5CD6CB2B");

            entity.HasOne(d => d.MapmNavigation).WithMany(p => p.ChiTietSachMuons)
                .HasForeignKey(d => d.Mapm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSac__MAPM__5BE2A6F2");
        });

        modelBuilder.Entity<ChiTietSachThanhLy>(entity =>
        {
            entity.HasKey(e => new { e.Maptl, e.Macuonsach }).HasName("PK__ChiTietS__9740669ECEF6E6D5");

            entity.ToTable("ChiTietSachThanhLy", tb =>
                {
                    tb.HasTrigger("SetTinhTrangchitietsachthanhly");
                    tb.HasTrigger("TinhtrangCTKHOTHANHLI");
                });

            entity.Property(e => e.Maptl).HasColumnName("MAPTL");
            entity.Property(e => e.Macuonsach)
                .HasMaxLength(10)
                .HasColumnName("MACUONSACH");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

            entity.HasOne(d => d.MacuonsachNavigation).WithMany(p => p.ChiTietSachThanhLies)
                .HasPrincipalKey(p => p.Macuonsach)
                .HasForeignKey(d => d.Macuonsach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSa__MACUO__7B5B524B");

            entity.HasOne(d => d.MaptlNavigation).WithMany(p => p.ChiTietSachThanhLies)
                .HasForeignKey(d => d.Maptl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSa__MAPTL__7A672E12");
        });

        modelBuilder.Entity<ChiTietSachTra>(entity =>
        {
            entity.HasKey(e => new { e.Mapt, e.Macuonsach }).HasName("PK__ChiTietS__8C4ADDFFD46A6549");

            entity.ToTable("ChiTietSachTra", tb =>
                {
                    tb.HasTrigger("TRG_InsertChiTietKhoThanhLy");
                    tb.HasTrigger("TRG_TT_CUONSACHTRA");
                });

            entity.Property(e => e.Mapt).HasColumnName("MAPT");
            entity.Property(e => e.Macuonsach)
                .HasMaxLength(10)
                .HasColumnName("MACUONSACH");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

            entity.HasOne(d => d.MacuonsachNavigation).WithMany(p => p.ChiTietSachTras)
                .HasForeignKey(d => d.Macuonsach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSa__MACUO__693CA210");

            entity.HasOne(d => d.MaptNavigation).WithMany(p => p.ChiTietSachTras)
                .HasForeignKey(d => d.Mapt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietSac__MAPT__68487DD7");
        });

        modelBuilder.Entity<ChitietKhoThanhLy>(entity =>
        {
            entity.HasKey(e => new { e.Masachkho, e.Macuonsach }).HasName("PK__ChitietK__1C993EAD06D4EE41");

            entity.ToTable("ChitietKhoThanhLy");

            entity.HasIndex(e => e.Macuonsach, "UQ_ChitietKhoThanhLy_MaCuonSach").IsUnique();

            entity.Property(e => e.Masachkho).HasColumnName("MASACHKHO");
            entity.Property(e => e.Macuonsach)
                .HasMaxLength(10)
                .HasColumnName("MACUONSACH");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");
            entity.Property(e => e.Vande).HasColumnName("VANDE");

            entity.HasOne(d => d.MacuonsachNavigation).WithOne(p => p.ChitietKhoThanhLy)
                .HasForeignKey<ChitietKhoThanhLy>(d => d.Macuonsach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietKh__MACUO__6EF57B66");

            entity.HasOne(d => d.MasachkhoNavigation).WithMany(p => p.ChitietKhoThanhLies)
                .HasForeignKey(d => d.Masachkho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietKh__MASAC__6FE99F9F");
        });

        modelBuilder.Entity<Chitietpn>(entity =>
        {
            entity.HasKey(e => new { e.Mapn, e.Masach }).HasName("CHITIETPN_MAPN_MASACH");

            entity.ToTable("CHITIETPN", tb =>
                {
                    tb.HasTrigger("TRG_SACHNHAP");
                    tb.HasTrigger("TRG_TAO_MA_CUON_SACH");
                });

            entity.Property(e => e.Mapn).HasColumnName("MAPN");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Giasach)
                .HasColumnType("money")
                .HasColumnName("GIASACH");
            entity.Property(e => e.Soluongnhap).HasColumnName("SOLUONGNHAP");

            entity.HasOne(d => d.MapnNavigation).WithMany(p => p.Chitietpns)
                .HasForeignKey(d => d.Mapn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPN__MAPN__4E88ABD4");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.Chitietpns)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPN__MASAC__4F7CD00D");
        });

        modelBuilder.Entity<CuonSach>(entity =>
        {
            entity.HasKey(e => e.Macuonsach).HasName("PK__CuonSach__C75BC2BF5A2F72D9");

            entity.ToTable("CuonSach");

            entity.Property(e => e.Macuonsach)
                .HasMaxLength(10)
                .HasColumnName("MACUONSACH");
            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.CuonSaches)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CuonSach__MASACH__47DBAE45");
        });

        modelBuilder.Entity<DkiMuonSach>(entity =>
        {
            entity.HasKey(e => e.Madk).HasName("PK__DkiMuonS__603F0042AE2E5A14");

            entity.ToTable("DkiMuonSach", tb =>
                {
                    tb.HasTrigger("SetTinhTrangPhieuDK");
                    tb.HasTrigger("TRG_SachMuonOnl");
                });

            entity.Property(e => e.Madk).HasColumnName("MADK");
            entity.Property(e => e.Ngaydkmuon).HasColumnName("NGAYDKMUON");
            entity.Property(e => e.Ngayhen).HasColumnName("NGAYHEN");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

            entity.HasOne(d => d.SdtNavigation).WithMany(p => p.DkiMuonSaches)
                .HasForeignKey(d => d.Sdt)
                .HasConstraintName("FK__DkiMuonSach__SDT__7E37BEF6");
        });

        modelBuilder.Entity<DocGium>(entity =>
        {
            entity.HasKey(e => e.Madg).HasName("PK__DocGia__603F004665A0F62F");

            entity.HasIndex(e => e.Sdt, "CHK_SDT_DG").IsUnique();

            entity.Property(e => e.Madg).HasColumnName("MADG");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(50)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hotendg)
                .HasMaxLength(50)
                .HasColumnName("HOTENDG");
            entity.Property(e => e.Ngaysinh).HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<DonViTl>(entity =>
        {
            entity.HasKey(e => e.Madv).HasName("PK__DonViTL__603F0055B11C9F6E");

            entity.ToTable("DonViTL");

            entity.HasIndex(e => e.Sdtdv, "CHK_SDT_DVTL").IsUnique();

            entity.Property(e => e.Madv).HasColumnName("MADV");
            entity.Property(e => e.Diachidv)
                .HasMaxLength(100)
                .HasColumnName("DIACHIDV");
            entity.Property(e => e.Sdtdv)
                .HasMaxLength(50)
                .HasColumnName("SDTDV");
            entity.Property(e => e.Tendv)
                .HasMaxLength(150)
                .HasColumnName("TENDV");
        });

        modelBuilder.Entity<KhoSachThanhLy>(entity =>
        {
            entity.HasKey(e => e.Masachkho).HasName("PK__KhoSachT__F0EC82866730F233");

            entity.ToTable("KhoSachThanhLy");

            entity.Property(e => e.Masachkho)
                .ValueGeneratedNever()
                .HasColumnName("MASACHKHO");
            entity.Property(e => e.Soluongkhotl).HasColumnName("SOLUONGKHOTL");
        });

        modelBuilder.Entity<LoginDg>(entity =>
        {
            entity.HasKey(e => e.Sdt).HasName("PK__LOGIN_DG__CA1930A4F84CBD30");

            entity.ToTable("LOGIN_DG");

            entity.HasIndex(e => e.Email, "CHK_EMAIL_LOGIN_DG").IsUnique();

            entity.HasIndex(e => e.Sdt, "CHK_SDT_LOGIN_DG").IsUnique();

            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .HasColumnName("HOTEN");
            entity.Property(e => e.PasswordDg)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD_DG");
        });

        modelBuilder.Entity<LoginNv>(entity =>
        {
            entity.HasKey(e => e.UsernameNv).HasName("PK__LOGIN_NV__296D7D766273A4EF");

            entity.ToTable("LOGIN_NV");

            entity.Property(e => e.UsernameNv)
                .HasMaxLength(50)
                .HasColumnName("USERNAME_NV");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.PasswordNv).HasColumnName("PASSWORD_NV");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.LoginNvs)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__LOGIN_NV__MANV__412EB0B6");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.Mancc).HasName("PK__NhaCungC__7ABEA582D2072C1E");

            entity.ToTable("NhaCungCap");

            entity.HasIndex(e => e.Sdtncc, "CHK_SDT_NCC").IsUnique();

            entity.Property(e => e.Mancc).HasColumnName("MANCC");
            entity.Property(e => e.Diachincc)
                .HasMaxLength(100)
                .HasColumnName("DIACHINCC");
            entity.Property(e => e.Sdtncc)
                .HasMaxLength(50)
                .HasColumnName("SDTNCC");
            entity.Property(e => e.Tenncc)
                .HasMaxLength(150)
                .HasColumnName("TENNCC");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("PK__NhanVien__603F51145BF4B39F");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Sdt, "CHK_SDT_NV").IsUnique();

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(50)
                .HasColumnName("CHUCVU");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(50)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hotennv)
                .HasMaxLength(50)
                .HasColumnName("HOTENNV");
            entity.Property(e => e.Ngaysinh).HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.Mapm).HasName("PK__PhieuMuo__603F61CD12F4DA68");

            entity.ToTable("PhieuMuon", tb => tb.HasTrigger("SetTinhTrangPM"));

            entity.Property(e => e.Mapm).HasColumnName("MAPM");
            entity.Property(e => e.Hantra).HasColumnName("HANTRA");
            entity.Property(e => e.Madk).HasColumnName("MADK");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Mathe).HasColumnName("MATHE");
            entity.Property(e => e.Ngaymuon).HasColumnName("NGAYMUON");
            entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__PhieuMuon__MANV__5441852A");

            entity.HasOne(d => d.MatheNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.Mathe)
                .HasConstraintName("FK__PhieuMuon__MATHE__5535A963");
        });

        modelBuilder.Entity<PhieuNhapSach>(entity =>
        {
            entity.HasKey(e => e.Mapn).HasName("PK__PhieuNha__603F61CEAED1D888");

            entity.ToTable("PhieuNhapSach");

            entity.Property(e => e.Mapn).HasColumnName("MAPN");
            entity.Property(e => e.Mancc).HasColumnName("MANCC");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaynhap).HasColumnName("NGAYNHAP");

            entity.HasOne(d => d.ManccNavigation).WithMany(p => p.PhieuNhapSaches)
                .HasForeignKey(d => d.Mancc)
                .HasConstraintName("FK__PhieuNhap__MANCC__4BAC3F29");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.PhieuNhapSaches)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__PhieuNhapS__MANV__4AB81AF0");
        });

        modelBuilder.Entity<PhieuThanhLy>(entity =>
        {
            entity.HasKey(e => e.Maptl).HasName("PK__PhieuTha__7B35DAB50185B150");

            entity.ToTable("PhieuThanhLy");

            entity.Property(e => e.Maptl).HasColumnName("MAPTL");
            entity.Property(e => e.Madv).HasColumnName("MADV");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaytl).HasColumnName("NGAYTL");

            entity.HasOne(d => d.MadvNavigation).WithMany(p => p.PhieuThanhLies)
                .HasForeignKey(d => d.Madv)
                .HasConstraintName("FK__PhieuThanh__MADV__73BA3083");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.PhieuThanhLies)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__PhieuThanh__MANV__72C60C4A");
        });

        modelBuilder.Entity<PhieuTra>(entity =>
        {
            entity.HasKey(e => e.Mapt).HasName("PK__PhieuTra__603F61D46B85B3CD");

            entity.ToTable("PhieuTra");

            entity.Property(e => e.Mapt).HasColumnName("MAPT");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Mapm).HasColumnName("MAPM");
            entity.Property(e => e.Mathe).HasColumnName("MATHE");
            entity.Property(e => e.Ngaytra).HasColumnName("NGAYTRA");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.PhieuTras)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__PhieuTra__MANV__60A75C0F");

            entity.HasOne(d => d.MapmNavigation).WithMany(p => p.PhieuTras)
                .HasForeignKey(d => d.Mapm)
                .HasConstraintName("FK__PhieuTra__MAPM__5FB337D6");

            entity.HasOne(d => d.MatheNavigation).WithMany(p => p.PhieuTras)
                .HasForeignKey(d => d.Mathe)
                .HasConstraintName("FK__PhieuTra__MATHE__619B8048");
        });

        modelBuilder.Entity<QuyDinh>(entity =>
        {
            entity.HasKey(e => e.NamXbmax).HasName("PK__QuyDinh__EDD1A17A57077B9C");

            entity.ToTable("QuyDinh");

            entity.Property(e => e.NamXbmax)
                .ValueGeneratedNever()
                .HasColumnName("NamXBMax");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__Sach__3FC48E4CEA4548CE");

            entity.ToTable("Sach");

            entity.Property(e => e.Masach).HasColumnName("MASACH");
            entity.Property(e => e.Mota)
                .HasColumnType("ntext")
                .HasColumnName("MOTA");
            entity.Property(e => e.Namxb).HasColumnName("NAMXB");
            entity.Property(e => e.Ngonngu)
                .HasMaxLength(50)
                .HasColumnName("NGONNGU");
            entity.Property(e => e.Nxb)
                .HasMaxLength(100)
                .HasColumnName("NXB");
            entity.Property(e => e.Soluonghientai).HasColumnName("SOLUONGHIENTAI");
            entity.Property(e => e.Tacgia)
                .HasMaxLength(50)
                .HasColumnName("TACGIA");
            entity.Property(e => e.Tensach)
                .HasMaxLength(150)
                .HasColumnName("TENSACH");
            entity.Property(e => e.Theloai)
                .HasMaxLength(50)
                .HasColumnName("THELOAI");
            entity.Property(e => e.UrlImage)
                .HasColumnType("text")
                .HasColumnName("URL_IMAGE");
        });

        modelBuilder.Entity<TheDocGium>(entity =>
        {
            entity.HasKey(e => e.Mathe).HasName("PK__TheDocGi__58248210EE6A7C53");

            entity.Property(e => e.Mathe).HasColumnName("MATHE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Madg).HasColumnName("MADG");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaydk).HasColumnName("NGAYDK");
            entity.Property(e => e.Ngayhh).HasColumnName("NGAYHH");
            entity.Property(e => e.Tienthe).HasColumnName("TIENTHE");

            entity.HasOne(d => d.MadgNavigation).WithMany(p => p.TheDocGia)
                .HasForeignKey(d => d.Madg)
                .HasConstraintName("FK__TheDocGia__MADG__3C69FB99");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.TheDocGia)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK__TheDocGia__MANV__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
