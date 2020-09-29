using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BehaviourTree)), CanEditMultipleObjects]
public class BehaviourTreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BehaviourTree behaviourTree = (BehaviourTree)target;

        EditorGUILayout.LabelField(behaviourTree.root.name);
        ShowAction(behaviourTree.root, 1);
    }

    public void ShowAction(Action action, int depth)
    {
        string spacer = "";
        for (int i = 0; i < depth; i++)
        {
            spacer += "    ";
        }
        if (action is Composite)
        {
            Composite composite = (Composite)action;
            for (int i = 0; i < composite.children.Count; i++)
            {
                //EditorGUILayout.PropertyField()
                EditorGUILayout.LabelField(spacer + composite.children[i].name);
                ShowAction(composite.children[i], depth + 1);

            }

            //for (int i = 0; i < composite.children.Count; i++)
            //{
            //    ShowAction(composite.children[i], depth + 1);
            //}
        }
        else if (action is Action)
        {
            //EditorGUILayout.LabelField(spacer + action.name);
        }

    }
}


[CustomEditor(typeof(MonsterBehaviour)), CanEditMultipleObjects]
public class MonsterBehaviourEditor : BehaviourTreeEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}