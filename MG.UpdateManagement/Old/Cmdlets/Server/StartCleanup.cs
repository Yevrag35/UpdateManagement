using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Management.Automation;

namespace MG.UpdateManagement.Cmdlets
{
    [Cmdlet(VerbsLifecycle.Start, "Cleanup")]
    public class StartCleanup : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            WriteObject(StartCleanup(true, true, true, true, true, false));
        }

        private UMCleanupResults StartCleanup(bool incSuperseded, bool incExpired, bool compress, bool incObComps, bool incUnneededFiles, bool localPublishedFiles)
        {
            var scope = new CleanupScope(
                incSuperseded, incExpired, compress, incObComps, incUnneededFiles, localPublishedFiles);
            var cleanup = new CleanupManager(UMContext.Context);
            UMCleanupResults results = cleanup.PerformCleanup(scope);
            return results;
        }
    }
}
