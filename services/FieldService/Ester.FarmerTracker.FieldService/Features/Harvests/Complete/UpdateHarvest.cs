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

namespace Ester.FarmerTracker.FieldService.Features.Harvests.Complete;

public record CompleteHarvestCommand(Guid FieldId) : IServiceRequest<CompleteHarvestResponse>;

public record CompleteHarvestResponse(Guid Id, DateTime HarvestTime);

public class CompleteHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<CompleteHarvestCommand, CompleteHarvestResponse>
{

    public async Task<Response<CompleteHarvestResponse>> Handle(CompleteHarvestCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.FieldId == request.FieldId && w.HarvestTime == null);

        _harvestBusinessRules.ThrowExceptionIfDataNull(data);
        await _harvestBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.FieldId);
        await _harvestBusinessRules.ClearFieldIfHarvestDateNotNull(data);

        data.HarvestTime = DateTime.Now;
        await _repository.UpdateAsync(data!);

        return Response<CompleteHarvestResponse>.Success(new CompleteHarvestResponse(data.Id, data.HarvestTime.Value), HttpStatusCode.OK);
    }
}

public static class CompleteHarvestEndpointExtension
{
    public static RouteGroupBuilder CompleteHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/complete/{fieldId:Guid}", async (Guid fieldId, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new CompleteHarvestCommand(fieldId))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}
