using System;
using System.Collections.Generic;
using FacturacionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FacturacionAPI.Repository;

public partial class AplicationDbContext : DbContext
{
    public AplicationDbContext()
    {
    }

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FamiliaProducto> FamiliaProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-K3LB2V2;Initial Catalog=SistemaFacturacion;Integrated Security=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PK__DetalleF__51E842620E51AA7E");

            entity.ToTable("DetalleFactura", tb => tb.HasTrigger("DisminuirStock"));

            entity.HasOne(d => d.CodigoProductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasPrincipalKey(p => p.Codigo)
                .HasForeignKey(d => d.CodigoProducto)
                .HasConstraintName("FK__DetalleFa__Codig__36B12243");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas).HasConstraintName("FK__DetalleFa__IdFac__35BCFE0A");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__50E7BAF16EAFD767");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturas).HasConstraintName("FK__Facturas__IdUsua__398D8EEE");
        });

        modelBuilder.Entity<FamiliaProducto>(entity =>
        {
            entity.HasKey(e => e.IdFamilia).HasName("PK__FamiliaP__751F80CF9B035409");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.FamiliaProductos).HasConstraintName("FK__FamiliaPr__IdUsu__3A81B327");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210715263C6");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.Productos).HasConstraintName("FK__Productos__IdFam__2E1BDC42");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Productos).HasConstraintName("FK__Productos__IdUsu__3B75D760");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF97A2B7BC03");

            entity.Property(e => e.Bloqueado).HasDefaultValueSql("((0))");
            entity.Property(e => e.IntentosFallidos).HasDefaultValueSql("((0))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
