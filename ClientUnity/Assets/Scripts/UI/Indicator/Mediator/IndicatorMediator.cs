using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;
using Assets.Scripts.Managers;
using Assets.Scripts.MVC;

namespace Assets.Scripts.UI
{
    public class IndicatorMediator : MediatorBase<ViewBase>
    {
        private IndicatorView _view;

        private ClasterManager _clasterManager;

        public override void Mediate(ViewBase view)
        {
            _view = (IndicatorView) view;

            _clasterManager = EntityContext.Get<ClasterManager>();

            _view.ClickAddEvent += ViewAddClickEvent;
            _view.ClickRemoveEvent += ViewRemoveClickEvent;

            _view.SetAllIndicators(_clasterManager.IndicatorsAll);
            _view.SetSelectedIndicators(_clasterManager.Indicators);
        }

        private void ViewAddClickEvent(string id)
        {
            if (_clasterManager.Indicators.Contains(id))
            {
                return;
            }
            _clasterManager.Indicators.Add(id);
            _view.SetSelectedIndicators(_clasterManager.Indicators);
        }

        private void ViewRemoveClickEvent(string id)
        {
            _clasterManager.Indicators.Remove(id);
            _view.SetSelectedIndicators(_clasterManager.Indicators);
        }

        public override void UnMediate()
        {
            _view.ClickAddEvent -= ViewAddClickEvent;
            _view.ClickRemoveEvent -= ViewRemoveClickEvent;
        }

        public override ViewBase View
        {
            get { return _view; }
        }
    }
}
