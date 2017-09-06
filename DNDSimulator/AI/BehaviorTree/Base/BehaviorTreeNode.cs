using System.Collections;
using System.Collections.Generic;


namespace DNDSimulator.AI.BehaviorTree.Base
{
  public enum NodeStatus
  {
    Success,
    Failure,
    Running
  }

  public class BehaviorTreeNode
  {

    public BehaviorTree tree;
    public BehaviorTreeNode parent;
    public string tag;

    public BehaviorTreeNode(BehaviorTree tree, BehaviorTreeNode parent, string tag)
    {
      this.tree = tree;
      this.parent = parent;
      this.tag = tag;
    }

    // the run function takes the controller as input to directly set its
    // behaviour in a leaf node
    public virtual NodeStatus Tick()
    {
      return NodeStatus.Success;
    }
  }
}
