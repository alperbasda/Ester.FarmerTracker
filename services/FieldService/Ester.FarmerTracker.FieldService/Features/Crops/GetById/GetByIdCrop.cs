using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Crops._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Crops.GetById;


public record GetByIdCropCommand(Guid Id) : IServiceRequest<GetByIdCropResponse>;

public record GetByIdCropResponse(Guid Id, string Name, string Description, DateTime CreatedTime, DateTime UpdatedTime);

public class GetByIdCropCommandHandler(CropBusinessRules _cropBusinessRules, ICropRepository _repository, IMapper _mapper) : IServiceRequestHandler<GetByIdCropCommand, GetByIdCropResponse>
{

    public async Task<Response<GetByIdCropResponse>> Handle(GetByIdCropCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

        _cropBusinessRules.ThrowExceptionIfDataNull(data);

        return Response<GetByIdCropResponse>.Success(_mapper.Map<GetByIdCropResponse>(data), HttpStatusCode.OK);
    }
}

public static class GetByICropEndpointExtension
{
    public static RouteGroupBuilder GetByIdCropEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new GetByIdCropCommand(id))).ToResult();
        });
        return group;
    }
}

