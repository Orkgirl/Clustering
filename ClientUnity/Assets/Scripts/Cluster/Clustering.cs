
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
        List<ClusterColumn> normalizeList = new List<ClusterColumn>();
        float originalValue = 0;
        float averageQuadratic = 0;
        float sampleMean = 0;
        float normaliseValue;

        var keys = orig.Columns.Keys;
        foreach(var key in keys)
        {
            List<ClusterColumn> columns;
            if(orig.Columns.TryGetValue(key, out columns))
            {
                float sumOfAllValues = 0;
                for (int i = 0; i < columns.Count; i++)
                {                    
                    sumOfAllValues += columns[i].Value;
                }
                sampleMean = (1 / columns.Count) * sumOfAllValues;

                float sumOfDifferenceCurrentSample = 0;
                for (int i = 0; i < columns.Count; i++)
                {
                    sumOfDifferenceCurrentSample += (columns[i].Value - sampleMean) * (columns[i].Value - sampleMean);
                }
                averageQuadratic = (float) Math.Sqrt((1 / (columns.Count - 1)) * sumOfDifferenceCurrentSample);
            }
            else
            {
                UnityEngine.Debug.LogError("!!!!!!!!!!!!");
            }
        }
               
        normaliseValue = (originalValue - sampleMean) / averageQuadratic;

        return result;
    }
}
