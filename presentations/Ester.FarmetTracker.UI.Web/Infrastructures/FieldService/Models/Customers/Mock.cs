namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Customers;

public class MockCustomer
{
    public Guid Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public Guid? SalesRepresantativeUserId { get; set; }

    public string? SalesRepresantativeUserName { get; set; }

    public long IdentityNumber { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string PhoneNumber { get; set; }

    public string MailAddress { get; set; }

    public int CityPlate { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

}
