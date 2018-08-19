using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MG.UpdateManagement.Framework
{
    public static class UMContext
    {
        public static UpdateServer Context { get; set; }

        public static UMUpdateCollection AllUpdates { get; set; }

        #region Static Methods
        public static bool IsSet => Context != null;

        public static string ServerName => IsSet ? Context.Name : null;

        public static bool? IsSecureConnection =>
            IsSet ? Context.UseSecureConnection && Context.IsConnectionSecureForApiRemoting : (bool?)null;

        public static bool? Bypassing => 
            IsSet ? Context.BypassApiRemoting : (bool?)null;

        public static IDictionary Properties =>
            IsSet ? new Dictionary<string, object>()
            {
                { "PortNumber", Context.PortNumber },
                { "ServerProtocolVersion", Context.ServerProtocolVersion },
                { "Version", Context.Version },
                { "Url", Context.WebServiceUrl }
            } : null;

        #endregion
    }
}
