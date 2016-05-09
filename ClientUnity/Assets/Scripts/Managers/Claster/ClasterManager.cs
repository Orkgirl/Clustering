using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Entity;

namespace Assets.Scripts.Managers
{
    public class ClasterManager : IEntity
    {
        private ClusterMap _origCluster;
        private ClusterMap _normalizeCluster;

        private bool _isInitialize = false;
        public  bool IsInitialize { get { return _isInitialize; } }

        public List<String> Indicators { get; set; }
        public List<String> IndicatorsAll { get; private set; }

        public void Install()
        {
            
        }

        public void Initialaze()
        {
            
        }

        public void ParseData(StorageMapData storageMapData)
        {
            ClusterMap mapData = new ClusterMap();

            foreach (var location in storageMapData.map)
            {
                foreach (var storagelocationDataKeyValue in location.data)
                {
                    mapData.Add(new ClusterDataItem() { Row = location.name, Column = storagelocationDataKeyValue.key, Id = location.id, Value = storagelocationDataKeyValue.value });
                }
            }

            if (mapData != null)
            {
                _origCluster = mapData;
                _normalizeCluster = Normolize(_origCluster);

                Indicators = _origCluster.ColumnsKeys;
                IndicatorsAll = _origCluster.ColumnsKeys;


                if (_origCluster != null && _normalizeCluster != null)
                {
                    _isInitialize = true;
                }
            }
        }

        public List<ClusterUnit> GetClasters(int clustersCount)
        {
            var clasters = new List<ClusterUnit>();

            var columnsKeys = _normalizeCluster.ColumnsKeys;
            foreach (var key in columnsKeys)
            {
                List<ClusterDataItem> columns = _normalizeCluster.ColumnsToList(key);

                if (columns.Count == 0)
                {
                    continue;
                }

                int columnsCount = columns.Count;
                int columnsInCluster = columnsCount / clustersCount;

                int columnsInClusterCount = 0;
                int currentClaster = 0;

                columns.Sort((x, y) => x.Value.CompareTo(y.Value));

                for (var i = 0; i < columnsCount; i++)
                {
                    if (columnsInClusterCount > columnsInCluster)
                    {
                        columnsInClusterCount = 0;
                        ++currentClaster;
                    }
                    ++columnsInClusterCount;

                    columns[i].Cluster = currentClaster;
                    _normalizeCluster.Update(columns[i]);
                }
            }

            var rowKeys = _normalizeCluster.RowsKeys;
            foreach (var key in rowKeys)
            {
                List<ClusterDataItem> rows = _normalizeCluster.RowsToList(key);


                int currentRowClasterSum = 0;

                for (var i = 0; i < rows.Count; i++)
                {
                    currentRowClasterSum += rows[i].Cluster;
                }

                var cluster = currentRowClasterSum / clustersCount;
                clasters.Add(new ClusterUnit() { Row = key, Cluster = cluster });

            }

            return clasters;
        }

      

        private ClusterMap Normolize(ClusterMap orig)
        {
            var result = new ClusterMap();
            float averageQuadratic = 0;
            float sampleMean = 0;

            var keys = orig.ColumnsKeys;
            foreach (var key in keys)
            {
                List<ClusterDataItem> columns = orig.ColumnsToList(key);

                if (columns.Count == 0)
                {
                    continue;
                }

                float sumOfAllValues = 0;
                for (int i = 0; i < columns.Count; i++)
                {
                    sumOfAllValues += columns[i].Value;
                }
                sampleMean = (1f / (float)columns.Count) * sumOfAllValues;

                float sumOfDifferenceCurrentSample = 0;
                for (int i = 0; i < columns.Count; i++)
                {
                    sumOfDifferenceCurrentSample += (columns[i].Value - sampleMean) * (columns[i].Value - sampleMean);
                }
                averageQuadratic = (float)Math.Sqrt((1f / ((float)columns.Count - 1f)) * sumOfDifferenceCurrentSample);

                //result.Columns.Add(key, new List<ClusterDataItem>());
                for (int i = 0; i < columns.Count; i++)
                {
                    var columnItem = new ClusterDataItem();

                    columnItem.Row = columns[i].Row;
                    columnItem.Column = columns[i].Column;
                    columnItem.Value = (columns[i].Value - sampleMean) / averageQuadratic;

                    columnItem.Id = columns[i].Id;

                    result.Add(columnItem);
                }

            }
            return result;
        }

        public ClusterMap GetNormalize()
        {
            return _normalizeCluster;
        }

        public  ClusterMap GetRaw()
        {
            return _origCluster;
        }

       
    }
}
