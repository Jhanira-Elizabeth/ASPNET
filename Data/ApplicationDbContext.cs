using HOTEL_MEDITERRANEO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_MEDITERRANEO.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VentasModel> Ventas { get; set; }
    public virtual DbSet<GastoModel> Gastos { get; set; } // ¡Asegúrate de que este es 'Gastos'!
    public DbSet<PresupuestoModel> Presupuestos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración para el PresupuestoModel (para que siempre exista uno por defecto)
        modelBuilder.Entity<PresupuestoModel>().HasData(
            new PresupuestoModel { Id = 1, MontoTotal = 0.00m, FechaActualizacion = DateTime.Now }
        );

        // Opcional pero recomendado: Configuración de precisión para tipos decimal
        modelBuilder.Entity<GastoModel>()
            .Property(g => g.Monto)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<VentasModel>()
            .Property(v => v.Monto)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<PresupuestoModel>()
            .Property(p => p.MontoTotal)
            .HasColumnType("decimal(18, 2)");
    }
}