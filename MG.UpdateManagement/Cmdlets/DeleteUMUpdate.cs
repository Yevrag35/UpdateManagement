using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace MG.UpdateManagement.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "UMUpdate", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [CmdletBinding(PositionalBinding = false)]
    [Alias("Delete-UMUpdate", "remup")]
    public class DeleteUMUpdate : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "FromPipeline", ValueFromPipeline = true)]
        public UMUpdate Update { get; set; }

        private protected bool _force = false;
        [Parameter(Mandatory = false)]
        public SwitchParameter Force
        {
            get => _force;
            set => _force = value;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (_force || (!_force && ShouldContinue("Delete Update '" + Update.ObjectId.ToString() + "' from WSUS server database?", 
                Update.Title)))
            {
                UMContext.AllUpdates.Delete(Update);
                //WriteObject(Update.Title + " deleted from database.");
            }
        }
    }
}
