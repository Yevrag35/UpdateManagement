using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace MG.UpdateManagement.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "Update", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [CmdletBinding(PositionalBinding = false)]
    [Alias("Delete-UMUpdate", "Delete-Update", "remup")]
    public class RemoveUpdate : Cmdlet
    {
        private protected const string stat = "Removing update {0}/{1}...";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "FromPipeline", ValueFromPipeline = true)]
        public UMUpdate Update { get; set; }

        private protected bool _force = false;
        [Parameter(Mandatory = false)]
        public SwitchParameter Force
        {
            get => _force;
            set => _force = value;
        }

        private protected List<Guid> list;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            list = new List<Guid>();
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            list.Add(Update.ObjectId);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            if (_force || (!_force && ShouldContinue("Delete Update '" + Update.ObjectId.ToString() + "' from WSUS server database?",
                Update.Title)))
            {
                for (int i = 1; i <= list.Count; i++)
                {
                    UpdateProgress(i);
                    var id = list[i - 1];
                    UMContext.AllUpdates.Delete(id);
                }
                var pr = new ProgressRecord(1, "Deleting Updates", "Complete")
                {
                    RecordType = ProgressRecordType.Completed
                };
                WriteProgress(pr);
            }
        }

        private void UpdateProgress(int on)
        {
            var progressRecord = new ProgressRecord(1, "Deleting Updates", string.Format(stat, on, list.Count));
            double num = Math.Round((((double)on / (double)list.Count)*100), 2, MidpointRounding.ToEven);
            progressRecord.PercentComplete = Convert.ToInt32(num);
            WriteProgress(progressRecord);
        }
    }

    //internal class ToDelete
    //{
    //    public readonly int Index;
    //    public readonly Guid Id;
    //    private protected ToDelete(int index, Guid objId)
    //    {
    //        Index = index;
    //        Id = objId;
    //    }
    //    public static explicit operator ToDelete(UMUpdate umup)
    //    {
    //        int index = UMContext.AllUpdates[umup.ObjectId];
    //        return new ToDelete(index, umup.ObjectId);
    //    }
    //}
}
