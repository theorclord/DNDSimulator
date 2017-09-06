using System.Collections;

namespace DNDSimulator.AI.BehaviorTree.Base
{
  public class DecoratorNode : BehaviorTreeNode
  {

    public BehaviorTreeNode child;

    public DecoratorNode(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag)
    {

    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }
  }
}
