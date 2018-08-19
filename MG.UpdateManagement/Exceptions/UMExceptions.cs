using System;
using System.Net;

namespace MG.UpdateManagement.Exceptions
{
    public interface IUMException
    {
        bool IsStaticMessage { get; }
        bool IsMessageCustomizable { get; }
        bool IsMessageFormattable { get; }
        bool InnerExceptionSupportable { get; }
    }

    public class UMContextNotSetException : NotImplementedException, IUMException
    {
        private protected const string _message = "You are not connected to any UMServer!  Set the context first with \"Connect-UMServer\"!";
        public bool IsStaticMessage => true;
        public bool IsMessageCustomizable => false;
        public bool IsMessageFormattable => false;
        public bool InnerExceptionSupportable => false;

        public UMContextNotSetException()
            : base(_message)
        {
        }
    }

    public class WsusInErrorException : WebException, IUMException
    {
        private protected const string _mtemp = "{0} is in an error state at port {1}.  Status Code = {2}";
        public bool IsStaticMessage => false;
        public bool IsMessageCustomizable => false;
        public bool IsMessageFormattable => true;
        public bool InnerExceptionSupportable => true;

        private static string Format(string s1, int s2, object s3) =>
            string.Format(_mtemp, s1, s2, s3.ToString());

        public WsusInErrorException(string serverName, int portNumber, Enum statusCode)
            : base(Format(serverName, portNumber, statusCode))
        {
        }
        public WsusInErrorException(string serverName, int portNumber, Exception e)
            : base(Format(serverName, portNumber, "Unknown"), e)
        {
        }
        public WsusInErrorException(string serverName, int portNumber, Enum statusCode, Exception e)
            : base(Format(serverName, portNumber, statusCode), e)
        {
        }
        public WsusInErrorException(string serverName, int portNumber, Enum statusCode, HttpWebResponse response)
            : base(Format(serverName, portNumber, statusCode), null, WebExceptionStatus.UnknownError, response)
        {
        }
        public WsusInErrorException(string serverName, int portNumber, 
            Exception e, WebExceptionStatus exStat, HttpWebResponse resp)
            : base(Format(serverName, portNumber, exStat), e, exStat, resp)
        {
        }
    }
}
