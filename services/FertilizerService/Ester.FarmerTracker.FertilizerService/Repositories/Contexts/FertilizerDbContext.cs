using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ester.FarmerTracker.FertilizerService.Repositories.Contexts;

public class FertilizerDbContext : DbContext
{
    public FertilizerDbContext(DbContextOptions<FertilizerDbContext> opt) : base(opt)
    {

    }

    [Obsolete("Sadece DbContexti eklerken hata vermemesi için kullanılıyor.")]
    public FertilizerDbContext()
    {

    }

    public DbSet<Fertilizer> Fertilizers { get; set; } = default!;
    public DbSet<FertilizerHistory> FertilizerHistories { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}