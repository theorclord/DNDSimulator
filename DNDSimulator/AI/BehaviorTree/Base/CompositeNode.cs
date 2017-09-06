using System.Collections;
using System.Collections.Generic;

namespace DNDSimulator.AI.BehaviorTree.Base
{
  public class CompositeNode : BehaviorTreeNode
  {

    public List<BehaviorTreeNode> children;

    public CompositeNode(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag)
    {
      this.children = new List<BehaviorTreeNode>();
    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }

  }
}
