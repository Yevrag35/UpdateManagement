using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MG.UpdateManagement
{
    public static class UmContext
    {
        #region FIELDS/CONSTANTS


        #endregion

        #region PROPERTIES
        public static IUpdateServer Context { get; private set; }
        public static bool IsConnected => Context != null && !string.IsNullOrEmpty(Context.Name)

        #endregion

        #region PUBLIC METHODS
        public static void SetContext(IUpdateServer ctx) => Context = ctx;

        #endregion

        #region BACKEND/PRIVATE METHODS


        #endregion
    }
}