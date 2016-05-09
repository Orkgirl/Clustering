using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;

namespace Assets.Scripts.UI
{
    public class DataGridMediator : MediatorBase<ViewBase>
    {
        private DataGridView _view;

        private ClasterManager _clasterManager;

        public override void Mediate(ViewBase view)
        {
            _view = (DataGridView) view;

            _clasterManager = EntityContext.Get<ClasterManager>();

            var clusterMap = _clasterManager.GetRaw();

            var header = new List<string>() { "Name:" };

            header.AddRange(_clasterManager.Indicators);

            Dictionary<string, List<string>> stringData = new Dictionary<string, List<string>>();

            foreach (var rowsKey in clusterMap.RowsKeys)
            {
                var stringList = new List<string>() { rowsKey };
                foreach (var clusterDataItem in clusterMap.RowsToList(rowsKey))
                {
                    if (_clasterManager.Indicators.Contains(clusterDataItem.Column))
                    {
                        stringList.Add(clusterDataItem.Value.ToString());
                    }
                }

                stringData.Add(rowsKey, stringList);
            }

            _view.SetData(header, stringData);
        }

        public override void UnMediate()
        {
            
        }

        public override ViewBase View
        {
            get { return _view; }
        }
    }
}
