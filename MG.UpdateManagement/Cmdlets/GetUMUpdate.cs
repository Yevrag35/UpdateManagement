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

        public object GetDynamicParameters()
        {
            if (_lib == null)
            {
                _lib = new Library();
                _lib.AddParameter(new ArchitectureParameter(Product));
            }
            return _lib;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (!string.IsNullOrEmpty(KBArticleId) && KBArticleId.IndexOf("KB", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                KBArticleId = KBArticleId.Replace("KB", string.Empty).Replace("kb", string.Empty);
            }
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            var prm = (ArchitectureParameter)_lib["Architecture"];
            var arcs = prm.MatchChoiceToEnum((string)prm.Value);
            if (!_all)
            {
                var ups = new List<UMUpdate>();
                if (Product != null && string.IsNullOrEmpty(KBArticleId))
                {
                    var prods = new string[Product.Length];
                    for (int i = 0; i < Product.Length; i++)
                    {
                        var p = Product[i];

                        var tempList = WittleDown(p, arcs, IsSuperseded, IsApproved, IsDeclined).ToArray();
                        ups.AddRange(tempList);
                    }
                }
                else if (!string.IsNullOrEmpty(KBArticleId))
                {
                    string[] prods = null;
                    if (Product != null)
                    {
                        prods = new string[Product.Length];
                        for (int i = 0; i < Product.Length; i++)
                        {
                            var p = Product[i];
                            var tempList = WittleDown(KBArticleId, p, arcs);
                            ups.AddRange(tempList);
                        }
                    }
                    else
                    {
                        var tempList = WittleDown(KBArticleId, null, arcs);
                        ups.AddRange(tempList);
                    }
                }
                else
                {
                    var info = new UMProductInfo(null, null, IsSuperseded, IsApproved, IsApproved);
                    for (int i = 0; i < UMContext.AllUpdates.Count; i++)
                    {
                        var u = UMContext.AllUpdates[i];
                        if (u.MatchesInfo(info))
                        {
                            ups.Add(u);
                        }
                    }
                }
                WriteObject(ups.ToArray(), true);
            }
            else
            {
                WriteObject(UMContext.AllUpdates, true);
            }
        }
    }
}
