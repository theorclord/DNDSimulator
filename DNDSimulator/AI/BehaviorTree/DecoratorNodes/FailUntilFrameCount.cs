using UnityEngine;
using System.Collections;

public class FailUntilFrameCount : DecoratorNode {

    int framecount;
    int framesSinceLastTick;

    public FailUntilFrameCount(int framecount, BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        this.framecount = framecount;
        framesSinceLastTick = 0;
    }
    
    public override NodeStatus Tick() {
        if (framesSinceLastTick > framecount) {
            framesSinceLastTick = 0;
            return child.Tick();
        } else {
            framesSinceLastTick++;
            return NodeStatus.Failure;
        }
    }
}

