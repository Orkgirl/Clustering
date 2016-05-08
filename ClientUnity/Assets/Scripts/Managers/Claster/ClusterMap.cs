using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Managers
{
    public class ClusterMap
    {
        private Dictionary<string, Dictionary<string, ClusterDataItem>> _columns;
        private Dictionary<string, Dictionary<string, ClusterDataItem>> _rows;

        public ClusterMap()
        {
            _columns = new Dictionary<string, Dictionary<string, ClusterDataItem>>();
            _rows = new Dictionary<string, Dictionary<string, ClusterDataItem>>();
        }

        public void Add(ClusterDataItem item)
        {
            if (!_columns.ContainsKey(item.Column))
            {
                _columns[item.Column] = new Dictionary<string, ClusterDataItem>();
            }

            _columns[item.Column].Add(item.Row, item);

            if (!_rows.ContainsKey(item.Row))
            {
                _rows[item.Row] = new Dictionary<string, ClusterDataItem>();
            }

            _rows[item.Row].Add(item.Column, item);
        }

        public ClusterDataItem GetFirstInRow(string row)
        {
            if (_rows.ContainsKey(row))
            {
                return _rows[row].Values.First();
            }

            return null;
        }

        public void Update(ClusterDataItem item)
        {
            if (_columns.ContainsKey(item.Column) && _columns[item.Column].ContainsKey(item.Row) &&
                _rows.ContainsKey(item.Row) && _rows[item.Row].ContainsKey(item.Column))
            {
                _rows[item.Row][item.Column].Cluster = item.Cluster;
                _rows[item.Row][item.Column].Value = item.Value;

                _columns[item.Column][item.Row].Cluster = item.Cluster;
                _columns[item.Column][item.Row].Value = item.Value;
            }
            else
            {
                Add(item);
            }
        }

        public List<string> ColumnsKeys
        {
            get { return _columns.Keys.ToList(); }
        }

        public List<string> RowsKeys
        {
            get { return _rows.Keys.ToList(); }
        }

        public List<ClusterDataItem> ColumnsToList(string column)
        {
            return _columns[column].Values.ToList();
        }

        public List<ClusterDataItem> RowsToList(string row)
        {
            return _rows[row].Values.ToList();
        }
    }
}