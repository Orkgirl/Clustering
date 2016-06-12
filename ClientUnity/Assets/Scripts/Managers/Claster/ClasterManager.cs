using System;
using System.Collections.Generic;
using Assets.Scripts.Entity;

namespace Assets.Scripts.Managers.Claster
{
    public class ClasterManager : IEntity
    {
        private ClusterMap _origCluster;
        private ClusterMap _normalizeCluster;

        public  bool IsInitialize { get; private set; }

        public List<string> Indicators { get; set; }
        public List<string> IndicatorsAll { get; private set; }

        public void Install()
        {
            
        }

        public void Initialaze()
        {
            
        }

        public void Tick(float deltaTime)
        {
            
        }

        public void ParseData(StorageMapData storageMapData)
        {
            var mapData = new ClusterMap();

            foreach (var location in storageMapData.map)
            {
                foreach (var storagelocationDataKeyValue in location.data)
                {
                    mapData.Add(new ClusterDataItem() { Row = location.name, Column = storagelocationDataKeyValue.key, Id = location.id, Value = storagelocationDataKeyValue.value });
                }
            }
            
            _origCluster = mapData;
            _normalizeCluster = Normolize(_origCluster);

            Indicators = _origCluster.ColumnsKeys;
            IndicatorsAll = _origCluster.ColumnsKeys;

            if (_origCluster != null && _normalizeCluster != null)
            {
                IsInitialize = true;
            }
        }

        public List<ClusterUnit> GetClasters(int clustersCount)
        {
            var clasters = new List<ClusterUnit>();
            
            foreach (var key in Indicators)
            {
                List<ClusterDataItem> columns = _normalizeCluster.ColumnsToList(key);

                if (columns.Count == 0)
                {
                    continue;
                }
                
                int currentClaster = 1;

                columns.Sort((x, y) => x.Value.CompareTo(y.Value));

                for (var i = 0; i < columns.Count; i++)
                {
                    if (currentClaster < clustersCount)
                    {
                        ++currentClaster;
                    }
                    else
                    {
                        currentClaster = 1;
                    }
                   

                    columns[i].Cluster = currentClaster;
                    //_normalizeCluster.Update(columns[i]);
                }
            }

            List<string> rowKeys = _normalizeCluster.RowsKeys;
            List<ClusterDataItem> rows;
            int currentCounter = 0;
            int biggestCounter = 0;
            int frequentClaster = 0;

            foreach (var key in rowKeys)
            {
                rows = _normalizeCluster.RowsToList(key);

                for (var i = 0; i < rows.Count; i++)
                {
                    if(!Indicators.Contains(rows[i].Column))
                    {
                        continue;
                    }
                    for (var j = 0; j < rows.Count; j++)
                    {
                        if (!Indicators.Contains(rows[j].Column))
                        {
                            continue;
                        }
                        if (rows[i].Cluster == rows[j].Cluster)
                        {
                            currentCounter++;
                        }
                    }
                    if (currentCounter > biggestCounter)
                    {
                        biggestCounter = currentCounter;
                        frequentClaster = rows[i].Cluster;
                    }
                    currentCounter = 0;
                }
                //frequentClaster = rows[0].Cluster;
                //var line = new StringBuilder();
                //line.Append(frequentClaster.ToString("00"));
                //line.Append(" - ");
                //for (var i = 0; i < rows.Count; i++)
                //{
                //    line.Append(rows[i].Cluster.ToString("00"));
                //    line.Append(" ");
                //}

                //Debug.Log(line);

                clasters.Add(new ClusterUnit() { Row = key, Cluster = frequentClaster});

                biggestCounter = 0;
                currentCounter = 0;
                frequentClaster = 0;
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
