using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Test))]
public class TestCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Test myScript = (Test)target;

        if (GUILayout.Button("Execute Test")) {
            myScript.Execute();
        }
    }
}
