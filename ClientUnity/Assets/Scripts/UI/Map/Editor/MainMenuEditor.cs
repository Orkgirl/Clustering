
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;


[CustomEditor(typeof (TopMenuView))]
public class MainMenuEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //if (GUILayout.Button("copyColors"))
        //{
        //    var mainMenu = target as MainMenu;

        //    var map = mainMenu.GetComponentInChildren<Map>();
        //    map._colors = mainMenu._colors;

        //    //EditorUtility
        //}
    }
}
