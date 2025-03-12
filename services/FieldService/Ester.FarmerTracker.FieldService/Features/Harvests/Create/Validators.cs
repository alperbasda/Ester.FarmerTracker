using FluentValidation;

namespace Ester.FarmerTracker.FieldService.Features.Harvests.Create;

public class CreateHarvestCommandValidator : AbstractValidator<CreateHarvestCommand>
{
    public CreateHarvestCommandValidator()
    {
        RuleFor(w => w.FieldId).NotNull().NotEmpty().WithMessage("Lütfen 'Tarla' alanını doldurun.");
    }
}



