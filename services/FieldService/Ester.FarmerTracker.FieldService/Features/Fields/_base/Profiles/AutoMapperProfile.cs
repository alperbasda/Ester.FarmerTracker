using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Fields.Create;
using Ester.FarmerTracker.FieldService.Features.Fields.DeleteById;
using Ester.FarmerTracker.FieldService.Features.Fields.GetById;
using Ester.FarmerTracker.FieldService.Features.Fields.ListDynamic;
using Ester.FarmerTracker.FieldService.Features.Fields.Update;

namespace Ester.FarmerTracker.FieldService.Features.Fields._base.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Field, CreateFieldResponse>();
        CreateMap<CreateFieldCommand, Field>()
            .ForMember(w => w.CurrentTotalFertilizerAmount, q => q.MapFrom(c => 0))
            .ForMember(w => w.CurrentCropName, q => q.Ignore());

        CreateMap<Field, UpdateFieldResponse>();
        CreateMap<UpdateFieldCommand, Field>();

        CreateMap<Field, DeleteFieldResponse>();
        CreateMap<DeleteFieldCommand, Field>();

        CreateMap<Field, GetByIdFieldResponse>();


        CreateMap<Field, ListDynamicFieldResponse>();

        CreateMap<Paginate<Field>, ListModel<ListDynamicFieldResponse>>();
    }
}
