using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : TreeNode
{
    protected List<TreeNode> m_children = new List<TreeNode>();
    protected TreeNode m_pendingChild = null;

    public void Add(TreeNode node)
    { 
        m_children.Add(node);
    }
}
