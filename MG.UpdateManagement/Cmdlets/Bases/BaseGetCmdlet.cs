using Dynamic;
using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.UpdateManagement.Cmdlets
{
    public abstract partial class BaseGetCmdlet : BaseCmdlet, IDynamicParameters
    {
        internal Library _lib;

        public abstract object GetDynamicParameters();

        internal string FormatKBString(string kb)
        {
            if (!string.IsNullOrEmpty(kb) && kb.IndexOf("KB", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                kb = kb.Replace("KB", string.Empty).Replace("kb", string.Empty);
            }
            return kb;
        }

        internal List<UMUpdate> FilterUpdates(UMProducts[] Product, Architectures? arcs, bool IsSuperseded, bool IsApproved, bool IsDeclined, bool displayResults = true)
        {
            var ups = new List<UMUpdate>();
            var prods = new string[Product.Length];
            for (int i = 0; i < Product.Length; i++)
            {
                var p = Product[i];

                var tempList = WittleDown(p, arcs, IsSuperseded, IsApproved, IsDeclined).ToArray();
                ups.AddRange(tempList);
            }
            if (displayResults)
            {
                WriteObject(ups.ToArray(), true);
                return null;
            }
            else
            {
                return ups;
            }
        }
        internal List<UMUpdate> FilterUpdates(UMProducts[] Product, Architectures? arcs, string KBArticleId, bool displayResults = true)
        {
            var ups = new List<UMUpdate>();
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
            if (displayResults)
            {
                WriteObject(ups.ToArray(), true);
                return null;
            }
            else
            {
                return ups;
            }
        }
        internal List<UMUpdate> FilterUpdates(bool IsSuperseded, bool IsApproved, bool IsDeclined, bool displayResults = true)
        {
            var ups = new List<UMUpdate>();
            var info = new UMProductInfo(null, null, IsSuperseded, IsApproved, IsApproved);
            for (int i = 0; i < UMContext.AllUpdates.Count; i++)
            {
                var u = UMContext.AllUpdates[i];
                if (u.MatchesInfo(info))
                {
                    ups.Add(u);
                }
            }
            if (displayResults)
            {
                WriteObject(ups.ToArray(), true);
                return null;
            }
            else
            {
                return ups;
            }
        }
    }
}
