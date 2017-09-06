using System;
using UnityEngine;
using System.Collections.Generic;

public class InverterNode : DecoratorNode {
    
    public InverterNode(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        
    }
    
    public override NodeStatus Tick() {
        NodeStatus childStatus = child.Tick();
        if (childStatus == NodeStatus.Failure) {
            #if(BT_DEBUG)
            Debug.Log(tag + " Success");
            #endif
            return NodeStatus.Success;
        }
        if (childStatus == NodeStatus.Success) {
            #if(BT_DEBUG)
            Debug.Log(tag + " Failure");
            #endif
            return NodeStatus.Failure;
        }
        return childStatus;
    }
}


