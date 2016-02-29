
using System;
using System.Collections.Generic;
using System.Linq;


public class ClusterUnit
{
    public string Row;
    public int Cluster;
}

public class ClusterDataItem
{
    public string Row;
    public string Column;
    public float Value;
    public int Cluster;

    public string Id;
}

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

public static class Clustering
{
    private static ClusterMap _origCluster;
    private static ClusterMap _normalizeCluster;

    public static void Init(ClusterMap mapData)
    {
        _origCluster = mapData;
        _normalizeCluster = Normolize(_origCluster);
    }

    public static List<ClusterUnit> GetClasters(int clustersCount)
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
            int columnsInCluster = columnsCount/clustersCount;

            int columnsInClusterCount = 0;
            int currentClaster = 0;

            columns.Sort((x, y) => x.Value.CompareTo(y.Value));

            for(var i = 0; i < columnsCount; i++)
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

            var cluster = currentRowClasterSum/clustersCount;
            clasters.Add(new ClusterUnit() {Row = key, Cluster = cluster});

        }

        return clasters;
    }

    private static ClusterMap Normolize(ClusterMap orig)
    {
       var result = new ClusterMap();
        float averageQuadratic = 0;
        float sampleMean = 0;

        var keys = orig.ColumnsKeys;
        foreach(var key in keys)
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
                averageQuadratic = (float) Math.Sqrt((1f / ((float)columns.Count - 1f)) * sumOfDifferenceCurrentSample);

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

    public static ClusterMap GetNormalize()
    {
        return _normalizeCluster;
    }
}
