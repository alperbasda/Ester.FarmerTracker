using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ester.FarmerTracker.FieldService.Repositories.Contexts;

public class FieldDbContext : DbContext
{
    public FieldDbContext(DbContextOptions<FieldDbContext> opt) : base(opt)
    {

    }

    [Obsolete("Sadece DbContexti eklerken hata vermemesi için kullanılıyor.")]
    public FieldDbContext()
    {

    }

    public DbSet<Crop> Crops { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Field> Fields { get; set; } = default!;
    public DbSet<Harvest> Harvests { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}