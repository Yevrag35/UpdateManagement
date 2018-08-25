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
    [Cmdlet(VerbsCommon.Get, "UMUpdate", ConfirmImpact = ConfirmImpact.None,
            DefaultParameterSetName = "ByProduct")]
    [CmdletBinding(PositionalBinding = false)]
    [OutputType(typeof(UMUpdate))]
    public class GetUMUpdate : BaseGetCmdlet, IDynamicParameters
    {
        [Parameter(Mandatory = false, Position = 0)]
        public UMProducts[] Product { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "ByKBId")]
        public string KBArticleId { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "ByProduct")]
        public bool IsSuperseded = false;

        [Parameter(Mandatory = false, ParameterSetName = "ByProduct")]
        public bool IsApproved = false;

        [Parameter(Mandatory = false, ParameterSetName = "ByProduct")]
        public bool IsDeclined = false;

        private bool _all;
        [Parameter(Mandatory = true, ParameterSetName = "AllUpdates")]
        public SwitchParameter All
        {
            get => _all;
            set => _all = value;
        }

        public override object GetDynamicParameters()
        {
            if (Product != null && _lib == null)
            {
                _lib = new Library();
                _lib.AddParameter(new ArchitectureParameter(Product));
            }
            return _lib;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            KBArticleId = FormatKBString(KBArticleId);
        }



        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ArchitectureParameter prm = null;
            Architectures? arcs = null;
            if (_lib != null)
            {
                prm = (ArchitectureParameter)_lib["Architecture"];
                arcs = prm.MatchChoiceToEnum((string)prm.Value);
            }
            if (!_all)
            {
                if (Product != null && string.IsNullOrEmpty(KBArticleId))
                {
                    FilterUpdates(Product, arcs, IsSuperseded, IsApproved, IsDeclined);
                }
                else if (!string.IsNullOrEmpty(KBArticleId))
                {
                    FilterUpdates(Product, arcs, KBArticleId);
                }
                else
                {
                    FilterUpdates(IsSuperseded, IsApproved, IsDeclined);
                }
            }
            else
            {
                WriteObject(UMContext.AllUpdates, true);
            }
        }
    }
}
