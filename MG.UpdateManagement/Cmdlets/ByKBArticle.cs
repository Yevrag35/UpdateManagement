using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Cmdlets
{
    public abstract partial class BaseGetCmdlet : BaseCmdlet
    {
        public List<UMUpdate> WittleDown(string kbText, UMProducts? prod, Architectures? arc)
        {
            var list = new List<UMUpdate>();
            var query = new UMKbQuery(kbText, prod, arc);
            for (int i = 0; i < UMContext.AllUpdates.Count; i++)
            {
                var up = UMContext.AllUpdates[i];
                if (up.MatchesInfo(query))
                {
                    list.Add(up);
                }
            }
            return list;
        }
    }
}
