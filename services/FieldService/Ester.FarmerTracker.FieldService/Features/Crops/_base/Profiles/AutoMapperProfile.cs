using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Crops.Create;
using Ester.FarmerTracker.FieldService.Features.Crops.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Crops.GetById;
using Ester.FarmerTracker.FieldService.Features.Crops.ListDynamic;
using Ester.FarmerTracker.FieldService.Features.Crops.Update;

namespace Ester.FarmerTracker.FieldService.Features.Crops._base.Profiles;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
        CreateMap<Crop, CreateCropResponse>();
        CreateMap<CreateCropCommand, Crop>();

        CreateMap<Crop, UpdateCropResponse>();
        CreateMap<UpdateCropCommand, Crop>();

        CreateMap<Crop, DeleteCropResponse>();
        CreateMap<DeleteCropCommand, Crop>();

        CreateMap<Crop, GetByIdCropResponse>();


        CreateMap<Crop, ListDynamicCropResponse>();

        CreateMap<Paginate<Crop>, ListModel<ListDynamicCropResponse>>();
    }
}
