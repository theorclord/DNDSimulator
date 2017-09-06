using UnityEngine;
using System.Collections;

/// <summary>
/// Sets the destination for the AI actor.
/// </summary>
public class SetAiActorDestination : LeafNode {

    public SetAiActorDestination(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {

    }

    /// <summary>
    /// Sets the destination for the AI actor.
    /// </summary>
    /// <returns>Success once the position is set, Failure if target is unreachable</returns>
    public override NodeStatus Tick() {
        AiActor actor = (AiActor)tree.treeData [BehaviorTreeData.Actor];
        LocationBehaviour target = (LocationBehaviour)tree.treeData [BehaviorTreeData.DesireDestination];
        bool desiredStimulated = (bool)tree.treeData[BehaviorTreeData.DesireStimulated];
        bool success = true;
        if(!desiredStimulated && !actor.HasTarget){
            success = actor.SetTarget(target.GetRandomPositionInLocation2D());
        }
        if (success) {
            return NodeStatus.Success;
        } else {
            return NodeStatus.Failure;
        }
    }
}
