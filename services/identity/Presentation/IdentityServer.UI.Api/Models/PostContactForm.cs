using Notification.Entities;

namespace IdentityServer.UI.Api.Models;

public class PostContactForm : IPostModel
{
    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public string Subject { get; set; }

    public string Message { get; set; }
}
