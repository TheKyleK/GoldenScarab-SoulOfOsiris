using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private TreeGraphView graphView;
    private EditorWindow window;
    private Texture2D indentationIcon;
    public void Init(EditorWindow window, TreeGraphView graphView)
    {
        this.window = window;
        this.graphView = graphView;

        indentationIcon = new Texture2D(1, 1);
        indentationIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        indentationIcon.Apply();
    }
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"), 0),
            //new SearchTreeGroupEntry(new GUIContent("Dialogue Node"), 1),
            new SearchTreeEntry(new GUIContent("Selector", indentationIcon))
            {
                userData = new SelectorNode(), level = 1
            },
            new SearchTreeEntry(new GUIContent("Sequence", indentationIcon))
            {
                userData = new SequenceNode(), level = 1
            }

        };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        return false;
        //var worldMousePosition = window.rootVisualElement.ChangeCoordinatesTo(
        //    window.rootVisualElement.parent,
        //    context.screenMousePosition - window.position.position
        //);

        //var localMousePosition = graphView.contentContainer.WorldToLocal(worldMousePosition);
        //switch (SearchTreeEntry.userData)
        //{
        //    case SelectorNode selectorNode:
        //        graphView.AddElement(selectorNode);
        //        selectorNode.SetPosition(new Rect(localMousePosition, new Vector2(100, 150)));
        //        return true;
        //    case SequenceNode sequenceNode:
        //        graphView.AddElement(sequenceNode);
        //        sequenceNode.SetPosition(new Rect(localMousePosition, new Vector2(100, 150)));
        //        return true;
        //    default:
        //        return false;
        //}
    }
}