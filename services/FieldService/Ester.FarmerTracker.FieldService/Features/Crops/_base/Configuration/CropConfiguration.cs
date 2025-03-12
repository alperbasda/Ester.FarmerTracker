using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ester.FarmerTracker.FieldService.Features.Crops._base.Configuration;

public class CropConfiguration : IEntityTypeConfiguration<Crop>
{
    public void Configure(EntityTypeBuilder<Crop> builder)
    {
        builder.ToTable("Crops");
        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);


        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.Description);

        builder.Property(x => x.CreatedTime);
        builder.Property(x => x.UpdatedTime);
        builder.Property(x => x.DeletedTime);

        builder.HasMany(w => w.Harvests);

        
    }
}
