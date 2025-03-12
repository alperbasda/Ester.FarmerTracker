﻿using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;
using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Settings;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.BusinessRules;

public class HarvestBusinessRules(TokenParameters tokenParameters, ICustomerRepository customerRepository, IFieldRepository fieldRepository) : BaseBusinessRules(tokenParameters)
{
    public async Task ThrowExceptionIfLoginUserNotWriteAccessToField(Guid fieldId)
    {
        var loggedUser = GetUserId();
        var exists = await fieldRepository.AnyAsync(w => w.Id == fieldId &&
                                                   (loggedUser == w.CustomerId || IsUserAdmin() || w.Customer.SalesRepresantativeUserId == loggedUser));

        if (exists)
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
