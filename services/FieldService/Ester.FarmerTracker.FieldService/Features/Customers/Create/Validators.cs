using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FieldService.Features.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(IDistributedCache distributedCache)
    {
        RuleFor(w => w.Id).NotNull().NotEmpty().WithMessage("Lütfen 'Id' alanını doldurun.");
        RuleFor(w => w.IdentityNumber).NotNull().NotEmpty().WithMessage("Lütfen 'Kimlik Numarası' alanını doldurun.");
        RuleFor(w => w.Name).NotNull().NotEmpty().WithMessage("Lütfen 'Ad' alanını doldurun.");
        RuleFor(w => w.Surname).NotNull().NotEmpty().WithMessage("Lütfen 'Soyad' alanını doldurun.");
        RuleFor(w => w.PhoneNumber).NotNull().NotEmpty().WithMessage("Lütfen 'Telefon' alanını doldurun.");
        RuleFor(w => w.MailAddress).NotNull().NotEmpty().WithMessage("Lütfen 'E-Posta' alanını doldurun.");
        RuleFor(w => w.CityPlate).NotNull().NotEmpty().WithMessage("Lütfen 'Şehir' alanını doldurun.");
        RuleFor(w => w.City).NotNull().NotEmpty().WithMessage("Lütfen 'Şehir' alanını doldurun.");
        RuleFor(w => w.Address).NotNull().NotEmpty().WithMessage("Lütfen 'Şehir' alanını doldurun.");
    }
}



