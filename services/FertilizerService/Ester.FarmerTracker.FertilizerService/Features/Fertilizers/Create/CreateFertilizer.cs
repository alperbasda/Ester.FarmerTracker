using AutoMapper;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.BusinessRules;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities.Enums;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Create;

public record CreateFertilizerCommand(Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime) : IServiceRequest<CreateFertilizerResponse>;

public record CreateFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime);

public class CreateFertilizerCommandHandler(FertilizerBusinessRules _cropBusinessRules, IFertilizerRepository _repository, IMapper _mapper) : IServiceRequestHandler<CreateFertilizerCommand, CreateFertilizerResponse>
{

    public async Task<Response<CreateFertilizerResponse>> Handle(CreateFertilizerCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Fertilizer>(request);

        _cropBusinessRules.SetId(data);

        await _repository.AddAsync(data);

        return Response<CreateFertilizerResponse>.Success(_mapper.Map<CreateFertilizerResponse>(data), HttpStatusCode.OK);
    }
}

public static class CreateFertilizerEndpointExtension
{
    public static RouteGroupBuilder CreateFertilizerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateFertilizerCommand command, [FromServices] IMediator mediatr) =>
        {

            return (await mediatr.Send(command)).ToResult();
        })
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope))
        .AddEndpointFilter<FluentValidationFilter<CreateFertilizerCommand>>();
        return group;
    }
}