



//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 23.10.2023 11:28
//---------------------------------------------------------------------------------------

using MediatR;

namespace IdentityServer.Application.Features.UserScopes.Queries.GetById;

public class GetByIdUserScopeQuery : IRequest<GetByIdUserScopeResponse>  
{
    
	public Guid Id { get; set; }
    
}




