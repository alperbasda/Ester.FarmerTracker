using AutoMapper;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.BusinessRules;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Entities.Enums;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.Update;

public record UpdateFertilizerCommand(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime) : IServiceRequest<UpdateFertilizerResponse>;

public record UpdateFertilizerResponse(Guid Id, Guid UserId, string UserFullName, string SerialNumber, decimal TotalAmount, decimal RemainingAmount, FertilizerStatus Status, DateTime ExpirationTime);

public class UpdateFertilizerCommandHandler(FertilizerBusinessRules _cropBusinessRules, IFertilizerRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateFertilizerCommand, UpdateFertilizerResponse>
{

    public async Task<Response<UpdateFertilizerResponse>> Handle(UpdateFertilizerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id);

        _cropBusinessRules.ThrowExceptionIfDataNull(data);

        _mapper.Map(request, data);
        await _repository.UpdateAsync(data!);

        return Response<UpdateFertilizerResponse>.Success(_mapper.Map<UpdateFertilizerResponse>(data), HttpStatusCode.OK);
    }
}

public static class UpdateFertilizerEndpointExtension
{
    public static RouteGroupBuilder UpdateFertilizerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateFertilizerCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        //.AddEndpointFilter(new AuthorizationFilter(ApplicationScopes.AdminScope))
        .AddEndpointFilter<FluentValidationFilter<UpdateFertilizerCommand>>();
        return group;
    }
}
