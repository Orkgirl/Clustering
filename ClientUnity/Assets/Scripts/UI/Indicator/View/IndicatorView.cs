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
        private UIItem _selectedContent;
        private List<IndicatorItemView> _selectedList = new List<IndicatorItemView>();

        [SerializeField]
        private UIItem _allContent;
        private List<IndicatorItemView> _allList = new List<IndicatorItemView>();

        private event Action<string> _clickAddEvent;
        public event Action<string> ClickAddEvent
        {
            add { _clickAddEvent += value; }
            remove { _clickAddEvent -= value; }
        }

        private event Action<string> _clickRemoveEvent;
        public event Action<string> ClickRemoveEvent
        {
            add { _clickRemoveEvent += value; }
            remove { _clickRemoveEvent -= value; }
        }

        private void ItemViewAddClickHendler(string id)
        {
            if (_clickAddEvent != null)
            {
                _clickAddEvent.Invoke(id);
            }
        }
        private void ItemViewRemoveClickHendler(string id)
        {
            if (_clickRemoveEvent != null)
            {
                _clickRemoveEvent.Invoke(id);
            }
        }

        public void SetAllIndicators(List<string> value)
        {
            foreach (var itemView in _allList)
            {
                itemView.ClickEvent -= ItemViewAddClickHendler;
                Destroy(itemView.gameObject);
            }

            _allList.Clear();

            foreach (var id in value)
            {
                var itemView = _allContent.AddChild<IndicatorItemView>(_itemPrefab.gameObject);
                itemView.Id = id;
                itemView.ClickEvent += ItemViewAddClickHendler;
                _allList.Add(itemView);
            }
        }

        public void SetSelectedIndicators(List<string> value)
        {
            foreach (var itemView in _selectedList)
            {
                itemView.ClickEvent -= ItemViewRemoveClickHendler;
                Destroy(itemView.gameObject);
            }
            _selectedList.Clear();

            foreach (var id in value)
            {
                var itemView = _selectedContent.AddChild<IndicatorItemView>(_itemPrefab.gameObject);
                itemView.Id = id;
                itemView.ClickEvent += ItemViewRemoveClickHendler;
                _selectedList.Add(itemView);
            }
        }
    }
}

