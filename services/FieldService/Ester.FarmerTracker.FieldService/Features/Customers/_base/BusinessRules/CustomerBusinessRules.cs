using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Settings;

namespace Ester.FarmerTracker.FieldService.Features.Customers._base.BusinessRules;

public class CustomerBusinessRules(TokenParameters tokenParameters) : BaseBusinessRules(tokenParameters)
{
    public void SetCustomerRepresantiveInfoIfNotAdmin(Customer customer)
    {
        if (IsUserAdmin())
            return;
        if (IsUserRepresantative())
        {
            customer.SalesRepresantativeUserId = base.TokenParameters.UserId;
            customer.SalesRepresantativeUserName = base.TokenParameters.UserName;
        }
    }

    public void ThrowExceptionIfCustomerRepresantiveNotLoggedUser(Customer customer)
    {
        if (IsUserAdmin() || base.TokenParameters.UserId == customer.SalesRepresantativeUserId || base.TokenParameters.UserId == customer.Id)
            return;

        throw new BusinessException($"{customer.Name} {customer.Surname} kullanıcısı üzerinde işlem yapma yetkiniz bulunmamaktadır.");
    }
}
