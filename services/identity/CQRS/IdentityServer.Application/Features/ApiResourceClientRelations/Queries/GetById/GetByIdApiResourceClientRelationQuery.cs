
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 23.10.2023 11:28
//---------------------------------------------------------------------------------------

using MediatR;

namespace IdentityServer.Application.Features.ApiResourceClientRelations.Queries.GetById;

public class GetByIdApiResourceClientRelationQuery : IRequest<GetByIdApiResourceClientRelationResponse>  
{
    
	public Guid Id { get; set; }
    
}




