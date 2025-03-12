using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Fields;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Customers;

public record GetByIdCustomerResponse(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);
public record ListCustomerResponse(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);

