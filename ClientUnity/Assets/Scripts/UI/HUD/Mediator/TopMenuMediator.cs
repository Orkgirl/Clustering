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

            _view.OnLoadButtonEvent += OnLoadButtonEvent;
            _view.OnSelectTableSelect += ViewOnOnSelectTableSelect;
        }

        private void ViewOnOnSelectTableSelect(string value)
        {
            string tableName;
            if (_resourcesManager.TableNames.TryGetValue(value, out tableName))
            {
                _resourcesManager.LoadData(tableName);
            }
        }

        public override void UnMediate()
        {
            _view.OnLoadButtonEvent -= OnLoadButtonEvent;
        }

        private void OnLoadButtonEvent()
        {
            //_resourcesManager.LoadData(_selectedTable);

            _windowsManager.Open(WindowType.Indicators);
        }

        public override ViewBase View
        {
            get { return _view; }
        }

      
    }
}
