using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : TreeNode
{
    protected TreeNode m_child;
    public Decorator(TreeNode child)
    {
        m_child = child;
    }
}
