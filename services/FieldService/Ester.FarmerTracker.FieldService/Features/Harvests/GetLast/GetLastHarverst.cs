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

namespace Ester.FarmerTracker.FieldService.Features.Harvests.GetLast;


public record GetLastHarvestCommand(Guid FieldId) : IServiceRequest<GetLastHarvestResponse>;

public record GetLastHarvestResponse(Guid Id, Guid FieldId, Guid? CropId, DateTime? HarvestTime);

public class GetLastHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<GetLastHarvestCommand, GetLastHarvestResponse>
{

    public async Task<Response<GetLastHarvestResponse>> Handle(GetLastHarvestCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetLastForFieldAsyc(request.FieldId);

        if (data != null)
        {
            await _harvestBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.FieldId);
        }
        if (data == null)
            return Response<GetLastHarvestResponse>.Success(new GetLastHarvestResponse(Guid.Empty, Guid.Empty, Guid.Empty, null), HttpStatusCode.OK);

        return Response<GetLastHarvestResponse>.Success(_mapper.Map<GetLastHarvestResponse>(data), HttpStatusCode.OK);
    }
}

public static class GetByIHarvestEndpointExtension
{
    public static RouteGroupBuilder GetLastHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/getlast/{fieldId:Guid}", async ([FromRoute] Guid fieldId, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new GetLastHarvestCommand(fieldId))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}

