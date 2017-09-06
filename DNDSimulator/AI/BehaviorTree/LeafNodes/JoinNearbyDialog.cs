using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoinNearbyDialog : LeafNode {

    private static readonly float JOIN_DIALOG_EPSILON = 0.3f;

    private float searchRadius;
    private LayerMask searchLayer;
    DialogBehavior dialogBehavior;
    AiActor actor;
    List<AiActor> nearbyActors;
    
    public JoinNearbyDialog(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        dialogBehavior = (DialogBehavior)tree.treeData [BehaviorTreeData.DialogBehavior];
        actor = ((AiActor)tree.treeData [BehaviorTreeData.Actor]);
        nearbyActors = tree.treeData [BehaviorTreeData.NearbyActors] as List<AiActor>;
    }
    
    /// <summary>
    /// If a nearby character is in a conversation with an interresting topic, this character will join
    /// </summary>
    /// <returns>Success once a dialog was joined, Failure if not</returns>
    public override NodeStatus Tick() {
    
        if(!dialogBehavior.IsAvailableForConversation){
            #if(BT_DEBUG)
            Debug.Log(tag + " Failure");
            #endif
            return NodeStatus.Failure;
        }
    
        List<DialogBehavior> possiblePartners = new List<DialogBehavior>();
        foreach (AiActor nearbyActor in nearbyActors) {
            DialogBehavior nearbyDialogBev = nearbyActor.gameObject.GetComponent<DialogBehavior>();
            Dialog nearbyDialog = nearbyDialogBev.CurrentDialog;
            if (nearbyDialog is PlayerDialog) {
                continue;
            }

            if (nearbyDialogBev != null && nearbyDialogBev.IsEngagedInDialog && !nearbyDialog.IsFull) {
                if (actor.Personality.IsInterestedIn(nearbyDialog.Topic)) {
                    // TODO This does two things, move into position to join dialog and then join, it should be in two different nodes
                    float distToDialog = Vector2.Distance(actor.transform.position, nearbyDialog.Position.NextOpenPosition);
                    if(distToDialog > JOIN_DIALOG_EPSILON) {
                        actor.SetTarget(nearbyDialog.Position.NextOpenPosition);
                        #if(BT_DEBUG)
                        Debug.Log(tag + " Success");
                        #endif
                        return NodeStatus.Success;
                    } else if (TryToJoinConversation(nearbyDialogBev)) {
                        #if(BT_DEBUG)
                        Debug.Log(tag + " Success");
                        #endif
                        return NodeStatus.Success;
                    }
                }
            }
        }
        #if(BT_DEBUG)
        Debug.Log(tag + " Failure");
        #endif
        return NodeStatus.Failure;
    }

    bool TryToJoinConversation(DialogBehavior nearbyDialogBev) {
        return DialogManager.JoinParticipantToDialog(nearbyDialogBev.CurrentDialog, dialogBehavior);
    }
}

