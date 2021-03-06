﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class KLTNContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public KLTNContext()
        {
        }

        public KLTNContext(DbContextOptions<KLTNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaiPost> BaiPost { get; set; }
        public virtual DbSet<BaoCaoTienDo> BaoCaoTienDo { get; set; }
        public virtual DbSet<BoNhiem> BoNhiem { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<CtxetDuyetVaDanhGia> CtxetDuyetVaDanhGia { get; set; }
        public virtual DbSet<DeTaiNghienCuu> DeTaiNghienCuu { get; set; }
        public virtual DbSet<GiangVien> GiangVien { get; set; }
        public virtual DbSet<HoiDong> HoiDong { get; set; }
        public virtual DbSet<ImgBaiPost> ImgBaiPost { get; set; }
        public virtual DbSet<MoDot> MoDot { get; set; }
        public virtual DbSet<NamHoc> NamHoc { get; set; }
        public virtual DbSet<Nhom> Nhom { get; set; }
        public virtual DbSet<NhomSinhVien> NhomSinhVien { get; set; }
        public virtual DbSet<QuanLy> QuanLy { get; set; }
        public virtual DbSet<SinhVien> SinhVien { get; set; }
        public virtual DbSet<XetDuyetVaDanhGia> XetDuyetVaDanhGia { get; set; }
        public virtual DbSet<YeuCauPheDuyet> YeuCauPheDuyet { get; set; }
        public virtual DbSet<YCChinhSuaDeTai> YCChinhSuaDeTai { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ERP-HAIDT\\SQLEXPRESS;Database=KLTN;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x=> new {x.UserId,x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x=>x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x=>x.UserId);

            modelBuilder.Entity<BaiPost>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IddeTaiNghienCuu).HasColumnName("IDDeTaiNghienCuu");

                entity.Property(e => e.IdnguoiTao).HasColumnName("IDNguoiTao");

                entity.Property(e => e.NgayPost).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(150);

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.HasOne(d => d.IddeTaiNghienCuuNavigation)
                    .WithMany(p => p.BaiPost)
                    .HasForeignKey(d => d.IddeTaiNghienCuu)
                    .HasConstraintName("FK__BaiPost__IDDeTa__6754599E");
            });

            modelBuilder.Entity<BaoCaoTienDo>(entity =>
            {
                entity.HasIndex(e => e.IddeTai);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IddeTai).HasColumnName("IDDeTai");

                entity.Property(e => e.NgayNop).HasColumnType("date");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.Property(e => e.TienDo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IddeTaiNavigation)
                    .WithMany(p => p.BaoCaoTienDo)
                    .HasForeignKey(d => d.IddeTai)
                    .HasConstraintName("FK__BaoCaoTie__IDDeT__5DCAEF64");
            });

           

            modelBuilder.Entity<BoNhiem>(entity =>
            {
                entity.HasIndex(e => e.IdhoiDong);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdgiangVien).HasColumnName("IDGiangVien");

                entity.Property(e => e.IdhoiDong).HasColumnName("IDHoiDong");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.HasOne(d => d.IdgiangVienNavigation)
                    .WithMany(p => p.BoNhiem)
                    .HasForeignKey(d => d.IdgiangVien)
                    .HasConstraintName("FK__BoNhiem__IDGiang__49C3F6B7");

                entity.HasOne(d => d.IdhoiDongNavigation)
                    .WithMany(p => p.BoNhiem)
                    .HasForeignKey(d => d.IdhoiDong)
                    .HasConstraintName("FK__BoNhiem__IDHoiDo__4BAC3F29");

            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasIndex(e => e.IdbaiPost);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdbaiPost).HasColumnName("IDBaiPost");

                entity.Property(e => e.IdnguoiTao).HasColumnName("IDNguoiTao");

                entity.Property(e => e.NgayPost).HasColumnType("datetime");

                entity.HasOne(d => d.IdbaiPostNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdbaiPost)
                    .HasConstraintName("FK__Comments__IDBaiP__6A30C649");
            });

            modelBuilder.Entity<CtxetDuyetVaDanhGia>(entity =>
            {
                entity.ToTable("CTXetDuyetVaDanhGia");

                entity.HasIndex(e => e.IdxetDuyet);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdgiangVien).HasColumnName("IDGiangVien");

                entity.Property(e => e.IdxetDuyet).HasColumnName("IDXetDuyet");

                entity.Property(e => e.NgayTaoCauHoi).HasColumnType("datetime");

                entity.Property(e => e.NgayDanhGia).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.HasOne(d => d.IdxetDuyetNavigation)
                    .WithMany(p => p.CtxetDuyetVaDanhGia)
                    .HasForeignKey(d => d.IdxetDuyet)
                    .HasConstraintName("FK__CTXetDuye__IDXet__5AEE82B9");

                entity.HasOne(d => d.IdgiangVienNavigation)
                    .WithMany(p => p.CtxetDuyetVaDanhGia)
                    .HasForeignKey(d => d.IdgiangVien)
                    .HasConstraintName("FK__CTXetDuye__IDGia__7AEE31B7");
            });

            modelBuilder.Entity<DeTaiNghienCuu>(entity =>
            {
                entity.HasIndex(e => e.IdgiangVien);


                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdgiangVien).HasColumnName("IDGiangVien");
                entity.Property(e => e.IdNguoiDangKy).HasColumnName("IDNguoiDangKy");
                entity.Property(e => e.NgayLap).HasColumnType("datetime");
                entity.Property(e => e.TinhTrangDangKy).HasDefaultValue(1);
                entity.Property(e => e.TinhTrangDeTai).HasDefaultValue(1);
                entity.Property(e => e.TinhTrangPheDuyet).HasDefaultValue(0);
                entity.Property(e => e.TinhTrangPhanCong).HasDefaultValue(0);

                entity.HasOne(d => d.IdgiangVienNavigation)
                    .WithMany(p => p.DeTaiNghienCuu)
                    .HasForeignKey(d => d.IdgiangVien)
                    .HasConstraintName("FK__DeTaiNghi__IDGia__4E88ABD4");

            });

            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ho).HasMaxLength(30);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ten).HasMaxLength(100);
            });

            modelBuilder.Entity<HoiDong>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValue(1);
                entity.Property(e => e.StatusPhanCong).HasDefaultValue(0);
            });

            modelBuilder.Entity<ImgBaiPost>(entity =>
            {
                entity.ToTable("imgBaiPost");

                entity.HasIndex(e => e.IdbaiPost);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdbaiPost).HasColumnName("IDBaiPost");

                entity.HasOne(d => d.IdbaiPostNavigation)
                    .WithMany(p => p.ImgBaiPost)
                    .HasForeignKey(d => d.IdbaiPost)
                    .HasConstraintName("FK__imgBaiPost__IDBaiP__6A30C649");
            });

            modelBuilder.Entity<MoDot>(entity =>
            {
                entity.HasIndex(e => e.IdnamHoc);

                entity.HasIndex(e => e.IdquanLy);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdnamHoc).HasColumnName("IDNamHoc");

                entity.Property(e => e.IdquanLy).HasColumnName("IDQuanLy");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.Property(e => e.ThoiGianBd)
                    .HasColumnName("ThoiGianBD")
                    .HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKt)
                    .HasColumnName("ThoiGianKT")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdnamHocNavigation)
                    .WithMany(p => p.MoDot)
                    .HasForeignKey(d => d.IdnamHoc)
                    .HasConstraintName("FK__MoDot__IDNamHoc__52593CB8");

                entity.HasOne(d => d.IdquanLyNavigation)
                    .WithMany(p => p.MoDot)
                    .HasForeignKey(d => d.IdquanLy)
                    .HasConstraintName("FK__MoDot__IDQuanLy__534D60F1");
            });

            modelBuilder.Entity<NamHoc>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Nhom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Status).HasDefaultValue(1);
            });

            modelBuilder.Entity<NhomSinhVien>(entity =>
            {
                entity.HasKey(e => new { e.Idnhom, e.IdsinhVien, e.IddeTai })
                    .HasName("PK__Nhom_Sin__CD149E43F71CE773");

                entity.ToTable("Nhom_SinhVien");

                entity.HasIndex(e => e.IdsinhVien);

                entity.Property(e => e.Idnhom).HasColumnName("IDNhom");

                entity.Property(e => e.IdsinhVien).HasColumnName("IDSinhVien");

                entity.Property(e => e.IddeTai).HasColumnName("IDDeTaiNghienCuu");

                entity.HasOne(d => d.IdnhomNavigation)
                    .WithMany(p => p.NhomSinhVien)
                    .HasForeignKey(d => d.Idnhom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nhom_Sinh__Statu__3F466844");

                entity.HasOne(d => d.IddeTaiNavigation)
                    .WithMany(p => p.NhomSinhVien)
                    .HasForeignKey(d => d.IddeTai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nhom_Sinh__IDDet__3F416244");

                entity.HasOne(d => d.IdsinhVienNavigation)
                    .WithMany(p => p.NhomSinhVien)
                    .HasForeignKey(d => d.IdsinhVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nhom_Sinh__IDSin__403A8C7D");
            });

            modelBuilder.Entity<QuanLy>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ho).HasMaxLength(30);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ten).HasMaxLength(100);

                entity.Property(e => e.Status).HasDefaultValue(1);
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.Mssv)
                    .HasName("PK__SinhVien__6CB3B7F96B8F18DF");

                entity.Property(e => e.Mssv)
                    .HasColumnName("MSSV")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ho).HasMaxLength(30);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ten).HasMaxLength(100);

                entity.Property(e => e.Status).HasDefaultValue(1);
            });

            modelBuilder.Entity<YCChinhSuaDeTai>(entity =>
            {
                entity.HasIndex(e => e.IDDeTai);
                entity.Property(e => e.IDDeTai).HasColumnName("IDDeTai");
                entity.Property(e => e.Status).HasDefaultValue(0);

                entity.HasOne(d => d.IddeTaiNghienCuuNavigation)
                    .WithMany(p => p.YCChinhSuaDeTai)
                    .HasForeignKey(d => d.IDDeTai)
                    .HasConstraintName("FK__YCChinh__IDDeT__6DZFFF12");

            });

            modelBuilder.Entity<YeuCauPheDuyet>(entity =>
            {
                entity.HasIndex(e => e.IddeTai);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IddeTai).HasColumnName("IDDeTai");

                entity.Property(e => e.NgayTao).HasColumnType("date");
                entity.Property(e => e.NgayDuyet).HasColumnType("date");

                entity.Property(e => e.Status).HasDefaultValue(0);

                entity.HasOne(d => d.IddeTaiNghienCuuNavigation)
                    .WithMany(p => p.YeuCauPheDuyet)
                    .HasForeignKey(d => d.IddeTai)
                    .HasConstraintName("FK__YeuCauPhe__IDDeT__6DCBF134");
            });

            modelBuilder.Entity<XetDuyetVaDanhGia>(entity =>
            {
                entity.HasIndex(e => e.IddeTai);

                entity.HasIndex(e => e.IdhoiDong);

                entity.HasIndex(e => e.IdmoDot);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IddeTai).HasColumnName("IDDeTai");

                entity.Property(e => e.IdhoiDong).HasColumnName("IDHoiDong");

                entity.Property(e => e.IdmoDot).HasColumnName("IDMoDot");

                entity.Property(e => e.Status).HasDefaultValue(1);

                entity.HasOne(d => d.IddeTaiNavigation)
                    .WithMany(p => p.XetDuyetVaDanhGia)
                    .HasForeignKey(d => d.IddeTai)
                    .HasConstraintName("FK__XetDuyetV__IDDeT__5629CD9C");

                entity.HasOne(d => d.IdhoiDongNavigation)
                    .WithMany(p => p.XetDuyetVaDanhGia)
                    .HasForeignKey(d => d.IdhoiDong)
                    .HasConstraintName("FK__XetDuyetV__IDHoi__571DF1D5");

                entity.HasOne(d => d.IdmoDotNavigation)
                    .WithMany(p => p.XetDuyetVaDanhGia)
                    .HasForeignKey(d => d.IdmoDot)
                    .HasConstraintName("FK__XetDuyetV__IDMoD__5812160E");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
