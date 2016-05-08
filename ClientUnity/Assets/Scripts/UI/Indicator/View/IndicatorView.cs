using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MVC;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class IndicatorView : ViewBase
    {
        [SerializeField]
        private IndicatorItemView _itemPrefab;

        [SerializeField]
        private GameObject _selectedContent;

        [SerializeField]
        private GameObject _allContent;

    }
}

