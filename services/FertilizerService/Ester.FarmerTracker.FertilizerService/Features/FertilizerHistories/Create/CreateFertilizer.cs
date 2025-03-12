using AutoMapper;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.BusinessRules;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Repositories;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities.Enums;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories.Create;

//TODO Alper Her Detaya göre farklı aksiyon al.
public record CreateFertilizerHistoryCommand(Guid FertilizerId, string Description, FertilizerHistoryAction Action, ActionRequest ActionRequest) : IServiceRequest<CreateFertilizerHistoryResponse>;

public record ActionRequest(TransferActionRequest? Transfer, LossActionRequest? Loss, ThrowActionRequest? Throw);

public record TransferActionRequest(Guid RecipientId, string RecipientName, Guid GiverId, string GiverName);

public record LossActionRequest(decimal Amount);

public record ThrowActionRequest(Guid FieldId, decimal Amount);

public record CreateFertilizerHistoryResponse(Guid FertilizerId, string Description, FertilizerHistoryAction Action);

public class CreateFertilizerHistoryCommandHandler(FertilizerHistoryBusinessRules _fetilizerHistoryBusinessRules, IFertilizerHistoryRepository _repository, IMapper _mapper) : IServiceRequestHandler<CreateFertilizerHistoryCommand, CreateFertilizerHistoryResponse>
{

    public async Task<Response<CreateFertilizerHistoryResponse>> Handle(CreateFertilizerHistoryCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<FertilizerHistory>(request);

        _fetilizerHistoryBusinessRules.SetId(data);

        await _repository.AddAsync(data);

        return Response<CreateFertilizerHistoryResponse>.Success(_mapper.Map<CreateFertilizerHistoryResponse>(data), HttpStatusCode.OK);
    }
}

public static class CreateFertilizerHistoryEndpointExtension
{
    public static RouteGroupBuilder CreateFertilizerHistoryEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateFertilizerHistoryCommand command, [FromServices] IMediator mediatr) =>
        {

            return (await mediatr.Send(command)).ToResult();
        })
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope))
        .AddEndpointFilter<FluentValidationFilter<CreateFertilizerHistoryCommand>>();
        return group;
    }
}