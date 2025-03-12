using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Create;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.DeleteById;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.GetById;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.ListDynamic;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Update;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Fertilizer, CreateFertilizerResponse>();
        CreateMap<CreateFertilizerCommand, Fertilizer>();

        CreateMap<Fertilizer, UpdateFertilizerResponse>();
        CreateMap<UpdateFertilizerCommand, Fertilizer>();

        CreateMap<Fertilizer, DeleteFertilizerResponse>();
        CreateMap<DeleteFertilizerCommand, Fertilizer>();

        CreateMap<Fertilizer, GetByIdFertilizerResponse>();


        CreateMap<Fertilizer, ListDynamicFertilizerResponse>();

        CreateMap<Paginate<Fertilizer>, ListModel<ListDynamicFertilizerResponse>>();
    }
}
