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

namespace Ester.FarmerTracker.FieldService.Features.Harvests.Update;

public record UpdateHarvestCommand(Guid Id, Guid FieldId, Guid? CropId, DateTime? HarvestTime) : IServiceRequest<UpdateHarvestResponse>;

public record UpdateHarvestResponse(Guid Id, Guid FieldId, Guid? CropId, DateTime? HarvestTime);

public class UpdateHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateHarvestCommand, UpdateHarvestResponse>
{

    public async Task<Response<UpdateHarvestResponse>> Handle(UpdateHarvestCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id);

        _harvestBusinessRules.ThrowExceptionIfDataNull(data);
        await _harvestBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.FieldId);

        _mapper.Map(request, data);
        await _repository.UpdateAsync(data!);

        return Response<UpdateHarvestResponse>.Success(_mapper.Map<UpdateHarvestResponse>(data), HttpStatusCode.OK);
    }
}

public static class UpdateHarvestEndpointExtension
{
    public static RouteGroupBuilder UpdateHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateHarvestCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<UpdateHarvestCommand>>();
        return group;
    }
}
