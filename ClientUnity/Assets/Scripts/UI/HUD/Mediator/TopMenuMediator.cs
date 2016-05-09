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
        private ClasterManager _clasterManager;


        private List<string> _dataList;
        private string _selectedTable;

      
        public override void Mediate(ViewBase view)
        {
            _view = (TopMenuView)view;

            _windowsManager = EntityContext.Get<WindowsManager>();
            _resourcesManager = EntityContext.Get<ResourcesManager>();
            _clasterManager = EntityContext.Get<ClasterManager>();

            _dataList = _resourcesManager.GetAllTables();
            if (_dataList.Count > 0)
            {
                ViewSelectTableSelect(_dataList[0]);
            }
            _view.SetTableSelect(_dataList);
            
            _view.SelectTableSelect += ViewSelectTableSelect;

            _view.LoadButtonEvent += LoadButtonHendler;
            _view.DataGridButtonEvent += DataGridButtonHendler;
            _view.IndicatorButtonEvent += IndicatorButtonHendler;
            _view.AnalizeButtonEvent += AnalizeButtonHendler;
            _view.MapButtonEvent += MapButtonHendler;
        }

        private void IndicatorButtonHendler()
        {
            _windowsManager.Open(WindowType.Indicator);
        }

        private void DataGridButtonHendler()
        {
            _windowsManager.Open(WindowType.DataGrid);
        }

        private void MapButtonHendler()
        {
            _windowsManager.Open(WindowType.Map);
        }

        private void AnalizeButtonHendler()
        {
            _windowsManager.Open(WindowType.Analize);
        }

        private void LoadButtonHendler()
        {
            _resourcesManager.LoadData(_selectedTable);
            _clasterManager.ParseData(_resourcesManager.StorageMapData);
            _windowsManager.Open(WindowType.DataGrid);
            //_windowsManager.Open(WindowType.Indicators);
        }

        private void ViewSelectTableSelect(string value)
        {
            _selectedTable = _resourcesManager.GetTable(value);
        }

        public override void UnMediate()
        {
            _view.SelectTableSelect -= ViewSelectTableSelect;

            _view.LoadButtonEvent -= LoadButtonHendler;
            _view.DataGridButtonEvent -= DataGridButtonHendler;
            _view.IndicatorButtonEvent -= IndicatorButtonHendler;
            _view.AnalizeButtonEvent -= AnalizeButtonHendler;
            _view.MapButtonEvent -= MapButtonHendler;
        }

        public override ViewBase View
        {
            get { return _view; }
        }

      
    }
}
