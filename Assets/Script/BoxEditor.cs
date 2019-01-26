using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Box))]
public class BoxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Box myScript = (Box)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.InitBox();
        }
    }
}
