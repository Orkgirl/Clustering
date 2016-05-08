using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MVC;

namespace Assets.Scripts.UI
{
    public class DataGridMediator : MediatorBase<ViewBase>
    {
        private DataGridView _view;

        public override void Mediate(ViewBase view)
        {
            _view = (DataGridView) view;
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
