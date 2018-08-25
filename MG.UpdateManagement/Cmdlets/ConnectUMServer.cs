using MG.UpdateManagement.Exceptions;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Management.Automation;
using System.Net;

namespace MG.UpdateManagement.Cmdlets
{
    [Cmdlet(VerbsCommunications.Connect, "UMServer", SupportsShouldProcess = true)]
    [CmdletBinding(PositionalBinding = false)]
    [Alias("conums")]
    public class ConnectUMServer : PSCmdlet
    {
        #region ### My Constants ###
        //private protected const string _myserv = "updates.yevrag35.com";
        //private protected const int _myport = 8531;

        #endregion

        #region ### Parameters ###

        #region -WsusServerName
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Name", "Server", "s", "Wsus")]
        public string WsusServerName { get; set; }

        #endregion

        #region -PortNumber
        [Parameter(Mandatory = true, Position = 1)]
        [Alias("p", "Port")]
        public int PortNumber { get; set; }

        #endregion

        #region -PreGatherUpdates
        /*  This switch will tell the Context to gather 'ALL'
         *  updates before returning.  The more updates a WSUS
         *  server syncs, the longer this usually takes.  But
         *  once all updates are gathered, all other cmdlets
         *  will performs much faster. */
        private bool _pre;
        [Parameter(Mandatory = false)]
        [Alias("pre")]
        public SwitchParameter PreGatherUpdates
        {
            get => _pre;
            set => _pre = value;
        }

        #endregion

        #region -UseSSL
        /*  '-UseSSL' will automatically applied when the port
         *  number chosen is either '443' or '8531'. */
        private bool _ssl;
        [Parameter(Mandatory = false)]
        [Alias("ssl")]
        public SwitchParameter UseSSL
        {
            get => _ssl;
            set => _ssl = value;
        }

        #endregion

        #region -Force
        /*  This switch is only needed when an existing
         *  UpdateManagement connection is already set.
         * '-Force' will override the already existing connection. */
        private bool _force;
        [Parameter(Mandatory = false, DontShow = true)]
        public SwitchParameter Force
        {
            get => _force;
            set => _force = value;
        }

        #endregion

        #endregion

        #region ### Process ###
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (PortNumber == 8531 || PortNumber == 443)
            {
                _ssl = true;
            }
            Uri url = CreateWsusUrl(_ssl);
            if (VerifyConnectivity(url))
            {
                IUpdateServer umSrv = null;
                try
                {
                    umSrv = AdminProxy.GetUpdateServer(
                        WsusServerName, _ssl, PortNumber
                    );
                }
                catch (Exception e)
                {
                    throw new WebException(e.Message, e);
                }
                UMContext.Context = (UpdateServer)umSrv;
            }
            if (_pre)
            {
                UMContext.AllUpdates = UMContext.Context.GetUpdates();
            }
        }

        #endregion

        #region ### Backend Methods ###
        private protected Uri CreateWsusUrl(bool ssl) =>
            ssl ? new Uri("https://" + WsusServerName + ":" + PortNumber) :
                   new Uri("http://" + WsusServerName + ":" + PortNumber);

        private protected bool VerifyConnectivity(Uri wsusUrl)
        {
            var req = WebRequest.Create(wsusUrl);
            HttpWebResponse resp = null;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                throw new WsusInErrorException(WsusServerName, PortNumber, e.Status, e);
            }
            catch (Exception e)
            {
                throw new WsusInErrorException(WsusServerName, PortNumber, e);
            }
            if (resp != null)
            {
                if (resp.StatusCode != HttpStatusCode.OK && resp.StatusCode != HttpStatusCode.Forbidden)
                {
                    throw new WsusInErrorException(WsusServerName, PortNumber, resp.StatusCode, resp);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new WsusInErrorException(WsusServerName, PortNumber, HttpStatusCode.NotFound);
            }
        }

        #endregion
    }
}
