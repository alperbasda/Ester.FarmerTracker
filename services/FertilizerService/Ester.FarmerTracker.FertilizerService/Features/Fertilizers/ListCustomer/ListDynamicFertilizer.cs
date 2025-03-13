using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FertilizerService.Features.Fertilizers.ListCustomer;

public record ListCustomerFertilizerCommand(Guid CustomerId) : IServiceRequest<List<ListCustomerFertilizerResponse>>;

public record ListCustomerFertilizerResponse(Guid Id, string Text);

public class ListCustomerFertilizerCommandHandler(IFertilizerRepository _repository) : IServiceRequestHandler<ListCustomerFertilizerCommand, List<ListCustomerFertilizerResponse>>
{
    public async Task<Response<List<ListCustomerFertilizerResponse>>> Handle(ListCustomerFertilizerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.ListAsync(w => w.UserId == request.CustomerId && w.RemainingAmount > 0, size: int.MaxValue, index: 0, enableTracking: false, cancellationToken: cancellationToken);

        var returnData = data.Items.Select(w => new ListCustomerFertilizerResponse(w.Id, $"{w.SerialNumber} ({w.TotalAmount}/{w.RemainingAmount})")).ToList();

        return Response<List<ListCustomerFertilizerResponse>>.Success(returnData, HttpStatusCode.OK);
    }
}

public static class ListCustomerFertilizerEndpointExtension
{
    public static RouteGroupBuilder ListCustomerFertilizerEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/customerfertilizerlist/{customerId:Guid}", async (Guid customerId, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new ListCustomerFertilizerCommand(customerId))).ToResult();
        });
        return group;
    }
}