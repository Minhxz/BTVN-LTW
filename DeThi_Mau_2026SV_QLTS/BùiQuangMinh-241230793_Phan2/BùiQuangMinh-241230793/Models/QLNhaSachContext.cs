using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BùiQuangMinh_241230793.Models;

public partial class QLNhaSachContext : DbContext
{
    public QLNhaSachContext(DbContextOptions<QLNhaSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sach> Sach { get; set; }

    public virtual DbSet<TheLoai> TheLoai { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach);

            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.Gia).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TenSach).HasMaxLength(100);

            entity.HasOne(d => d.MaTLNavigation).WithMany(p => p.Sach)
                .HasForeignKey(d => d.MaTL)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sach_TheLoai");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.HasKey(e => e.MaTL);

            entity.Property(e => e.TenTL).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
