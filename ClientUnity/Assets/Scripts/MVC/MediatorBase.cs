using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;

namespace Assets.Scripts.MVC
{
    public abstract class MediatorBase
    {
        public abstract void Mediate(ViewBase view);
        public abstract void UnMediate();
        public abstract ViewBase View { get; }

        public virtual void Hide()
        {
            
        }

        public virtual void Show()
        {
            
        }
    }
}
