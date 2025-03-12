using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FieldService.Features.Crops.Update;

public class UpdateCropCommandValidator : AbstractValidator<UpdateCropCommand>
{
    public UpdateCropCommandValidator(IDistributedCache distributedCache)
    {
        RuleFor(w => w.Description).NotNull().NotEmpty().WithMessage("Lütfen 'Açıklama' alanını doldurun.");
        RuleFor(w => w.Name).NotNull().NotEmpty().WithMessage("Lütfen 'Ad' alanını doldurun.");
    }
}
