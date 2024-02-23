using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos;

public partial class PruebaNtContext : DbContext
{
    public PruebaNtContext()
    {
    }

    public PruebaNtContext(DbContextOptions<PruebaNtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DataPruebaTecnica> DataPruebaTecnicas { get; set; }

    public virtual DbSet<Total> Totals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= PruebaNT; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cargo__3214EC07424A7600");

            entity.ToTable("Cargo");

            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Company).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cargo__company_i__145C0A3F");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__3E2672358E847A6B");

            entity.Property(e => e.CompanyId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(130)
                .IsUnicode(false)
                .HasColumnName("company_name");
        });

        modelBuilder.Entity<DataPruebaTecnica>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("data_prueba_tecnica");

            entity.Property(e => e.Amount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("amount");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_at");
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PaidAt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("paid_at ");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Total>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("total");

            entity.Property(e => e.CompanyId)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(130)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.Total1)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
