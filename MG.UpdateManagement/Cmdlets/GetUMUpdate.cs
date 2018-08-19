using MG.UpdateManagement.Enumerations;
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
    [Cmdlet(VerbsCommon.Get, "UMUpdate", ConfirmImpact = ConfirmImpact.None)]
    [CmdletBinding(PositionalBinding = false)]
    public class GetUMUpdate : BaseGetCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public UMProducts? Product { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (Product.HasValue)
            {
                
            }
            else
            {
                WriteObject(UMContext.AllUpdates, true);
            }
        }
    }
}
