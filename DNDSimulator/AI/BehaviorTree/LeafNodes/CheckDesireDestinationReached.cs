using System.Collections;
using DNDSimulator.AI.BehaviorTree.Base;


/// <summary>
/// Checks if the AI has reached its desired destination
/// </summary>
public class CheckDesireDestinationReached : LeafNode {

    public CheckDesireDestinationReached(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {

    }

    public override NodeStatus Tick() {
        LocationBehaviour target = (LocationBehaviour)tree.treeData[BehaviorTreeData.DesireDestination];
        AiActor actor = tree.treeData[BehaviorTreeData.Actor] as AiActor;
        foreach (Topic top in actor.currentLocationTopics) {
            if (target.DesireAffordance == top) {
                return NodeStatus.Success;
            }
        }
        return NodeStatus.Failure;
    }
}
