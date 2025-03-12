using AutoMapper;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.BusinessRules;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.DeleteById;

public record DeleteFertilizerCommand(Guid Id) : IServiceRequest<DeleteFertilizerResponse>;

public record DeleteFertilizerResponse(Guid Id);

public class DeleteFertilizerCommandHandler(FertilizerBusinessRules _cropBusinessRules, IFertilizerRepository _repository, IMapper _mapper) : IServiceRequestHandler<DeleteFertilizerCommand, DeleteFertilizerResponse>
{

    public async Task<Response<DeleteFertilizerResponse>> Handle(DeleteFertilizerCommand request, CancellationToken cancellationToken)
    {

        var data = await _repository.GetAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

        _cropBusinessRules.ThrowExceptionIfDataNull(data);

        await _repository.DeleteAsync(data!);

        return Response<DeleteFertilizerResponse>.Success(_mapper.Map<DeleteFertilizerResponse>(data), HttpStatusCode.OK);
    }
}

public static class DeleteFertilizerEndpointExtension
{
    public static RouteGroupBuilder DeleteFertilizerEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new DeleteFertilizerCommand(id))).ToResult();
        });
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope));
        return group;
    }
}
