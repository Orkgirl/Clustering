using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.UI.Windows;
using Assets.Scripts.MVC;

namespace Assets.Scripts.UI.HUD
{
    public class TopMenuMediator : MediatorBase<ViewBase>
    {
        private TopMenuView _view;


        private WindowsManager _windowsManager;
        private ResourcesManager _resourcesManager;


        private List<string> _dataList;
        private string _selectedTable;

      
        public override void Mediate(ViewBase view)
        {
            _view = (TopMenuView)view;

            _windowsManager = EntityContext.Get<WindowsManager>();
            _resourcesManager = EntityContext.Get<ResourcesManager>();

            _dataList = _resourcesManager.TableNames.Keys.ToList();
            if (_dataList.Count > 0)
            {
                _selectedTable = _dataList[0];
            }
            _view.SetTableSelect(_dataList);
            
            _view.SelectTableSelect += ViewSelectTableSelect;

            _view.LoadButtonEvent += LoadButtonHendler;
            _view.DataGridButtonEvent += DataGridButtonHendler;
            _view.IndicatorButtonEvent += IndicatorButtonHendler;
        }

        private void IndicatorButtonHendler()
        {
            _windowsManager.Open(WindowType.Indicators);
        }

        private void DataGridButtonHendler()
        {
            _windowsManager.Open(WindowType.DataGrid);
        }

        private void LoadButtonHendler()
        {
            //_resourcesManager.LoadData(_selectedTable);
            //InitDataGrid(Clustering.GetRaw());
            //_windowsManager.Open(WindowType.Indicators);
        }

        private void ViewSelectTableSelect(string value)
        {
            string tableName;
            if (_resourcesManager.TableNames.TryGetValue(value, out tableName))
            {
                _resourcesManager.LoadData(tableName);
            }
        }

        public override void UnMediate()
        {
            _view.LoadButtonEvent -= LoadButtonHendler;
        }

        

        public override ViewBase View
        {
            get { return _view; }
        }

      
    }
}
