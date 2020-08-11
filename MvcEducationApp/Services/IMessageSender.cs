using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp.Services
{
    public interface IMessageSender
    {
        string Send();
    }

    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Message is sent by email";
        }
    }

    public class SmsMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Message is sent by sms";
        }
    }
}
