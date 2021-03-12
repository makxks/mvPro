using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

#if (UNITY_EDITOR)
[CustomEditor(typeof(GunInstance))]
public class GunEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        GunInstance myTarget = (GunInstance)target;

        myTarget.GunName = EditorGUILayout.TextField("Gun Name", myTarget.GunName);

        serializedObject.ApplyModifiedProperties();


    }
}
#endif
