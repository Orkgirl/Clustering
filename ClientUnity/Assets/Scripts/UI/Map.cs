using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class MapImageData
{
    [SerializeField]
    public string Key;

    [SerializeField]
    public Image Value;
}

public class Map : UIItem
{

    public List<MapImageData> MapList;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
