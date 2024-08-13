using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lonches_Restaurant.Models;

public partial class LonchesRestaurantContext : DbContext
{
    public LonchesRestaurantContext()
    {
    }

    public LonchesRestaurantContext(DbContextOptions<LonchesRestaurantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ComandaDetalle> ComandaDetalles { get; set; }

    public virtual DbSet<Comandum> Comanda { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoMesaIntermedium> EmpleadoMesaIntermedia { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Produccion> Produccions { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LONCHES; Initial Catalog=lonches_restaurant; user id=sa; password=root;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__677F38F5E66C4FBC");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdMesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_cliente_mesa");
        });

        modelBuilder.Entity<ComandaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdComandaDetalle).HasName("PK__comanda___BF1F6694B0FC3AB2");

            entity.ToTable("comanda_detalle");

            entity.Property(e => e.IdComandaDetalle).HasColumnName("id_comanda_detalle");
            entity.Property(e => e.CantidadPedida).HasColumnName("cantidad_pedida");
            entity.Property(e => e.IdComanda).HasColumnName("id_comanda");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .HasColumnName("observaciones");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.ComandaDetalles)
                .HasForeignKey(d => d.IdComanda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_comanda_comanda");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ComandaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_comanda_producto");
        });

        modelBuilder.Entity<Comandum>(entity =>
        {
            entity.HasKey(e => e.IdComanda).HasName("PK__comanda__6D6D170D711F9C1C");

            entity.ToTable("comanda");

            entity.Property(e => e.IdComanda).HasColumnName("id_comanda");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado"); // Nueva propiedad para la clave foránea

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Comanda)
                .HasForeignKey(d => d.IdMesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_comanda_mesa");

            entity.HasOne(d => d.IdClienteNavigation)
                .WithMany(p => p.Comanda)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_comanda_cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation) // Configuración de la relación con Empleado
                .WithMany(p => p.Comanda)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_comanda_empleado");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__empleado__88B51394BC852F41");

            entity.ToTable("empleado");

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.ApeMaterno)
                .HasMaxLength(255)
                .HasColumnName("ape_materno");
            entity.Property(e => e.ApePaterno)
                .HasMaxLength(255)
                .HasColumnName("ape_paterno");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(255)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol).HasColumnName("rol");
        });

        modelBuilder.Entity<EmpleadoMesaIntermedium>(entity =>
        {
            entity.HasKey(e => e.IdEmpleadoMesa).HasName("PK__empleado__E0EEFA74198E8502");

            entity.ToTable("empleado_mesa_intermedia");

            entity.Property(e => e.IdEmpleadoMesa).HasColumnName("id_empleado_mesa");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.EmpleadoMesaIntermedia)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_empleado_mesa");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.EmpleadoMesaIntermedia)
                .HasForeignKey(d => d.IdMesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_mesa_empleado");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa).HasName("PK__mesa__68A1E15942B7D0A4");

            entity.ToTable("mesa");

            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.NumMesa)
                .HasMaxLength(25)
                .HasColumnName("num_mesa");
        });

        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.IdProduccion).HasName("PK__producci__9EBBA4335C138B7A");

            entity.ToTable("produccion");

            entity.Property(e => e.IdProduccion).HasColumnName("id_produccion");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdComanda).HasColumnName("id_comanda");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.IdComanda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_produccion_comanda");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Produccions)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_produccion_empleado");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__producto__FF341C0DF7F68424");

            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.Fotografia)
                .HasColumnType("text")
                .HasColumnName("fotografia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalle).HasName("PK__venta_de__7AA8F41BB9F3BF5A");

            entity.ToTable("venta_detalle");

            entity.Property(e => e.IdVentaDetalle).HasColumnName("id_venta_detalle");
            entity.Property(e => e.CantidadVendida).HasColumnName("cantidad_vendida");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .HasColumnName("observaciones");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_venta_producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_venta_venta");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__venta__459533BF44ED1F6A");

            entity.ToTable("venta");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdComanda).HasColumnName("id_comanda");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_venta_cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_venta_empleado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
