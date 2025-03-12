using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Crops._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;
using Ester.FarmetTracker.Common;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Crops.Update;

public record UpdateCropCommand(Guid Id, string Name, string Description) : IServiceRequest<UpdateCropResponse>;

public record UpdateCropResponse(Guid Id, string Name, string Description);

public class UpdateCropCommandHandler(CropBusinessRules _cropBusinessRules, ICropRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateCropCommand, UpdateCropResponse>
{

    public async Task<Response<UpdateCropResponse>> Handle(UpdateCropCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id);

        _cropBusinessRules.ThrowExceptionIfDataNull(data);

        _mapper.Map(request, data);
        await _repository.UpdateAsync(data!);

        return Response<UpdateCropResponse>.Success(_mapper.Map<UpdateCropResponse>(data), HttpStatusCode.OK);
    }
}

public static class UpdateCropEndpointExtension
{
    public static RouteGroupBuilder UpdateCropEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateCropCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope))
        .AddEndpointFilter<FluentValidationFilter<UpdateCropCommand>>();
        return group;
    }
}
