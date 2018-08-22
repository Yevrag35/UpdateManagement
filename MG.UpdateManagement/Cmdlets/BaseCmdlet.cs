using MG.Attributes;
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
    public abstract class BaseCmdlet : PSCmdlet
    {
        internal readonly UpdateServer ctx = UMContext.Context;
    }

    public abstract partial class BaseGetCmdlet : BaseCmdlet
    {
        public List<UMUpdate> GetProductUpdates(string[] prodsToMatch)
        {
            var allUpdates = UMContext.AllUpdates;
            var list = new List<UMUpdate>();
            for (int i = 0; i < prodsToMatch.Length; i++)
            {
                var s = prodsToMatch[i];
                var items = allUpdates.Where(x => x.Products.Contains(s));
                list.AddRange(items);
            }
            return list;
        }
    }
}
