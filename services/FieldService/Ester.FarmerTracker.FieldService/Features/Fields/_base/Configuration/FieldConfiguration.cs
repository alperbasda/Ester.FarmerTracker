using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base.Configuration;

public class FieldConfiguration : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.ToTable("Fields");
        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CustomerId);
        builder.Property(x => x.Name);
        builder.Property(x => x.Coordinate);
        builder.Property(x => x.SquareMeter);
        builder.Property(x => x.CityPlate);
        builder.Property(x => x.City);
        builder.Property(x => x.Address);
        builder.Property(x => x.CurrentCropName).IsRequired(false);
        builder.Property(x => x.CurrentTotalFertilizerAmount);

        builder.Property(x => x.CreatedTime);
        builder.Property(x => x.UpdatedTime);
        builder.Property(x => x.DeletedTime);

        builder.HasOne(w => w.Customer);
        builder.HasMany(w => w.Harvests);

        builder.HasIndex(x => x.CityPlate);
    }
}
