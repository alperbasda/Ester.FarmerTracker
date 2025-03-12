using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Features.Users.Commands.ValidateForgetCode;

public class ValidateForgetCodeUserResponse
{
    public string? DeviceId { get; set; }
    public string? CompanyName { get; set; }
    public string PhoneNumber { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string NormalizedMailAddress { get; set; }
    public string MailAddress { get; set; }
    public string NormalizedUserName { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime DeletedTime { get; set; }
    public string FirstName { get; set; }
    public DateTime UpdatedTime { get; set; }
    public Guid Id { get; set; }
    public int Status { get; set; }
    public bool AgreeContact { get; set; }
}
