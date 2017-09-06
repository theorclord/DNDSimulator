using System.Collections;
using System.Collections.Generic;
using DNDSimulator.AI.BehaviorTree.Base;

namespace DNDSimulator.AI.BehaviorTree.CompositeNodes
{
  public class SequenceNode : CompositeNode
  {

    public SequenceNode(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag)
    {

    }

    public override NodeStatus Tick()
    {
      foreach (BehaviorTreeNode child in children)
      {
        NodeStatus childStatus = child.Tick();
        if (childStatus == NodeStatus.Running)
        {
#if (BT_DEBUG)
                Debug.Log(tag + " Running");
#endif
          return NodeStatus.Running;
        }
        if (childStatus == NodeStatus.Failure)
        {
#if (BT_DEBUG)
                Debug.Log(tag + " Child Failure");
                Debug.Log(tag + " Failure");
#endif
          return NodeStatus.Failure;
        }
      }
#if (BT_DEBUG)
        Debug.Log(tag + " Success");
#endif
      return NodeStatus.Success;
    }
  }
}
