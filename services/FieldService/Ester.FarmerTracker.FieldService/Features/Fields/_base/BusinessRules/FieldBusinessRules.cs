using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Settings;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base.BusinessRules;

public class FieldBusinessRules(TokenParameters tokenParameters, ICustomerRepository customerRepository) : BaseBusinessRules(tokenParameters)
{
    public async Task ThrowExceptionIfLoginUserNotWriteAccessToField(Guid customerId)
    {
        var loggedUser = GetUserId();
        if (loggedUser == customerId || IsUserAdmin() || await customerRepository.AnyAsync(w => w.SalesRepresantativeUserId == loggedUser))
            return;

        throw new BusinessException("Bu işlem için yetkiniz yok.");
    }

    public async Task<List<Guid>> GetRepresentiveCustomers()
    {
        List<Guid> returnList = [];

        var loggedUser = GetUserId();
        if (IsUserRepresantative())
        {
            returnList.AddRange(
                (await customerRepository.ListAsync(w => w.SalesRepresantativeUserId == loggedUser, size: int.MaxValue, index: 0)).Items.Select(w => w.Id)
                );
        }
        return returnList;
    }
}
