using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class TreeGraphWindow : EditorWindow
{
    private TreeGraphView graphView;
    /// <summary>
    /// Create a menu item that opens a editor window
    /// </summary>
    [MenuItem("Graph/Tree Graph")]
    public static void OpenWindow()
    {
        var window = GetWindow<TreeGraphWindow>();
        window.titleContent = new GUIContent("Tree Graph");
    }

    private void ConstructGraph()
    {
        graphView = new TreeGraphView(this)
        {
            name = "Tree Graph"
        };


        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);

        GameObject selection = Selection.activeTransform?.gameObject;
        if (selection != null)
        {
            MonsterBehaviour behaviour = selection.GetComponent<MonsterBehaviour>();
            if (behaviour !=null)
            {
                var root = behaviour.root;
                if (root != null)
                {
                    //root.AddNode(graphView);
                    //graphView.AddElement(behaviour.root);
                }

            }
        }
    }

    private void OnSelectionChange()
    {
        rootVisualElement.Remove(graphView);
        ConstructGraph();
    }

    private void OnEnable()
    {
        ConstructGraph();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}
