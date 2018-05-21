using System;

namespace Agent.Files
{
    public class FtpException : Exception
    {
        public FtpException(Exception e) : base(e.Message, e)
        {

        }
    }
}
