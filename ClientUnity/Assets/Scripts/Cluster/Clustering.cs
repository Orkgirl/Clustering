
using System;
using System.Collections.Generic;


public class ClusterColumn
{
    public string Name;
    public string Id;
    public float Value;
}

public class ClusterMap
{
    public Dictionary<string, List<ClusterColumn>> Columns;
    public ClusterMap()
    {
        Columns = new Dictionary<string, List<ClusterColumn>>();
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

    public static ClusterMap Normolize(ClusterMap orig)
    {
       var result = new ClusterMap();
        float averageQuadratic = 0;
        float sampleMean = 0;

        var keys = orig.Columns.Keys;
        foreach(var key in keys)
        {
            List<ClusterColumn> columns;
            if(orig.Columns.TryGetValue(key, out columns))
            {
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

                result.Columns.Add(key, new List<ClusterColumn>());
                for (int i = 0; i < columns.Count; i++)
                {
                    var columnItem = new ClusterColumn();
                    columnItem.Value = (columns[i].Value - sampleMean) / averageQuadratic;
                    columnItem.Name = columns[i].Name;
                    columnItem.Id = columns[i].Id;
                    result.Columns[key].Add(columnItem);
                }
            }
            else
            {
                UnityEngine.Debug.LogError("Error! Invalid key" + key);
            }
        }
        return result;
    }

    public static ClusterMap GetNormalize()
    {
        return _normalizeCluster;
    }
}
