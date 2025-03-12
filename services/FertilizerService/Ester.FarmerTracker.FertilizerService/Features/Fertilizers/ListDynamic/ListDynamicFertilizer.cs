using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FertilizerService.Features._base;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.BusinessRules;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities.Enums;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.ListDynamic;

public class ListDynamicFertilizerCommand : BaseDynamicRequest, IServiceRequest<ListModel<ListDynamicFertilizerResponse>>
{

}

public record ListDynamicFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime, DateTime CreatedTime, DateTime UpdatedTime);

public class ListDynamicFertilizerCommandHandler(FertilizerBusinessRules _cropBusinessRules, IFertilizerRepository _repository, IMapper _mapper) : IServiceRequestHandler<ListDynamicFertilizerCommand, ListModel<ListDynamicFertilizerResponse>>
{

    public async Task<Response<ListModel<ListDynamicFertilizerResponse>>> Handle(ListDynamicFertilizerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.ListDynamicAsync(request.DynamicQuery, size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, enableTracking: false, cancellationToken: cancellationToken);

        var returnData = _mapper.Map<ListModel<ListDynamicFertilizerResponse>>(data);

        _cropBusinessRules.FillFilters(returnData, request.DynamicQuery, request.PageRequest);

        return Response<ListModel<ListDynamicFertilizerResponse>>.Success(returnData, HttpStatusCode.OK);
    }

}

public static class ListDynamicFertilizerEndpointExtension
{
    public static RouteGroupBuilder ListDynamicFertilizerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/list", async ([FromBody] ListDynamicFertilizerCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        });
        return group;
    }
}