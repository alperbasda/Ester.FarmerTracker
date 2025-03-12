using Alp.RepositoryAbstraction.Models;
using Alp.RepositoryAbstraction.Models.Dynamic;
using Ester.FarmetTracker.Common.Exceptions;
using Ester.FarmetTracker.Common.Settings;
using MassTransit;

namespace Ester.FarmerTracker.FieldService.Features._base;

public class BaseBusinessRules(TokenParameters tokenParameters)
{
    protected readonly TokenParameters TokenParameters = tokenParameters;

    public void ThrowExceptionIfDataNull(IEntity? entity)
    {

        if (entity == null)
        {
            throw new NotFoundException("Veri Bulunamadı !!!");
        }
    }

    public void FillFilters<T>(ListModel<T> data, DynamicQuery? dq, PageRequest pr)
    {
        data.DynamicQuery = dq ?? new DynamicQuery();
        data.PageRequest = pr;
    }

    public void SetId(IEntity entity)
    {
        entity.Id = NewId.NextSequentialGuid();
    }

    public Guid GetUserId() => TokenParameters.UserId;

    public bool IsUserAdmin() => TokenParameters.Roles.Exists(w => w == UserRole.Admin);

    public bool IsUserRepresantative() => TokenParameters.Roles.Exists(w => w == UserRole.Representative);
}
