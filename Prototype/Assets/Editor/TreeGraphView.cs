using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeGraphView : GraphView
{
    private NodeSearchWindow nodeSearchWindow;
    public TreeGraphView(EditorWindow window)
    {
        styleSheets.Add(Resources.Load<StyleSheet>("Graph"));

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());


        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();


        //AddElement(GenerateEntryNode());
        //AddSearch
        AddSearchWindow(window);
    }

    private Node GenerateEntryNode()
    {
        var node = new Node();

        //var outputPort = GeneratePort(node, Direction.Output);
        //outputPort.portName = "Next";
        //node.outputContainer.Add(outputPort);

        //node.outputContainer.style.flexDirection = FlexDirection.Row;

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(296, 50, 100, 150));
        return node;
    }

    

    public Port GeneratePort(Node node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Vertical, portDirection, capacity, typeof(float));
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        nodeSearchWindow.Init(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), nodeSearchWindow);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach(port =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;
    }

    public Port CreateInputPort(
    Node node,
    VisualElement wrapper,
    float margin,
    Color backgroundColor,
    Color textColor,
    string text
)
    {
        var inputContainer = new VisualElement();
        var inputPort = node.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(float));
        inputContainer.Add(inputPort);
        inputPort.portName = text;
        wrapper.Add(inputContainer);
        inputPort.style.marginTop = margin;
        inputPort.style.marginLeft = margin;
        inputPort.style.marginRight = margin;
        inputPort.style.marginBottom = margin;
        inputPort.style.color = textColor;
        inputContainer.style.backgroundColor = backgroundColor;
        inputContainer.style.alignSelf = Align.Center;

        node.RefreshExpandedState();
        node.RefreshPorts();
        return inputPort;
    }

    public Port CreateOutputPort(
        Node node,
        VisualElement wrapper,
        float margin,
        Color backgroundColor,
        Color textColor,
        string text
    )
    {
        var outputContainer = new VisualElement();
        var outputPort = node.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(float));
        outputContainer.Add(outputPort);
        outputPort.portName = text;
        wrapper.Add(outputContainer);
        outputPort.style.marginTop = margin;
        outputPort.style.marginLeft = margin;
        outputPort.style.marginRight = margin;
        outputPort.style.marginBottom = margin;
        outputPort.style.color = textColor;
        outputPort.style.alignSelf = Align.Center;
        outputContainer.style.backgroundColor = backgroundColor;

        node.RefreshExpandedState();
        node.RefreshPorts();
        return outputPort;
    }

    public Label CreateLabel(
        VisualElement wrapper,
        float margin,
        Color backgroundColor,
        Color textColor,
        string text
    )
    {
        var labelContainer = new VisualElement();
        var label = new Label(text);
        labelContainer.Add(label);
        wrapper.Add(labelContainer);
        label.style.marginTop = margin;
        label.style.marginLeft = margin;
        label.style.marginRight = margin;
        label.style.marginBottom = margin;
        label.style.color = textColor;
        labelContainer.style.backgroundColor = backgroundColor;
        return label;
    }

    public Button CreateButton(
        VisualElement wrapper,
        float margin,
        Color backgroundColor,
        Color textColor,
        string text,
        Action callback)
    {
        var buttonContainer = new VisualElement();
        var button = new Button(callback);
        button.text = text;
        buttonContainer.Add(button);
        wrapper.Add(buttonContainer);
        button.style.marginTop = margin;
        button.style.marginLeft = margin;
        button.style.marginRight = margin;
        button.style.marginBottom = margin;
        button.style.color = textColor;
        button.style.unityTextAlign = TextAnchor.MiddleCenter;
        buttonContainer.style.backgroundColor = backgroundColor;
        return button;
    }

    public virtual void AddNode(GraphView graphView, Node node, Vector2 position)
    {
        node.SetPosition(new Rect(position, new Vector2(100, 150)));
        graphView.AddElement(node);
    }

    public Node CreateNode()
    {
        var node = new Node();
        node.title = GetType().Name;
        node.mainContainer.Clear();
        node.mainContainer.style.flexDirection = FlexDirection.Column;
        node.mainContainer.style.backgroundColor = new Color(0, 0, 0, 1);
        node.mainContainer.style.unityTextAlign = TextAnchor.MiddleCenter;
        node.mainContainer.style.alignContent = Align.Center;
        node.mainContainer.style.alignSelf = Align.Center;
        return node;
    }

}
