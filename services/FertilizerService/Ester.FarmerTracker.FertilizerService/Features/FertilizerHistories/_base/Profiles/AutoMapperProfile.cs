using AutoMapper;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities.Aggregations;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories.Create;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateFertilizerHistoryCommand, FertilizerHistory>()
            .ForMember(w => w.LossActionFertilizerHistoryDetail, q => q.MapFrom(c => c.ActionRequest.Loss))
            .ForMember(w => w.ThrowActionFertilizerHistoryDetail, q => q.MapFrom(c => c.ActionRequest.Throw))
            .ForMember(w => w.TransferActionFertilizerHistoryDetail, q => q.MapFrom(c => c.ActionRequest.Transfer));

        CreateMap<FertilizerHistory, CreateFertilizerHistoryResponse>();
        CreateMap<TransferActionRequest, TransferActionFertilizerHistoryDetail>();
        CreateMap<LossActionRequest, LossActionFertilizerHistoryDetail>();
        CreateMap<ThrowActionRequest, ThrowActionFertilizerHistoryDetail>();
    }
}
