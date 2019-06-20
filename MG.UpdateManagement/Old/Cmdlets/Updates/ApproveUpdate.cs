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
    [Cmdlet(VerbsLifecycle.Approve, "Update", DefaultParameterSetName = "FromPipeline")]
    [CmdletBinding(PositionalBinding = false)]
    public class ApproveUpdate : BaseGetCmdlet, IDynamicParameters
    {
        [Parameter(Mandatory = true, ParameterSetName = "FromPipeline",
            ValueFromPipeline = true, DontShow = true)]
        public UMUpdate Update { get; set; }

        private IComputerTargetGroup group;

        public override object GetDynamicParameters()
        {
            if (_lib == null)
            {
                _lib = new Library();
            }
            if (!_lib.ContainsKey("ComputerGroup"))
            {
                _lib.AddParameter(new TargetGroupParameter());
            }
            return _lib;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            group = TargetGroupParameter.GroupFromTheName(_lib["ComputerGroup"].Value as string);
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            Update.Approve(UpdateApprovalAction.Install, group);
        }
    }
}
