using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR)
[CustomEditor(typeof(ChangeGun))]
public class ChangeGunEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChangeGun myScript = (ChangeGun)target;
        if (GUILayout.Button("Trace Rifle"))
        {
            myScript.selectGun("Trace Rifle");
        }

        if (GUILayout.Button("Rocket"))
        {
            myScript.selectGun("Rocket");
        }

        if (GUILayout.Button("Auto Rifle"))
        {
            myScript.selectGun("Auto Rifle");
        }

        if (GUILayout.Button("Shotgun"))
        {
            myScript.selectGun("Shotgun");
        }

        if (GUILayout.Button("Lightning Rifle"))
        {
            myScript.selectGun("Lightning Rifle");
        }

        if (GUILayout.Button("Charge Rifle"))
        {
            myScript.selectGun("Charge Rifle");
        }

        if (GUILayout.Button("Grenade Launcher"))
        {
            myScript.selectGun("Grenade Launcher");
        }
    }
}
#endif