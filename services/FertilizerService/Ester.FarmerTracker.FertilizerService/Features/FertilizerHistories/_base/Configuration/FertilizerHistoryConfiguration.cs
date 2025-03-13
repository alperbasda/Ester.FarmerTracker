using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Configuration;

public class FertilizerHistoryConfiguration : IEntityTypeConfiguration<FertilizerHistory>
{
    public void Configure(EntityTypeBuilder<FertilizerHistory> builder)
    {
        builder.ToCollection("fertilizerhistory");
        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.FertilizerId).HasElementName("fertilizerId");
        builder.Property(x => x.Description).HasElementName("description");
        builder.Property(x => x.Action).HasElementName("action");

        builder.Property(x => x.CreatedTime).HasElementName("createdTime");
        builder.Property(x => x.UpdatedTime).HasElementName("updatedTime");
        builder.Property(x => x.DeletedTime).HasElementName("deletedTime");

        builder.OwnsOne(w => w.TransferActionFertilizerHistoryDetail, feature =>
        {
            feature.HasElementName("transferdetail");
            feature.Property(w => w.RecipientId).HasElementName("recipientId");
            feature.Property(w => w.RecipientName).HasElementName("recipientName");
            feature.Property(w => w.GiverId).HasElementName("giverId");
            feature.Property(w => w.GiverName).HasElementName("giverName");
        });

        builder.OwnsOne(w => w.LossActionFertilizerHistoryDetail, feature =>
        {
            feature.HasElementName("lossdetail");
            feature.Property(w => w.Amount).HasElementName("amount");
        });
        builder.OwnsOne(w => w.ThrowActionFertilizerHistoryDetail, feature =>
        {
            feature.HasElementName("throwdetail");
            feature.Property(w => w.Amount).HasElementName("amount");
            feature.Property(w => w.FieldId).HasElementName("fieldId");
        });

        builder.HasIndex(w => w.FertilizerId);
        builder.HasIndex(w => w.DeletedTime);

    }
}
