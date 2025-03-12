using AutoMapper;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmerTracker.FieldService.Features.Crops._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common;

namespace Ester.FarmerTracker.FieldService.Features.Crops.DeleteById;

public record DeleteCropCommand(Guid Id) : IServiceRequest<DeleteCropResponse>;

public record DeleteCropResponse(Guid Id);

public class DeleteCropCommandHandler(CropBusinessRules _cropBusinessRules, ICropRepository _repository, IMapper _mapper) : IServiceRequestHandler<DeleteCropCommand, DeleteCropResponse>
{

    public async Task<Response<DeleteCropResponse>> Handle(DeleteCropCommand request, CancellationToken cancellationToken)
    {

        var data = await _repository.GetAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

        _cropBusinessRules.ThrowExceptionIfDataNull(data);

        await _repository.DeleteAsync(data!);

        return Response<DeleteCropResponse>.Success(_mapper.Map<DeleteCropResponse>(data), HttpStatusCode.OK);
    }
}

public static class DeleteCropEndpointExtension
{
    public static RouteGroupBuilder DeleteCropEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new DeleteCropCommand(id))).ToResult();
        });
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope));
        return group;
    }
}
