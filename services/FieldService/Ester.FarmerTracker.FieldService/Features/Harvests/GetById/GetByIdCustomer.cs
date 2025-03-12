using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Harvests.GetById;


public record GetByIdHarvestCommand(Guid Id) : IServiceRequest<GetByIdHarvestResponse>;

public record GetByIdHarvestResponse(Guid Id, Guid HarvestId, Guid? CropId, DateTime? HarvestTime);

public class GetByIdHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<GetByIdHarvestCommand, GetByIdHarvestResponse>
{

    public async Task<Response<GetByIdHarvestResponse>> Handle(GetByIdHarvestCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

        _harvestBusinessRules.ThrowExceptionIfDataNull(data);
        await _harvestBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.FieldId);

        return Response<GetByIdHarvestResponse>.Success(_mapper.Map<GetByIdHarvestResponse>(data), HttpStatusCode.OK);
    }
}

public static class GetByIHarvestEndpointExtension
{
    public static RouteGroupBuilder GetByIdHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new GetByIdHarvestCommand(id))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}

