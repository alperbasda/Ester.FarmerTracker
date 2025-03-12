using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.Configuration;

public class HarvestConfiguration : IEntityTypeConfiguration<Harvest>
{
    public void Configure(EntityTypeBuilder<Harvest> builder)
    {
        builder.ToTable("Harvests");
        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.FieldId);
        builder.Property(x => x.CropId).IsRequired(false); ;
        builder.Property(x => x.HarvestTime).IsRequired(false);
        
        builder.Property(x => x.CreatedTime);
        builder.Property(x => x.UpdatedTime);
        builder.Property(x => x.DeletedTime);

        builder.HasOne(w => w.Field);
        builder.HasOne(w => w.Crop);

    }
}
