using System;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Entidades;

namespace Cafeteria.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }
    public DbSet<Descuentos> Descuentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Configuración Cliente ---
        modelBuilder.Entity<Cliente>(entity => {
            entity.ToTable("Cliente");
            entity.Property(x => x.Nombre).HasMaxLength(50);
            entity.Property(x => x.Extension).HasMaxLength(2);
        });

        // --- Configuración Venta ---
        modelBuilder.Entity<Venta>(entity => {
            entity.ToTable("Venta");
            
            // Relación con Cliente (Uno a Muchos)
            entity.HasOne(v => v.Cliente)
                  .WithMany(c => c.Ventas)
                  .HasForeignKey(v => v.ClienteId);

            // Relación con Descuento (Uno a Muchos, Opcional)
            entity.HasOne(v => v.Descuento)
                  .WithMany(d => d.Ventas)
                  .HasForeignKey(v => v.Descuentoid);

            entity.Property(x => x.Total).HasColumnType("decimal(6,2)");
        });

        // --- Configuración Categoria ---
        modelBuilder.Entity<Categoria>().ToTable("Categoria");

        // --- Configuración Producto ---
        modelBuilder.Entity<Producto>(entity => {
            entity.ToTable("Producto");
            entity.Property(p => p.Precio).HasColumnType("decimal(8,2)");
            entity.Property(p => p.Stock).IsRequired();
            entity.HasOne(p => p.Categoria)
                  .WithMany(c => c.Productos)
                  .HasForeignKey(p => p.CategoriaId);
        });

        // --- Configuración DetalleVenta ---
        modelBuilder.Entity<DetalleVenta>(entity => {
            entity.ToTable("DetalleVenta");
            entity.Property(d => d.PrecioUnitario).HasColumnType("decimal(8,2)");
            
            entity.HasOne(d => d.Venta)
                  .WithMany() 
                  .HasForeignKey(d => d.VentaId);

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.Detalles)
                  .HasForeignKey(d => d.ProductoId);
        });

        // --- Configuración Descuentos ---
        modelBuilder.Entity<Descuentos>(entity => {
            entity.ToTable("Descuentos");
            entity.Property(d => d.Porcentaje).HasColumnType("decimal(8,2)");
        });
    }
}