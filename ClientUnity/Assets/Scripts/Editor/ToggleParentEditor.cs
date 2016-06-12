using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof (ToggleParent))]
public class ToggleParentEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        ToggleParent toggle = (ToggleParent) target;
        
    }
}
