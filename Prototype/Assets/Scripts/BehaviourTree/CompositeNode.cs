
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections;
using System.Collections.Generic;


public abstract class CompositeNode : BTNode
{
    protected List<BTNode> children = new List<BTNode>();
    public BTNode pendingChild = null;
    //public VisualElement wrapper;
    

    public CompositeNode()
    {
        //CreateInputPort(mainContainer, 5, Color.black, Color.white, "Input");
        //CreateLabel(mainContainer, 10, Color.gray, Color.black, title);
        //CreateButton(mainContainer, 5, Color.black, Color.white, "Add Child", () => {
        //    var portContainer = new VisualElement();
        //    var port = CreateOutputPort(portContainer, 0, Color.black, Color.white, "");
        //    CreateButton(portContainer, 3, Color.black, Color.white, "x", () =>
        //    {
        //        wrapper.Remove(portContainer);
        //        RefreshExpandedState();
        //        RefreshPorts();
        //    });

        //    portContainer.style.flexDirection = FlexDirection.Row;
        //    portContainer.style.alignContent = Align.Center;
        //    wrapper.Add(portContainer);
        //});

        //wrapper = new VisualElement();
        //mainContainer.Add(wrapper);
        //wrapper.style.marginTop = 5;
        //wrapper.style.marginLeft = 5;
        //wrapper.style.marginRight = 5;
        //wrapper.style.marginBottom = 5;
        //wrapper.style.flexDirection = FlexDirection.Row;


        //for (int i = 0; i < children.Count; i++)
        //{
        //    CreateOutputPort(wrapper, 0, Color.black, Color.white, $"{i + 1}:");
        //}
    }

    public void Add(BTNode node)
    {
       
        children.Add(node);
    }

    //public override void AddNode(GraphView graphView)
    //{
    //    graphView.AddElement(this);
    //    SetPosition(new Rect(x, y, 100, 150));
    //    float mid = children.Count / 2;
    //    for (int i = 0; i < children.Count; i++)
    //    {
    //        var port = CreateOutputPort(wrapper, 0, Color.black, Color.white, $"");
    //        children[i].AddNode(graphView);
    //        if (children[i].input != null)
    //        {
    //            var edge = port.ConnectTo(children[i].input);
    //            graphView.Add(edge);
    //        }
    //    }
    //}

    public override string GetPath(string input)
    {
        string newString = input + " " + GetType().Name;
        foreach (BTNode child in children)
        {
            newString = child.GetPath(newString);
        }
        return newString;
    }
}
