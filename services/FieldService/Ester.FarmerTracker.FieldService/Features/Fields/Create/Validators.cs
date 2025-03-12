using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FieldService.Features.Fields.Create;

public class CreateFieldCommandValidator : AbstractValidator<CreateFieldCommand>
{
    public CreateFieldCommandValidator()
    {
        RuleFor(w => w.CustomerId).NotNull().NotEmpty().WithMessage("Lütfen 'Kullanıcı' alanını doldurun.");
        RuleFor(w => w.Name).NotNull().NotEmpty().WithMessage("Lütfen 'Ad' alanını doldurun.");
        RuleFor(w => w.Coordinate).NotNull().NotEmpty().WithMessage("Lütfen 'Kordinat' alanını doldurun.");
        RuleFor(w => w.SquareMeter).NotNull().NotEmpty().WithMessage("Lütfen 'Metre Kare' alanını doldurun.");
        RuleFor(w => w.CityPlate).NotNull().NotEmpty().WithMessage("Lütfen 'Şehir' alanını doldurun.");
        RuleFor(w => w.City).NotNull().NotEmpty().WithMessage("Lütfen 'Şehir' alanını doldurun.");
        RuleFor(w => w.Address).NotNull().NotEmpty().WithMessage("Lütfen 'Şehir' alanını doldurun.");
    }
}



