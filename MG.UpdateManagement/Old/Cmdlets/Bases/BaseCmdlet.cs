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
        internal UpdateServer ctx = UMContext.Context;

        internal void PopulateRelevantWithUpdates()
        {
            UMClassifications classes = UMContext.Context.GetUpdateClassifications();
            UMCategories categories = UMContext.Context.GetUpdateCategories();

            var upScope = new UpdateScope()
            {
                ApprovedStates = ApprovedStates.Any,
                UpdateSources = UpdateSources.All,
                IncludedInstallationStates = UpdateInstallationStates.All,
                UpdateTypes = UpdateTypes.All,
                UpdateApprovalActions = UpdateApprovalActions.All
            };
            upScope.Classifications.AddRange(classes);
            upScope.Categories.AddRange(categories);
            UMContext.AllUpdates = UMContext.Context.GetUpdates(upScope);
        }
    }
}
