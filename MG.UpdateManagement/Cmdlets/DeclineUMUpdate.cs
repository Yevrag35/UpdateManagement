using Dynamic;
using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Exceptions;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.UpdateManagement.Cmdlets
{
    [Cmdlet("Decline", "UMUpdate", ConfirmImpact = ConfirmImpact.None)]
    [CmdletBinding(PositionalBinding = false)]
    [Alias("deup")]
    public class DeclineUMUpdate : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "FromPipeline", ValueFromPipeline = true)]
        public UMUpdate Update { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            Update.Decline();
        }
    }
}
