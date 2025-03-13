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

namespace Ester.FarmerTracker.FieldService.Features.Harvests.DeleteById;

public record DeleteHarvestCommand(Guid Id) : IServiceRequest<DeleteHarvestResponse>;

public record DeleteHarvestResponse(Guid Id);

public class DeleteHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<DeleteHarvestCommand, DeleteHarvestResponse>
{

    public async Task<Response<DeleteHarvestResponse>> Handle(DeleteHarvestCommand request, CancellationToken cancellationToken)
    {

        var data = await _repository.GetAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

        _harvestBusinessRules.ThrowExceptionIfDataNull(data);
        await _harvestBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.FieldId);

        await _repository.DeleteAsync(data!);

        return Response<DeleteHarvestResponse>.Success(_mapper.Map<DeleteHarvestResponse>(data), HttpStatusCode.OK);
    }
}

public static class DeleteHarvestEndpointExtension
{
    public static RouteGroupBuilder DeleteHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new DeleteHarvestCommand(id))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}
