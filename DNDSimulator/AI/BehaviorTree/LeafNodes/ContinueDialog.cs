using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinueDialog : LeafNode {

    DialogBehavior dialogBehavior;
    AiActor actor;
    
    public ContinueDialog(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        dialogBehavior = (DialogBehavior)tree.treeData [BehaviorTreeData.DialogBehavior];
        actor = (AiActor)tree.treeData[BehaviorTreeData.Actor];
    }
    
    public override NodeStatus Tick() {
        if(dialogBehavior.IsEngagedInDialog){

            if (dialogBehavior.CurrentDialog is PlayerDialog) {

                actor.LookAt((dialogBehavior.CurrentDialog as PlayerDialog).Player.transform.position);   

            } else {

                DialogBehavior lastSpeaker = dialogBehavior.CurrentDialog.LastSpeaker;
                if(lastSpeaker == null || lastSpeaker == dialogBehavior){
                    actor.LookAt(dialogBehavior.GetCurrentDialogPartners()[0].transform.position);   
                } else {
                    actor.LookAt(lastSpeaker.transform.position);
                }
                #if(BT_DEBUG)
                Debug.Log(tag + " Success");
                #endif
            }

            return NodeStatus.Running;

        } else {
            #if(BT_DEBUG)
            Debug.Log(tag + " Failure");
            #endif
            return NodeStatus.Failure;
        }
    }
}
