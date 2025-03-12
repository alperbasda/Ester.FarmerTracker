using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Update;

public class UpdateFertilizerCommandValidator : AbstractValidator<UpdateFertilizerCommand>
{
    public UpdateFertilizerCommandValidator(IDistributedCache distributedCache)
    {
        RuleFor(w => w.UserId).NotNull().NotEmpty().WithMessage("Lütfen Sayfayı Yenileyin.");
        RuleFor(w => w.UserFullName).NotNull().NotEmpty().WithMessage("Lütfen Sayfayı Yenileyin.");
        RuleFor(w => w.SerialNumber).NotNull().NotEmpty().WithMessage("Lütfen 'Seri Numarası' alanını doldurun.");
        RuleFor(w => w.TotalAmount).NotNull().NotEmpty().WithMessage("Lütfen 'Miktar' alanını doldurun.");
        RuleFor(w => w.Status).NotNull().NotEmpty().WithMessage("Lütfen 'Durum' alanını doldurun.");
        RuleFor(w => w.ExpirationTime).NotNull().NotEmpty().WithMessage("Lütfen 'Son Kullan Tarihi' alanını doldurun.");
    }
}
