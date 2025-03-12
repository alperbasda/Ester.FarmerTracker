using AutoMapper;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.BusinessRules;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities.Enums;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.GetById;


public record GetByIdFertilizerCommand(Guid Id) : IServiceRequest<GetByIdFertilizerResponse>;

public record GetByIdFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime, DateTime CreatedTime, DateTime UpdatedTime);

public class GetByIdFertilizerCommandHandler(FertilizerBusinessRules _cropBusinessRules, IFertilizerRepository _repository, IMapper _mapper) : IServiceRequestHandler<GetByIdFertilizerCommand, GetByIdFertilizerResponse>
{

    public async Task<Response<GetByIdFertilizerResponse>> Handle(GetByIdFertilizerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

        _cropBusinessRules.ThrowExceptionIfDataNull(data);

        return Response<GetByIdFertilizerResponse>.Success(_mapper.Map<GetByIdFertilizerResponse>(data), HttpStatusCode.OK);
    }
}

public static class GetByIFertilizerEndpointExtension
{
    public static RouteGroupBuilder GetByIdFertilizerEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new GetByIdFertilizerCommand(id))).ToResult();
        });
        return group;
    }
}

