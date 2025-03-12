
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 23.10.2023 11:28
//---------------------------------------------------------------------------------------

using AutoMapper;
using Core.Persistence.Models.Responses;
using Core.Persistence.Paging;
using IdentityServer.Application.Features.ApiResources.Commands.BulkCreate;
using IdentityServer.Application.Features.ApiResources.Commands.Create;
using IdentityServer.Application.Features.ApiResources.Commands.Update;
using IdentityServer.Application.Features.ApiResources.Commands.BulkUpdate;
using IdentityServer.Application.Features.ApiResources.Commands.DeleteById;
using IdentityServer.Application.Features.ApiResources.Queries.ListDynamic;
using IdentityServer.Application.Features.ApiResources.Queries.GetById;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Features.ApiResources.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
		CreateMap<BulkCreateApiResourceCommand,ApiResource>();
		CreateMap<ApiResource, BulkCreateApiResourceResponse>();
		CreateMap<CreateApiResourceCommand,ApiResource>();
		CreateMap<ApiResource, CreateApiResourceResponse>();
		CreateMap<UpdateApiResourceCommand,ApiResource>();
		CreateMap<ApiResource, UpdateApiResourceResponse>();
		CreateMap<BulkUpdateApiResourceCommand,ApiResource>();
		CreateMap<ApiResource, BulkUpdateApiResourceResponse>();
		CreateMap<DeleteByIdApiResourceCommand,ApiResource>();
		CreateMap<ApiResource, DeleteByIdApiResourceResponse>();
		CreateMap<Paginate<ApiResource>, ListModel<ListDynamicApiResourceResponse>>();
		CreateMap<ApiResource, ListDynamicApiResourceResponse>();
		CreateMap<ApiResource, GetByIdApiResourceResponse>();
    }
}




