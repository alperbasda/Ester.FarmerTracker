using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Configuration;

public class FertilizerConfiguration : IEntityTypeConfiguration<Fertilizer>
{
    public void Configure(EntityTypeBuilder<Fertilizer> builder)
    {
        builder.ToCollection("fertilizer");
        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.UserId).HasElementName("userId");
        builder.Property(x => x.UserFullName).HasElementName("userFullName");
        builder.Property(x => x.SerialNumber).HasElementName("serialNumber");
        builder.Property(x => x.TotalAmount).HasElementName("totalAmount");
        builder.Property(x => x.RemainingAmount).HasElementName("remainingAmount");
        builder.Property(x => x.Status).HasElementName("status");
        builder.Property(x => x.ExpirationTime).HasElementName("expirationTime");

        builder.Property(x => x.CreatedTime).HasElementName("createdTime");
        builder.Property(x => x.UpdatedTime).HasElementName("updatedTime");
        builder.Property(x => x.DeletedTime).HasElementName("deletedTime");
        

        builder.HasIndex(w => w.UserId);
        builder.HasIndex(w => w.DeletedTime);

    }
}
