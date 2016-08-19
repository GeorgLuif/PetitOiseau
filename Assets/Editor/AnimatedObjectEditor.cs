using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(AnimatedObject))]

public class AnimatedObjectEditor : Editor
    {

        public override void OnInspectorGUI()
        {

        DrawDefaultInspector();

        AnimatedObject myScript = (AnimatedObject)target;

        if (GUILayout.Button("RecordPos1"))
            myScript.RecordPos1();

        if (GUILayout.Button("RecordPos2"))
            myScript.RecordPos2();

        if (GUILayout.Button("SwitchBetweenPos1&2"))
            myScript.SwitchBetween();

        if (GUILayout.Button("DebugAnimate"))
            myScript.Animate();


    }


    }   
