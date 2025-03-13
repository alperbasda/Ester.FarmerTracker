using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasQueryFilter(w => !w.DeletedTime.HasValue);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.SalesRepresantativeUserId).IsRequired(false);
        builder.Property(x => x.SalesRepresantativeUserName).IsRequired(false);
        builder.Property(x => x.IdentityNumber);
        builder.Property(x => x.Name);
        builder.Property(x => x.Surname);
        builder.Property(x => x.PhoneNumber);
        builder.Property(x => x.MailAddress);
        builder.Property(x => x.CityPlate);
        builder.Property(x => x.City);
        builder.Property(x => x.Address);
        builder.Property(x => x.FieldsSquereMeterSum);
        


        builder.Property(x => x.CreatedTime);
        builder.Property(x => x.UpdatedTime);
        builder.Property(x => x.DeletedTime);

        builder.HasMany(w => w.Fields);
        builder.HasIndex(x => x.SalesRepresantativeUserId);
        builder.HasIndex(x => x.CityPlate);


    }
}
