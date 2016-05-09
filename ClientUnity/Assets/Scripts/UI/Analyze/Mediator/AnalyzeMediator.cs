using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;

namespace Assets.Scripts.UI
{
    public class AnalyzeMediator : MediatorBase<ViewBase>
    {
        private AnalyzeView _view;

        private ClasterManager _clasterManager;

        private int _clusterCount = 2;

        public override void Mediate(ViewBase view)
        {
            _view = (AnalyzeView) view;

            _clasterManager = EntityContext.Get<ClasterManager>();

            _view.Clear();

            var listClusterCount = new List<string>();
            for (var i = 2; i < 27; i++)
            {
                listClusterCount.Add(i.ToString());
            }

            _view.SetClusterCountDropdown(listClusterCount);

            _view.SetClusterColumn(_clasterManager.GetNormalize().ColumnsKeys);
            _view.SetClusterRow(_clasterManager.GetNormalize().RowsKeys);

            _view.InitDataGrid(_clasterManager.GetClasters(_clusterCount));

            _view.SetClasterCountEvent += SetClasterCountHendler;
            _view.UpdateDataEvent += UpdateDataHendler;
        }

        private void UpdateDataHendler()
        {
            _view.InitDataGrid(_clasterManager.GetClasters(_clusterCount));
        }

        private void SetClasterCountHendler(int value)
        {
            _clusterCount = value + 2;
        }

        public override void UnMediate()
        {
            _view.SetClasterCountEvent -= SetClasterCountHendler;
            _view.UpdateDataEvent -= UpdateDataHendler;
        }

        public override ViewBase View
        {
            get { return _view; }
        }
    }
}
