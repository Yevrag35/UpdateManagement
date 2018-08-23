using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using MG.UpdateManagement.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using MG.UpdateManagement.Cmdlets;

namespace MG.UpdateManagement.Objects
{
    public sealed class UMUpdate : IUpdate, IUMObject, IEquatable<UMUpdate>
    {
        private readonly IUpdate _up;

        public static implicit operator UMUpdate(Update u)
        {
            IUpdate iu = u;
            return new UMUpdate(iu);
        }

        #region Inherited Properties

        public UMUpdate(IUpdate update) => _up = update;
        public UMUpdate(Update update) => _up = update;

        public UpdateRevisionId Id => _up.Id;

        public Guid ObjectId => _up.Id.UpdateId;

        public string ObjectName => Title;

        public bool Equals(UMUpdate up)
        {
            var ieq = new UMEquality();
            return ieq.Equals(this, up) ? true : false;
        }

        public string Title => _up.Title;

        public string Description => _up.Description;

        public string LegacyName => _up.LegacyName;

        public MsrcSeverity MsrcSeverity => _up.MsrcSeverity;

        public dynamic KBArticles
        {
            get
            {
                dynamic strs = null;
                if (_up.KnowledgebaseArticles != null && _up.KnowledgebaseArticles.Count == 1)
                {
                    strs = _up.KnowledgebaseArticles[0];
                }
                else if (_up.KnowledgebaseArticles != null && _up.KnowledgebaseArticles.Count > 1)
                {
                    strs = new string[_up.KnowledgebaseArticles.Count];
                    for (int i = 0; i < _up.KnowledgebaseArticles.Count; i++)
                    {
                        strs[i] = _up.KnowledgebaseArticles[i];
                    }
                }
                return strs;
            }
        }

        public string[] Products => _up.ProductTitles.Cast<string>().ToArray();

        public StringCollection KnowledgebaseArticles => null;

        public StringCollection SecurityBulletins => _up.SecurityBulletins;

        public StringCollection AdditionalInformationUrls => _up.AdditionalInformationUrls;

        public string UpdateClassificationTitle => _up.UpdateClassificationTitle;

        public StringCollection CompanyTitles => _up.CompanyTitles;

        public StringCollection ProductTitles => null;

        public StringCollection ProductFamilyTitles => _up.ProductFamilyTitles;

        public DateTime CreationDate => _up.CreationDate;

        public DateTime ArrivalDate => _up.ArrivalDate;

        public UpdateType UpdateType => _up.UpdateType;

        public PublicationState PublicationState => _up.PublicationState;

        public InstallationBehavior InstallationBehavior => _up.InstallationBehavior;

        public InstallationBehavior UninstallationBehavior => _up.UninstallationBehavior;

        public bool IsApproved => _up.IsApproved;

        public bool IsDeclined => _up.IsDeclined;

        public bool HasStaleUpdateApprovals => _up.HasStaleUpdateApprovals;

        public bool IsLatestRevision => _up.IsLatestRevision;

        public bool HasEarlierRevision => _up.HasEarlierRevision;

        public UpdateState State => _up.State;

        public bool HasLicenseAgreement => _up.HasLicenseAgreement;

        public bool RequiresLicenseAgreementAcceptance => _up.RequiresLicenseAgreementAcceptance;

        public bool HasSupersededUpdates => _up.HasSupersededUpdates;

        public bool IsSuperseded => _up.IsSuperseded;

        public bool IsWsusInfrastructureUpdate => _up.IsWsusInfrastructureUpdate;

        public bool IsEditable => _up.IsEditable;

        public UpdateSource UpdateSource => _up.UpdateSource;

        public void AcceptLicenseAgreement() => _up.AcceptLicenseAgreement();
        public IUpdateApproval Approve(UpdateApprovalAction action, IComputerTargetGroup targetGroup) => _up.Approve(action, targetGroup);
        public IUpdateApproval Approve(UpdateApprovalAction action, IComputerTargetGroup targetGroup, DateTime deadline) => _up.Approve(action, targetGroup, deadline);
        public IUpdateApproval ApproveForOptionalInstall(IComputerTargetGroup targetGroup) => _up.ApproveForOptionalInstall(targetGroup);
        public void CancelDownload() => _up.CancelDownload();
        public void Decline() => _up.Decline();
        public void ExpirePackage() => _up.ExpirePackage();
        public void ExportPackageMetadata(string fileName) => _up.ExportPackageMetadata(fileName);
        public RevisionChanges GetChangesFromPreviousRevision() => _up.GetChangesFromPreviousRevision();
        public ReadOnlyCollection<IInstallableItem> GetInstallableItems() => _up.GetInstallableItems();
        public ILicenseAgreement GetLicenseAgreement() => _up.GetLicenseAgreement();
        public UpdateCollection GetRelatedUpdates(UpdateRelationship relationship) => _up.GetRelatedUpdates(relationship);
        public IUpdateSummary GetSummary(ComputerTargetScope computersToInclude) => _up.GetSummary(computersToInclude);
        public IUpdateSummary GetSummaryForComputerTargetGroup(IComputerTargetGroup targetGroup) => _up.GetSummaryForComputerTargetGroup(targetGroup);
        public IUpdateSummary GetSummaryForComputerTargetGroup(IComputerTargetGroup targetGroup, bool includeSubgroups) => _up.GetSummaryForComputerTargetGroup(targetGroup, includeSubgroups);
        public UpdateSummaryCollection GetSummaryPerComputerTargetGroup() => _up.GetSummaryPerComputerTargetGroup();
        public UpdateSummaryCollection GetSummaryPerComputerTargetGroup(bool includeSubgroups) => _up.GetSummaryPerComputerTargetGroup(includeSubgroups);
        public StringCollection GetSupportedUpdateLanguages() => _up.GetSupportedUpdateLanguages();
        public UpdateApprovalCollection GetUpdateApprovals() => _up.GetUpdateApprovals();
        public UpdateApprovalCollection GetUpdateApprovals(IComputerTargetGroup targetGroup) => _up.GetUpdateApprovals(targetGroup);
        public UpdateApprovalCollection GetUpdateApprovals(IComputerTargetGroup targetGroup, UpdateApprovalAction approvalAction, DateTime fromApprovalDate, DateTime toApprovalDate) => _up.GetUpdateApprovals(targetGroup, approvalAction, fromApprovalDate, toApprovalDate);
        public UpdateCategoryCollection GetUpdateCategories() => _up.GetUpdateCategories();
        public IUpdateClassification GetUpdateClassification() => _up.GetUpdateClassification();
        public UpdateEventCollection GetUpdateEventHistory(DateTime fromDate, DateTime toDate) => _up.GetUpdateEventHistory(fromDate, toDate);
        public UpdateInstallationInfoCollection GetUpdateInstallationInfoPerComputerTarget(IComputerTargetGroup targetGroup) => _up.GetUpdateInstallationInfoPerComputerTarget(targetGroup);
        public UpdateInstallationInfoCollection GetUpdateInstallationInfoPerComputerTarget(IComputerTargetGroup targetGroup, bool includeSubgroups) => _up.GetUpdateInstallationInfoPerComputerTarget(targetGroup, includeSubgroups);
        public UpdateInstallationInfoCollection GetUpdateInstallationInfoPerComputerTarget(ComputerTargetScope computersToInclude) => _up.GetUpdateInstallationInfoPerComputerTarget(computersToInclude);
        public void PurgeAssociatedReportingEvents(DateTime fromDate, DateTime toDate) => _up.PurgeAssociatedReportingEvents(fromDate, toDate);
        public void Refresh() => _up.Refresh();
        public void RefreshUpdateApprovals() => _up.RefreshUpdateApprovals();
        public void ResumeDownload() => _up.ResumeDownload();

        #endregion


        #region Cool Functions
        public bool MatchesInfo(UMProductInfo info)
        {
            Type t = GetType();
            foreach (string s in info.Is)
            {
                object getProp = t.InvokeMember(s, BindingFlags.GetProperty, null, this, null);
                if (!getProp.Equals(info.LookingFor[s]))
                {
                    return false;
                }
            }
            if (!info.AllProductSearch)
            {
                if (!Products.Contains(info.BaseProduct))
                {
                    return false;
                }
                if (!info.BaseProduct.Contains("Office"))
                {
                    if (Title.IndexOf(info.Name, StringComparison.OrdinalIgnoreCase) < 0 || 
                        (!string.IsNullOrEmpty(info.DesiredPlats) && Title.IndexOf(info.DesiredPlats, StringComparison.Ordinal) < 0) ||
                        (info.MutuallyExclusiveTo.Length > 0 && info.MutuallyExclusiveTo.Any(Title.Contains)))
                    {
                        return false;
                    }
                }
                else
                {
                    if (Title.IndexOf(info.Name, StringComparison.OrdinalIgnoreCase) < 0 ||
                        (!string.IsNullOrEmpty(info.DesiredPlats) && Title.IndexOf(info.DesiredPlats, StringComparison.OrdinalIgnoreCase) < 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool MatchesInfo(UMKbQuery query)
        {
            var s = (string)KBArticles;
            return s != query.SearchFor
                ? false
                : !string.IsNullOrEmpty(query.Product) && Title.IndexOf(query.Product, StringComparison.OrdinalIgnoreCase) < 0 ||
                !string.IsNullOrEmpty(query.Arc) && Title.IndexOf(query.Arc, StringComparison.OrdinalIgnoreCase) < 0
                ? false
                : true;
        }

        #endregion
    }
}
