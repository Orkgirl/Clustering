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
        if (toggle.Label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Label: ");
            toggle.Label.text = GUILayout.TextField(toggle.Label.text);
            GUILayout.EndHorizontal();
        }
    }
}
