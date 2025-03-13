using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Fields._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Fields.GetById;


public record GetByIdFieldCommand(Guid Id) : IServiceRequest<GetByIdFieldResponse>;

public record GetByIdFieldResponse(Guid Id, Guid CustomerId, string Name, string Coordinate, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, decimal SquareMeter, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);

public class GetByIdFieldCommandHandler(FieldBusinessRules _fieldBusinessRules, IFieldRepository _repository, IMapper _mapper) : IServiceRequestHandler<GetByIdFieldCommand, GetByIdFieldResponse>
{

    public async Task<Response<GetByIdFieldResponse>> Handle(GetByIdFieldCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

        _fieldBusinessRules.ThrowExceptionIfDataNull(data);
        await _fieldBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.CustomerId);

        return Response<GetByIdFieldResponse>.Success(_mapper.Map<GetByIdFieldResponse>(data), HttpStatusCode.OK);
    }
}

public static class GetByIFieldEndpointExtension
{
    public static RouteGroupBuilder GetByIdFieldEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new GetByIdFieldCommand(id))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}

