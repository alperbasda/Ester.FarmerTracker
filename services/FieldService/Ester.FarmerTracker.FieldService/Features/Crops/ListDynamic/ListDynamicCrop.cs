using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Crops._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Crops.ListDynamic;

public class ListDynamicCropCommand : BaseDynamicRequest, IServiceRequest<ListModel<ListDynamicCropResponse>>
{

}

public record ListDynamicCropResponse(Guid Id, string Name, string Description, DateTime CreatedTime, DateTime UpdatedTime);

public class ListDynamicCropCommandHandler(CropBusinessRules _cropBusinessRules, ICropRepository _repository, IMapper _mapper) : IServiceRequestHandler<ListDynamicCropCommand, ListModel<ListDynamicCropResponse>>
{

    public async Task<Response<ListModel<ListDynamicCropResponse>>> Handle(ListDynamicCropCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.ListDynamicAsync(request.DynamicQuery, size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, enableTracking: false, cancellationToken: cancellationToken);

        var returnData = _mapper.Map<ListModel<ListDynamicCropResponse>>(data);

        _cropBusinessRules.FillFilters(returnData, request.DynamicQuery, request.PageRequest);

        return Response<ListModel<ListDynamicCropResponse>>.Success(returnData, HttpStatusCode.OK);
    }

}

public static class ListDynamicCropEndpointExtension
{
    public static RouteGroupBuilder ListDynamicCropEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/list", async ([FromBody] ListDynamicCropCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        });
        return group;
    }
}