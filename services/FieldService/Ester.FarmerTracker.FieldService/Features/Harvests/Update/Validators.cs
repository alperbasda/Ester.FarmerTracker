using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FieldService.Features.Harvests.Update;

public class UpdateHarvestCommandValidator : AbstractValidator<UpdateHarvestCommand>
{
    public UpdateHarvestCommandValidator(IDistributedCache distributedCache)
    {
        RuleFor(w => w.FieldId).NotNull().NotEmpty().WithMessage("Lütfen 'Tarla' alanını doldurun.");
    }
}
