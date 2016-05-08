using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Entity;

namespace Assets.Scripts.MVC
{
    public abstract class MediatorBase<T> where T : ViewBase
    {
        public abstract void Mediate(T view);
        public abstract void UnMediate();
        public abstract T View { get; }
    }
}
