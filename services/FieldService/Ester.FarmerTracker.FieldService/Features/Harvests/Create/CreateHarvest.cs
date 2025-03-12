using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Harvests._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Harvests.Create;

public record CreateHarvestCommand(Guid FieldId, Guid? CropId, DateTime? HarvestTime) : IServiceRequest<CreateHarvestResponse>;

public record CreateHarvestResponse(Guid Id, Guid FieldId, Guid? CropId, DateTime? HarvestTime);

public class CreateHarvestCommandHandler(HarvestBusinessRules _harvestBusinessRules, IHarvestRepository _repository, IMapper _mapper) : IServiceRequestHandler<CreateHarvestCommand, CreateHarvestResponse>
{
    public async Task<Response<CreateHarvestResponse>> Handle(CreateHarvestCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Harvest>(request);

        await _harvestBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(request.FieldId);
        await _repository.AddAsync(data);


        return Response<CreateHarvestResponse>.Success(_mapper.Map<CreateHarvestResponse>(data), HttpStatusCode.OK);
    }
}

public static class CreateHarvestEndpointExtension
{
    public static RouteGroupBuilder CreateHarvestEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateHarvestCommand command, [FromServices] IMediator mediatr) =>
        {

            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<CreateHarvestCommand>>();
        return group;
    }
}