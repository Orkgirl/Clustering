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

        public ToggleParent ToggleParentPrefab;

        public UIItem target;


        public float offsetX = 10;
        public float offsetY = -10;

        public float pendingX = 35;
        public float pendingY = -30;
        public float targetWidth = 500;

        public IndicatorNoda indicatorMap;
        private List<ToggleParent> _list;


        public void SetAllRegions(List<string> listData)
        {
            _list = new List<ToggleParent>();

            indicatorMap = new IndicatorNoda();
            indicatorMap.Name = "root";
            indicatorMap.Child = new List<IndicatorNoda>();

            foreach (var item in listData)
            {
                indicatorMap.Child.Add(new IndicatorNoda() { Name = item });
            }

            DrawMapItem(indicatorMap, new Vector3(offsetX, offsetY));

            target.GetComponent<RectTransform>().sizeDelta = new Vector2(targetWidth, Mathf.Abs(_list.Count * pendingY + offsetY * 2));
        }

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
        
        private List<ToggleParent> DrawMapItem(IndicatorNoda map, Vector3 offset)
        {
            var result = new List<ToggleParent>();

            Vector3 position = new Vector3(offset.x, offsetY + _list.Count * pendingY);

            var itemToggle = target.AddChild<ToggleParent>(ToggleParentPrefab.gameObject);
            itemToggle.transform.localPosition = position;
            itemToggle.Id = map.Id;
            itemToggle.name = map.Name;
            itemToggle.Label.text = map.Name;
            itemToggle.onValueChanged.AddListener(OnValueChanged);
            _list.Add(itemToggle);

            if (map.Child != null)
            {
                foreach (var itemMap in map.Child)
                {
                    result.AddRange(DrawMapItem(itemMap, new Vector3(offset.x + pendingX, offset.y)));
                }
            }

            foreach (var toggle in result)
            {
                itemToggle.child.Add(toggle);
            }

            result.Add(itemToggle);
            return result;
        }

        private void OnValueChanged(bool value)
        {

        }
    }
}

