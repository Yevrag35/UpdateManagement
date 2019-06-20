﻿using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal;
using Microsoft.UpdateServices.Internal.DatabaseAccess;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MG.UpdateManagement.Objects
{
    public class UmUpdate : MarshalByRefObject, IUpdate
    {
        #region FIELDS/CONSTANTS
        private StringCollection _infoUrls;
        private StringCollection _companyTitles;
        private string _description;
        private Guid _eulaId;
        private UpdateRevisionId _id;
        private InstallationBehavior _installBehavior;
        private StringCollection _kbArticles;
        private string _legacyName;
        private MsrcSeverity _msrcSeverity;
        private StringCollection _productFamilyTitles;
        private StringCollection _productTitles;
        private string _releaseNotes;
        private StringCollection _securityBulletins;
        private UpdateState _updateState;
        private string _title;
        private InstallationBehavior _uninstallBehavior;
        private string _updateClassificationTitle;
        private UpdateServer _updateServer;


        #endregion

        #region PROPERTIES
        public StringCollection AdditionalInformationUrls => _infoUrls;
        public DateTime ArrivalDate { get; set; }
        public StringCollection CompanyTitles => _companyTitles;
        public DateTime CreationDate { get; set; }
        public string DefaultPropertiesLanguage { get; set; }
        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException("Description");
        }
        public bool HasEarlierRevision { get; set; }
        public bool HasLicenseAgreement => _eulaId != Guid.Empty;
        public bool HasStaleUpdateApprovals { get; set; }
        public bool HasSupersededUpdates { get; set; }
        public UpdateRevisionId Id
        {
            get => _id;
            set => _id = value ?? throw new ArgumentNullException("Id");
        }
        public InstallationBehavior InstallationBehavior
        {
            get => _installBehavior;
            set => _installBehavior = value ?? throw new ArgumentNullException("InstallationBehavior");
        }
        public bool IsApproved { get; set; }
        public bool IsBeta { get; set; }
        public bool IsDeclined { get; set; }
        public bool IsEditable { get; set; }
        public bool IsLatestRevision { get; set; }
        public bool IsSuperseded { get; set; }
        public bool IsWsusInfrastructureUpdate { get; set; }
        public StringCollection KnowledgebaseArticles => _kbArticles;
        public string LegacyName
        {
            get => _legacyName;
            set => _legacyName = value ?? throw new ArgumentNullException("LegacyName");
        }
        public MsrcSeverity MsrcSeverity
        {
            get => _msrcSeverity;
            set
            {
                if (value > MsrcSeverity.Critical)
                    throw new ArgumentOutOfRangeException("MsrcSeverity", value, LocalizedStrings.InvalidEnumerationValue);
                _msrcSeverity = value;
            }
        }
        public StringCollection ProductFamilyTitles => _productFamilyTitles;
        public StringCollection ProductTitles => _productTitles;
        public PublicationState PublicationState { get; set; }
        private string ReleaseNotes
        {
            get => _releaseNotes;
            set => _releaseNotes = value ?? throw new ArgumentNullException("ReleaseNotes");
        }
        public bool RequiresLicenseAgreementAcceptance { get; set; }
        public StringCollection SecurityBulletins => _securityBulletins;
        public long Size { get; set; }
        public UpdateState State
        {
            get => _updateState;
            set
            {
                if ((value - 1) > UpdateState.Failed)
                    throw new ArgumentOutOfRangeException("UpdateState");

                _updateState = value;
            }
        }
        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException("Title");
        }
        public InstallationBehavior UninstallationBehavior
        {
            get => _uninstallBehavior;
            set => _uninstallBehavior = value ?? throw new ArgumentNullException("UninstallationBehavior");
        }
        public string UpdateClassificationTitle
        {
            get => _updateClassificationTitle;
            set
            {
                StringValidation.ValidateUpdateContainerTitleString("UpdateClassificationTitle", value);
                _updateClassificationTitle = value;
            }
        }
        public UpdateServer UpdateServer => _updateServer;
        public UpdateSource UpdateSource { get; set; }
        public UpdateType UpdateType { get; set; }

        #endregion

        #region CONSTRUCTORS
        public UmUpdate(UpdateServer updateServer)
        {
            _title = string.Empty;
            _description = string.Empty;
            _legacyName = string.Empty;
            _releaseNotes = string.Empty;
            _updateClassificationTitle = string.Empty;
            _kbArticles = new StringCollection();
            _securityBulletins = new StringCollection();
            _productTitles = new StringCollection();
            _productFamilyTitles = new StringCollection();
            _infoUrls = new StringCollection();
            _updateServer = updateServer;
            _id = new UpdateRevisionId();
            _installBehavior = new InstallationBehavior();
            _uninstallBehavior = new InstallationBehavior();
        }
        public UmUpdate(UpdateServer updateServer, GenericReadableRow tableRow)
            : this(updateServer)
        {
            _id = new UpdateRevisionId(tableRow.GetGuid(0), tableRow.GetInt32(1));
            _installBehavior = new InstallationBehavior(tableRow.GetBoolean(5), tableRow.GetBoolean(9),
                (InstallationImpact)tableRow.GetInt32(6), (RebootBehavior)tableRow.GetInt32(7),
                tableRow.GetBoolean(8));

            _uninstallBehavior = new InstallationBehavior(tableRow.GetBoolean(10), tableRow.GetBoolean(14), 
                (InstallationImpact)tableRow.GetInt32(11), (RebootBehavior)tableRow.GetInt32(12), tableRow.GetBoolean(13));

            this.PublicationState = (PublicationState)tableRow.GetInt32(0x10);
            this.CreationDate = tableRow.GetDateTime(0x11);
            this.ArrivalDate = tableRow.GetDateTime(0x19);
            this.IsApproved = tableRow.GetBoolean(0x12);
            this.State = (UpdateState)tableRow.GetByte(0x13);
            _eulaId = tableRow.GetGuid(20);
            this.IsDeclined = tableRow.GetBoolean(0x16);
            this.RequiresLicenseAgreementAcceptance = tableRow.GetBoolean(0x15);
            this.HasStaleUpdateApprovals = tableRow.GetBoolean(0x17);
            this.HasEarlierRevision = tableRow.GetBoolean(0x20);
            this.IsLatestRevision = tableRow.GetBoolean(0x18);
            _title = tableRow.GetString(0x1d);
            _description = tableRow.GetString(30);
            _releaseNotes = tableRow.GetString(0x1f);
            _legacyName = tableRow.GetString(0x1b);
            this.UpdateType = (UpdateType)Enum.Parse(typeof(ExtendedUpdateType), tableRow.GetString(15));
            _msrcSeverity = (MsrcSeverity)Enum.Parse(typeof(MsrcSeverity), tableRow.GetString(0x1c), true);
            this.HasSupersededUpdates = tableRow.GetBoolean(0x1a);
            this.IsSuperseded = tableRow.GetBoolean(0x22);
            this.IsWsusInfrastructureUpdate = tableRow.GetBoolean(0x23);
            this.IsEditable = tableRow.GetBoolean(0x23);
            this.UpdateSource = (UpdateSource)tableRow.GetInt32(0x24);
        }

        #endregion

        #region PUBLIC METHODS
        public void AcceptLicenseAgreement()
        {
            object[] args = new object[1] { _updateServer };
            var access = (AdminDataAccess)ClassFactory.CreateInstance(typeof(AdminDataAccess), args);
            try
            {
                access.ExecuteSPAcceptEula(_eulaId, UpdateServer.GetCurrentUserName(), _id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            this.RequiresLicenseAgreementAcceptance = false;
        }

        public IUpdateApproval Approve(UpdateApprovalAction action, IComputerTargetGroup targetGroup, DateTime deadline, bool isAssigned)
        {
            if (targetGroup == null)
                throw new ArgumentNullException("targetGroup");

            if (!UpdateApproval.IsUpdateApprovalActionExplicit(action))
                throw new ArgumentOutOfRangeException("action");

            if ((deadline != DateTime.MaxValue) && (((action == UpdateApprovalAction.Install) && this.InstallationBehavior.CanRequestUserInput)
                || ((action == UpdateApprovalAction.Uninstall) && this.UninstallationBehavior.CanRequestUserInput)))
                throw new InvalidOperationException(LocalizedStrings.ErrorCannotApproveWithDeadline);

            object[] args = new object[1] { _updateServer };
            var ada = (AdminDataAccess)ClassFactory.CreateInstance(typeof(AdminDataAccess), args);
            GenericReadableRow row = ada.ExecuteSPDeployUpdate1(_id, (int)action, targetGroup.Id, deadline, UpdateServer.GetCurrentUserName(), isAssigned);

            var inst = Activator.CreateInstance(typeof(UpdateApproval), BindingFlags.NonPublic, null, new object[1] { row }, CultureInfo.CurrentCulture);
            
        }

        #endregion

        #region BACKEND/PRIVATE METHODS


        #endregion
    }
}