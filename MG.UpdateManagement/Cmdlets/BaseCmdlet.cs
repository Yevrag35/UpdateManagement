using MG.Attributes;
using MG.UpdateManagement.Exceptions;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace MG.UpdateManagement.Cmdlets
{
    public abstract class BaseCmdlet : PSCmdlet
    {
        internal readonly UpdateServer ctx = UMContext.Context;
    }

    public abstract class BaseGetCmdlet : BaseCmdlet
    {
        
    }
}
