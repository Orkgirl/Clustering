using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MapMediator : MediatorBase<ViewBase>
    {
        private MapView _view;

        private ClasterManager _clasterManager;

        private int _clustersCount = 2;
        private int _columnIndex = 0;

        public override void Mediate(ViewBase view)
        {
            _view = (MapView) view;

            _clasterManager = EntityContext.Get<ClasterManager>();
            
            _view.SetClasterCountEvent += SetClasterCountHendler;
            _view.ShowOnMapColumnEvent += ShowOnMapColumnHendler;
            _view.UpdateButtonEvent += UpdateButtonHendler;

            _view.SetMapColumn(_clasterManager.GetRaw().ColumnsKeys.ToList());

            var listClusterCount = new List<string>();
            for (var i = 2; i < 27; i++)
            {
                listClusterCount.Add(i.ToString());
            }
           
            _view.SetMapClusterCount(listClusterCount);
        }

        private void UpdateButtonHendler()
        {
            var list = _clasterManager.GetRaw().ColumnsKeys.ToList();
            if (_columnIndex < list.Count)
            {
                ShowOnMapColumn(list[_columnIndex]);
            }
        }

        private void ShowOnMapColumnHendler(int index)
        {
            _columnIndex = index;
            _view.UpdateData();
        }

        private void SetClasterCountHendler(int index)
        {
            _clustersCount = index + 2;
            _view.UpdateData();
        }

        public void ShowOnMapClaser(List<ClusterUnit> clasers)
        {
            var map = _clasterManager.GetNormalize();

            for (var i = 0; i < clasers.Count; i++)
            {
                var item = map.GetFirstInRow(clasers[i].Row);
                _view.SetColor(item.Id, _view.Colors[clasers[i].Cluster]);
            }
        }

        public void ShowOnMapColumn(string column)
        {
            if (_clustersCount < 1)
            {
                return;
            }

            List<ClusterDataItem> columns = _clasterManager.GetRaw().ColumnsToList(column);

            int itemInClaster = (int)Mathf.Round((float)columns.Count / (float)_clustersCount);

            int currentClaster = 0;
            int columnsInClusterCount = 0;

            columns.Sort((x, y) => x.Value.CompareTo(y.Value));

            for (var i = 0; i < columns.Count; i++)
            {
                _view.SetColor(columns[i].Id, _view.Colors[currentClaster]);

                ++columnsInClusterCount;

                if (columnsInClusterCount >= itemInClaster)
                {
                    columnsInClusterCount = 0;
                    if (currentClaster < _clustersCount - 1)
                    {
                        ++currentClaster;
                    }
                }
            }
        }

        public override void UnMediate()
        {
            _view.SetClasterCountEvent += SetClasterCountHendler;
            _view.ShowOnMapColumnEvent += ShowOnMapColumnHendler;
            _view.UpdateButtonEvent += UpdateButtonHendler;
        }

        public override ViewBase View
        {
            get { return _view; }
        }
    }
}
