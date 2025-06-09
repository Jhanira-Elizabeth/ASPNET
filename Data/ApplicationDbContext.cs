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
    public virtual DbSet<GastoModel> Habitaciones { get; set; }
}
