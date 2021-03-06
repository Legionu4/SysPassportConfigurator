﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Input;
using System.Windows;

namespace SysPassportConfigurator.ViewModels
{
    public class ConfigsViewModel : ObservableObject
    {
        private readonly DbEngine.FirebirdDbEngine dataBase = new DbEngine.FirebirdDbEngine(@"127.0.01:D:\arm_vz\Reg\Tune\reg\BASE2009\REG201612.GDB");

        #region VzName
        private string _VzName = "";
        public string VzName
        {
            get { return _VzName; }
            set
            {
                _VzName = value;
                RaisePropertyChangedEvent("VzName");
            }
        }
        #endregion

        #region IndexVz
        private string _indexVz;
        public string IndexVz
        {
            get { return _indexVz; }
            set
            {
                _indexVz = value;
                RaisePropertyChangedEvent("IndexVz");
            }
        }
        #endregion

        #region additional index 
        private bool _cbAddIndex;
        public bool CbAddIndex
        {
            get { return _cbAddIndex; }
            set
            {
                if (this._cbAddIndex != value)
                {
                    this._cbAddIndex = value;
                    RaisePropertyChangedEvent("CbAddIndex");
                }
                //_cbAddIndex = value;
                //RaisePropertyChangedEvent("CbAddIndex");
            }
        }

        private string _addIndex;
        public string AddIndex
        {
            get { return _addIndex; }
            set
            {
                _addIndex = value;
                RaisePropertyChangedEvent("AddIndex");
            }
        }
        #endregion

        public ICommand UpdateDataCommand
        {
            get { return new DelegateCommand(UpdateSomeData); }
        }

        public void UpdateSomeData()
        {
            string sql = "select sp.code , sp.value_param from sys_passport sp where sp.code in (1,2,62)";

            DataTable dt = dataBase.ExecuteQuery(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                VzName = dt.Rows[1]["value_param"].ToString();
                IndexVz = dt.Rows[0]["value_param"].ToString();
                AddIndex = dt.Rows[2]["value_param"].ToString();
            }

            //CbAddIndex = true;
        }
    }
}
