using Microsoft.UpdateServices.Administration;
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
    public class UmUpdateApproval : MarshalByRefObject, IUmObject, IUpdateApproval
    {
        #region FIELDS/CONSTANTS
        private UpdateApprovalAction _action;
        private UpdateApprovalState _state;
        private string _administratorName;

        #endregion

        #region PROPERTIES

        public UpdateApprovalAction Action
        {
            get => _action;
            set
            {
                if (!IsUpdateApprovalActionValid(value) || (value == UpdateApprovalAction.All))
                {
                    throw new ArgumentOutOfRangeException("Action");
                }
                _action = value;
            }
        }
        public string AdministratorName
        {
            get => _administratorName;
            set => _administratorName = value ?? throw new ArgumentNullException("AdministratorName");
        }
        public Guid ComputerTargetGroupId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime GoLiveTime { get; set; }
        public Guid Id { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsOptional => !this.IsAssigned;
        public UpdateApprovalState State
        {
            get => _state;
            set
            {
                if (value > UpdateApprovalState.Active)
                    throw new ArgumentOutOfRangeException("State");

                _state = value;
            }
        }
        public UpdateRevisionId UpdateId { get; set; }
        public UpdateServer UpdateServer { get; }

        #endregion

        #region CONSTRUCTORS
        public UmUpdateApproval(UpdateServer us)
        {
            this.Deadline = DateTime.MaxValue;
            this.UpdateServer = us;
            this.UpdateId = new UpdateRevisionId();
        }
        public UmUpdateApproval(UpdateServer us, GenericReadableRow row)
            : this(us)
        {
            int num = row.GetInt32(2);
            _action = num != 2
                ? (UpdateApprovalAction)num
                : UpdateApprovalAction.NotApproved;

            this.CreationDate = row.GetDateTime(0);
            if (!row.IsDBNull(4))
                this.Deadline = row.GetDateTime(4);

            this.GoLiveTime = row.GetDateTime(3);
            this.Id = row.GetGuid(6);
            this.IsAssigned = row.GetBoolean(7);
            this.UpdateId = new UpdateRevisionId(row.GetGuid(8), row.GetInt32(9));
            _state = (UpdateApprovalState)row.GetByte(1);
            this.ComputerTargetGroupId = row.GetGuid(10);
            this.AdministratorName = row.GetString(5);
        }

        #endregion

        #region METHODS
        public static UpdateApprovalCollection BuildUpdateApprovalCollection(GenericReadableRow[] rows, UpdateServer us)
        {
            if (rows == null)
                throw new ArgumentNullException("rows");

            UmUpdateApproval[] apArr = new UmUpdateApproval[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                apArr[i] = new UmUpdateApproval(us, rows[i]);
            }
            var apCol = new UpdateApprovalCollection();
            apCol.AddRange(apArr);
            return apCol;
        }

        public void Delete()
        {
            object[] args = new object[1] { this.UpdateServer };
            var ada = (AdminDataAccess)ClassFactory.CreateInstance(typeof(AdminDataAccess), args);
            try
            {
                ada.ExecuteSPDeleteDeployment(this.Id, UpdateServer.GetCurrentUserName());
            }
            catch (WsusObjectNotFoundException)
            {
            }
        }

        public IComputerTargetGroup GetComputerTargetGroup()
        {
            ComputerTargetGroup byId = null;
            try
            {
                byId = ComputerTargetGroup.GetById(this.ComputerTargetGroupId, this.UpdateServer);
            }
            catch (WsusObjectNotFoundException ex)
            {
                throw new WsusObjectNotFoundException(LocalizedStrings.TargetGroupForDeploymentNotFound, ex);
            }
            return byId;
        }

        public IUpdate GetUpdate() => UmUpdate.GetById(this.UpdateId, this.UpdateServer);

        public static bool IsUpdateApprovalActionValid(UpdateApprovalAction action)
        {
            bool result = false;
            if ((action <= UpdateApprovalAction.Uninstall) || (action == UpdateApprovalAction.NotApproved) || action == UpdateApprovalAction.All)
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region REFLECTION
        private void SetProperties(UmUpdateApproval obj)
        {
            IEnumerable<PropertyInfo> allProps = this.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance).Where(
                    x => x.CanWrite);

            foreach (PropertyInfo pi in allProps)
            {
                object newVal = pi.GetValue(obj);
                if (newVal != null)
                    pi.SetValue(this, newVal);
            }
        }

        #endregion
    }
}