using AutoMapper;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmerTracker.FieldService.Features.Crops._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Crops._base.Repositories;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common;

namespace Ester.FarmerTracker.FieldService.Features.Crops.Create;

public record CreateCropCommand(string Name, string Description) : IServiceRequest<CreateCropResponse>;

public record CreateCropResponse(Guid Id, string Name, string Description);

public class CreateCropCommandHandler(CropBusinessRules _cropBusinessRules, ICropRepository _repository, IMapper _mapper) : IServiceRequestHandler<CreateCropCommand, CreateCropResponse>
{

    public async Task<Response<CreateCropResponse>> Handle(CreateCropCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Crop>(request);

        _cropBusinessRules.SetId(data);

        await _repository.AddAsync(data);

        return Response<CreateCropResponse>.Success(_mapper.Map<CreateCropResponse>(data), HttpStatusCode.OK);
    }
}

public static class CreateCropEndpointExtension
{
    public static RouteGroupBuilder CreateCropEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateCropCommand command, [FromServices] IMediator mediatr) =>
        {

            return (await mediatr.Send(command)).ToResult();
        })
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope))
        .AddEndpointFilter<FluentValidationFilter<CreateCropCommand>>();
        return group;
    }
}