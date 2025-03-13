using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;
using Ester.FarmetTracker.Common;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Harvests.ListDynamic;

public class ListDynamicHarvestCommand : BaseDynamicRequest, IServiceRequest<ListModel<ListDynamicHarvestResponse>>
{

}

public record ListDynamicHarvestResponse(Guid Id, Guid HarvestId, Guid? CropId, DateTime? HarvestTime);

public class ListDynamicHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<ListDynamicHarvestCommand, ListModel<ListDynamicHarvestResponse>>
{

    public async Task<Response<ListModel<ListDynamicHarvestResponse>>> Handle(ListDynamicHarvestCommand request, CancellationToken cancellationToken)
    {
        var customers = await _harvestBusinessRules.GetRepresentiveCustomers();
        customers.Add(_harvestBusinessRules.GetUserId());
        var data = await _repository.ListDynamicAsync(request.DynamicQuery,
                                                      predicate: w => _harvestBusinessRules.IsUserAdmin() ||
                                                      customers.Contains(w.Field.CustomerId),
                                                      size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, enableTracking: false, cancellationToken: cancellationToken);

        var returnData = _mapper.Map<ListModel<ListDynamicHarvestResponse>>(data);

        _harvestBusinessRules.FillFilters(returnData, request.DynamicQuery, request.PageRequest);

        return Response<ListModel<ListDynamicHarvestResponse>>.Success(returnData, HttpStatusCode.OK);
    }

}

public static class ListDynamicHarvestEndpointExtension
{
    public static RouteGroupBuilder ListDynamicHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/list", async ([FromBody] ListDynamicHarvestCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}