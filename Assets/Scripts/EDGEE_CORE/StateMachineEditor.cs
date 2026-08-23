using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FSM))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    { 
       base.OnInspectorGUI();

       FSM fsm = (FSM)target;

       EditorGUILayout.Space(30);
       EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null)
        {
            return;
        }

        if (fsm.stateMachine.CurrentState != null)
        { 
            EditorGUILayout.LabelField("Current State: " + fsm.stateMachine.CurrentState);
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout)
        {
          if (fsm.stateMachine.dictionaryStates != null)
          { 
             var keys = fsm.stateMachine.dictionaryStates.Keys.ToArray();
             var vals = fsm.stateMachine.dictionaryStates.Values.ToArray();

             for (int i = 0; i < keys.Length; i++)
             {
                EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
             }
          }
        }

    }
}
