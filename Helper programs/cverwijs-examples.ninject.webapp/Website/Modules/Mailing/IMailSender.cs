using System;

namespace Website
{
    public interface IMailSender
    {
        void Send(string toAddress, string subject);
    }
}
