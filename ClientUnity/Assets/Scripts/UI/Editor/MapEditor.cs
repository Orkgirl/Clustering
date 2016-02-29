using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;


[CustomEditor(typeof(Map))]
public class MapEditor : Editor {

    private Vector2 _scroll = Vector2.zero;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Map map = (Map)target;

        GUILayout.Label("Map");

        if (GUILayout.Button("Add"))
        {
            map.MapList.Add(new MapImageData());
        }

        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        MapImageData delete = null;

        foreach (var keyValue in map.MapList)
        {
            GUILayout.BeginHorizontal();

            keyValue.Key = EditorGUILayout.TextArea(keyValue.Key);

            var isNullBegin = keyValue.Value == null;
           
            keyValue.Value = (Image)EditorGUILayout.ObjectField(keyValue.Value, typeof(Image), true);

            var isNullEnd = keyValue.Value == null;

            if (isNullBegin && !isNullEnd)
            {
                keyValue.Key = keyValue.Value.name;
            }


            if (GUILayout.Button("X"))
            {
                delete = keyValue;
            }

            GUILayout.EndHorizontal();
        }
        if (delete != null)
        {
            map.MapList.Remove(delete);
        }

        EditorGUILayout.EndScrollView();
    }
}
