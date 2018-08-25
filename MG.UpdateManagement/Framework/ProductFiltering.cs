using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Cmdlets
{
    public abstract partial class BaseGetCmdlet : BaseCmdlet
    {
        public List<UMUpdate> WittleDown(UMProducts prod, Architectures? arcs, bool super, bool appr, bool decline)
        {
            var list = new List<UMUpdate>();
            var info = new UMProductInfo(prod, arcs, super, appr, decline);
            for (int i = 0; i < UMContext.AllUpdates.Count; i++)
            {
                var up = UMContext.AllUpdates[i];
                if (up.MatchesInfo(info))
                {
                    list.Add(up);
                }
            }
            return list;
        }
    }
}
