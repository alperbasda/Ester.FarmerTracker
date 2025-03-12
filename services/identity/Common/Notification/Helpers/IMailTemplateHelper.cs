using System.Threading.Tasks;
using Notification.Entities;

namespace Notification.Helpers
{
    public interface IMailTemplateHelper
    {
        Task<string> GetTemplateHtmlAsStringAsync(
            string viewName, IPostModel model);
    }
}