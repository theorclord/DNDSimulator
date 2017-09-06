using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindNearbyActors : LeafNode {

    private float searchRadius;
    private LayerMask searchLayer;
    AiActor actor;
    List<AiActor> nearbyActors;
    
    public FindNearbyActors(float searchRadius, LayerMask searchLayer, BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        this.searchRadius = searchRadius;
        this.searchLayer = searchLayer;
        actor = ((AiActor)tree.treeData [BehaviorTreeData.Actor]) as AiActor;
        nearbyActors = tree.treeData [BehaviorTreeData.NearbyActors] as List<AiActor>;
    }
    
    /// <summary>
    /// Searches for nearby characters
    /// </summary>
    /// <returns>Success once characters found, failure if not</returns>
    public override NodeStatus Tick() {
        // Remove actors from the list that are out of the "loose" range. This is a bit larger than the "find" range, to not instantly loose found actors again the next frame
        RemoveTooFarActors();
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(tree.owner.transform.position, searchRadius);
        foreach (Collider2D col in nearbyColliders) {
            if (col.gameObject.GetComponent<AiActor>() == actor) {
                continue;
            }
            AiActor otherActor = col.gameObject.GetComponent<AiActor>();
            if (otherActor != null && !nearbyActors.Contains(otherActor)) {
                // check if they can see each other
                if (VisibilityUtil.CheckLineOfSight2D(tree.owner.transform, col.transform, ~actor.gameObject.layer)) {
                    nearbyActors.Add(otherActor);
                }
            }
        }
        if (nearbyActors.Count > 0) {
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

    void RemoveTooFarActors() {
        AiActor[] searchList = new AiActor[nearbyActors.Count];
        nearbyActors.CopyTo(searchList);
        foreach (AiActor nearbyActor in searchList) {
            if (Vector3.Distance(nearbyActor.transform.position, actor.transform.position) > searchRadius + 2) {
                nearbyActors.Remove(nearbyActor);
            }
        }
    }
}

