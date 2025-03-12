using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FieldService.Features.Crops.Create;

public class CreateCropCommandValidator : AbstractValidator<CreateCropCommand>
{
    public CreateCropCommandValidator(IDistributedCache distributedCache)
    {
        RuleFor(w => w.Description).NotNull().NotEmpty().WithMessage("Lütfen 'Açıklama' alanını doldurun.");
        RuleFor(w => w.Name).NotNull().NotEmpty().WithMessage("Lütfen 'Ad' alanını doldurun.");
    }
}



