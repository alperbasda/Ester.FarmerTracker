using System.Collections.Generic;

namespace Notification.Entities.Concrete
{
    public class SendMailBaseModel
    {
        public HashSet<string> To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public bool IsContactMail { get; set; }

        public string TemplateName { get; set; }

        public IPostModel MailModel { get; set; }

        public bool Success { get; set; }

        public string Receiver { get; set; }
    }
}