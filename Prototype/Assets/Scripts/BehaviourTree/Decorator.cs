
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Unity;
using System;


public abstract class Decorator : BTNode
{
    public BTNode child;

    public Decorator()
    {


        //CreateInputPort(mainContainer, 5, Color.black, Color.white, "Input");
        //CreateLabel(mainContainer, 10, Color.gray, Color.black, title);
        //CreateOutputPort(mainContainer, 5, Color.black, Color.white, "Output");

    }

    //public override void AddNode(GraphView graphView)
    //{
    //    graphView.AddElement(this);
    //    SetPosition(new Rect(x, y, 100, 150));
    //    var port = CreateOutputPort(mainContainer, 0, Color.black, Color.white, $"");
    //    child.AddNode(graphView);
    //    if (child.input != null)
    //    {
    //        var edge = port.ConnectTo(child.input);
    //        graphView.Add(edge);
    //    }
    //}

    public override string GetPath(string input)
    {
        string newString = input + " " + GetType().Name;
        newString = child.GetPath(newString);
        return newString;
    }
}
