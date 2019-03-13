using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TeagueRoom))]
public class TeagueRoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Pre-Label Room"))
        {
            TeagueRoom myTeagueRoom = (TeagueRoom)target;
            myTeagueRoom.preLabelRoom();
        }
    }
}