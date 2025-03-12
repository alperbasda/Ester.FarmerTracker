namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Customers;

public record CreateCustomerRequest(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address,string Password);

public record CreateCustomerResponse(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address);
