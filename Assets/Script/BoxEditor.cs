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
        
            myScript.InitBox();
        
    }
}
