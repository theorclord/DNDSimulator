using System.Collections;

namespace DNDSimulator.AI.BehaviorTree.Base
{
  public class LeafNode : BehaviorTreeNode
  {

    public bool running { get; protected set; }

    /// <summary>
    /// Basic leaf node for the behavior tree
    /// </summary>
    /// <param name="tree">The behavior tree for the node</param>
    /// <param name="parent">The parent node in the behavior tree</param>
    /// <param name="tag">Sub group tag</param>
    public LeafNode(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag)
    {

    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }

  }
}

