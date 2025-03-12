
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 23.10.2023 11:28
//---------------------------------------------------------------------------------------

namespace IdentityServer.Application.Features.Clients.Commands.Update;

public class UpdateClientResponse
{
    
	public byte[] ClientSecret { get; set; }
	public DateTime UpdatedTime { get; set; }
	public DateTime CreatedTime { get; set; }
	public DateTime DeletedTime { get; set; }
	public string Name { get; set; }
	public int Status { get; set; }
	public Guid Id { get; set; }
	public string ClientSecretSalt { get; set; }
	public string? ExternalId { get; set; } = null!;
    
}




