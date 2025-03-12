using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories.Create;
using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Create;

public class CreateFertilizerHistoryCommandValidator : AbstractValidator<CreateFertilizerHistoryCommand>
{
    public CreateFertilizerHistoryCommandValidator(IDistributedCache distributedCache)
    {
        //RuleFor(w => w.UserId).NotNull().NotEmpty().WithMessage("Lütfen Sayfayı Yenileyin.");
        
    }
}



