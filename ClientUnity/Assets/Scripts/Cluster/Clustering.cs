
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
    }

    public static ClusterMap Normolize(ClusterMap orig)
    {
       var result = new ClusterMap();

        return result;
    }
}
