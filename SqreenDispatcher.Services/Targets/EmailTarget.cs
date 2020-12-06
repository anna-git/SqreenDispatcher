using GeekLearning.Email;
using GeekLearning.Email.Internal;
using SqreenDispatcher.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Targets
{
    public class EmailTarget : ITarget
    {
        private readonly IEmailSender _emailSender;

        public EmailTarget(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public  Task Notify(IEnumerable<SqreenMessage> messages)
        {
            return _emailSender.SendTemplatedEmailAsync("AlertEmail", new { messages = messages}, new EmailAddress("someone@gmail.com","test")); 
            //todo: get the recipient's email from a DB or from configuration settings
        }
    }
}
