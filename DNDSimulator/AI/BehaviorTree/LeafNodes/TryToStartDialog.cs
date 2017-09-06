using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TryToStartDialog : LeafNode {

    DialogBehavior dialogBehavior;
    AiActor actor;
    List<AiActor> nearbyActors;
    
    public TryToStartDialog(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        dialogBehavior = (DialogBehavior)tree.treeData [BehaviorTreeData.DialogBehavior];
        actor = ((AiActor)tree.treeData [BehaviorTreeData.Actor]);
        nearbyActors = tree.treeData [BehaviorTreeData.NearbyActors] as List<AiActor>;
    }
    
    /// <summary>
    /// Tries to start a conversation with nearby characters
    /// </summary>
    /// <returns>Success once dialog started, Failure if no partners or dialog failed</returns>
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
            if (nearbyDialogBev != null && nearbyDialogBev.IsAvailableForConversation) {
                possiblePartners.Add(nearbyDialogBev);
            }
        }
    
        bool success = StartDialogIfPossible(possiblePartners);
        if (success) {
            #if(BT_DEBUG)
            Debug.Log(tag + " Success");
            #endif
            return NodeStatus.Success;
        }
        #if(BT_DEBUG)
        Debug.Log(tag + " Failure");
        #endif
        return NodeStatus.Failure;
        
    }
    
    private bool StartDialogIfPossible(List<DialogBehavior> dialogPartners) {
        
        if (dialogPartners.Count == 0) {
            return false;
        }
        if (!dialogBehavior.IsAvailableForConversation) {
            return false;
        }
        Dialog newDialog;
        List<DialogBehavior> dialogParticipants = new List<DialogBehavior>();
        dialogParticipants.Add(dialogBehavior);
        dialogParticipants.AddRange(dialogPartners);
        bool success = DialogManager.RequestDialog(actor.Personality.GetFavouriteTopic(), out newDialog, dialogParticipants.ToArray());
        if (success) {
            DialogManager.StartDialog(newDialog);
            #if(BT_DEBUG)
            Debug.Log(tag + " Dialog start succeed");
            #endif
            return true;
        } else {
            DialogManager.EndDialog(newDialog);
            #if(BT_DEBUG)
            Debug.Log(tag + " Dialog start failure");
            #endif
            return false;
        }
    }
    
    
}
