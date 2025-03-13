using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Harvests.Create;
using Ester.FarmerTracker.FieldService.Features.Harvests.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Harvests.GetById;
using Ester.FarmerTracker.FieldService.Features.Harvests.GetLast;
using Ester.FarmerTracker.FieldService.Features.Harvests.ListDynamic;

namespace Ester.FarmerTracker.FieldService.Features.Harvests._base.Profiles;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
        CreateMap<Harvest, CreateHarvestResponse>();
        CreateMap<CreateHarvestCommand, Harvest>();

        CreateMap<Harvest, DeleteHarvestResponse>();
        CreateMap<DeleteHarvestCommand, Harvest>();

        CreateMap<Harvest, GetByIdHarvestResponse>();
        CreateMap<Harvest, GetLastHarvestResponse>();
        

        CreateMap<Harvest, ListDynamicHarvestResponse>();

        CreateMap<Paginate<Harvest>, ListModel<ListDynamicHarvestResponse>>();
    }
}
