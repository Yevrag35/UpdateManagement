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
        public UMProducts[] Product { get; set; }

        [Parameter(Mandatory = false)]
        public bool IsSuperseded = false;

        [Parameter(Mandatory = false)]
        public bool IsApproved = true;

        [Parameter(Mandatory = false)]
        public bool IsDeclined = false;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Product != null)
            {
                var ups = new List<UMUpdate>();
                var prods = new string[Product.Length];
                for (int i = 0; i < Product.Length; i++)
                {
                    var p = Product[i];
                    var tempList = WittleDown(p, IsSuperseded, IsApproved, IsDeclined).ToArray();
                    ups.AddRange(tempList);
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
