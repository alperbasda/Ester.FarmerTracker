using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Fields._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Fields.ListDynamic;

public class ListDynamicFieldCommand : BaseDynamicRequest, IServiceRequest<ListModel<ListDynamicFieldResponse>>
{

}

public record ListDynamicFieldResponse(Guid Id, Guid CustomerId, decimal CurrentTotalFertilizerAmount, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);

public class ListDynamicFieldCommandHandler(FieldBusinessRules _fieldBusinessRules, IFieldRepository _repository, IMapper _mapper) : IServiceRequestHandler<ListDynamicFieldCommand, ListModel<ListDynamicFieldResponse>>
{

    public async Task<Response<ListModel<ListDynamicFieldResponse>>> Handle(ListDynamicFieldCommand request, CancellationToken cancellationToken)
    {
       var customers = await _fieldBusinessRules.GetRepresentiveCustomers();
        customers.Add(_fieldBusinessRules.GetUserId());
        var data = await _repository.ListDynamicAsync(request.DynamicQuery,
                                                      predicate: w => _fieldBusinessRules.IsUserAdmin() ||
                                                      customers.Contains(w.CustomerId),
                                                      size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, enableTracking: false, cancellationToken: cancellationToken);

        var returnData = _mapper.Map<ListModel<ListDynamicFieldResponse>>(data);

        _fieldBusinessRules.FillFilters(returnData, request.DynamicQuery, request.PageRequest);

        return Response<ListModel<ListDynamicFieldResponse>>.Success(returnData, HttpStatusCode.OK);
    }

}

public static class ListDynamicFieldEndpointExtension
{
    public static RouteGroupBuilder ListDynamicFieldEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/list", async ([FromBody] ListDynamicFieldCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}