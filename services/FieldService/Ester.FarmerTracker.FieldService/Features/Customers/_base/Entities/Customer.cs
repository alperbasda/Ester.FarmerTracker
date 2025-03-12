using Alp.MsSqlAdapter.Models;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;

public class Customer : Entity
{
    public Guid? SalesRepresantativeUserId { get; set; }

    public string? SalesRepresantativeUserName { get; set; }

    public long IdentityNumber { get; set; }

    public string Name { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public string MailAddress { get; set; } = default!;

    public int CityPlate { get; set; }

    public string City { get; set; } = default!;

    public string Address { get; set; } = default!;

    public ICollection<Field> Fields { get; set; } = [];
}
