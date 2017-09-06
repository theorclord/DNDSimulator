using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Goes through all the sorted desires and sets the DesireDestination in the tree,
/// to the highest desired topic, which is available.
/// </summary>
public class FindAndSetDesireDestination : LeafNode {

    public FindAndSetDesireDestination(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {

    }

    /// <summary>
    /// Goes through all the sorted desires and sets the DesireDestination in the tree,
    /// to the highest desired topic, which is available.
    /// </summary>
    /// <returns>The status of the node. Success if the node finds a desire to fulfill, failure if not</returns>
    public override NodeStatus Tick() {
        // Find Game object from game controller which fulfills the most wanted desire
        List<KeyValuePair<Topic, float>> desireList = (List<KeyValuePair<Topic, float>>)tree.treeData[BehaviorTreeData.SortedDesires];
        List<LocationBehaviour> locations = EnvironmentControl.Instance.LocationList;
        AiActor actor = tree.treeData[BehaviorTreeData.Actor] as AiActor;
        //Skips the destination set if it is forbidden
        if (tree.treeData.ContainsKey(BehaviorTreeData.SetNewDestAllowed) && (bool)tree.treeData[BehaviorTreeData.SetNewDestAllowed] == false) {
            return NodeStatus.Success;
        }

        Topic mon = Database.Instance.GetMoneyTopic();

        foreach (KeyValuePair<Topic,float> pair in desireList) {
            foreach (LocationBehaviour l in locations) {

                // If the character doesn't know the location, he can't go there
                if (!actor.Personality.KnowsTopic(l.SelfTopic)) continue;

                // If the topics of the AI macthes the topic of the desire, set destination and end
                if (l.DesireAffordance == pair.Key) {
                    
                    if (actor.Inventory.ItemCount(mon) >= l.DesireAffordance.value)
                    {
                        tree.AddDataToTree(BehaviorTreeData.DesireDestination, l);
                        tree.AddDataToTree(BehaviorTreeData.SetNewDestAllowed, false);
                        return NodeStatus.Success;
                    } else
                    {
                        foreach(LocationBehaviour loc in locations)
                        {
                            if(loc.DesireAffordance == mon)
                            {
                                tree.AddDataToTree(BehaviorTreeData.DesireDestination, loc);
                                tree.AddDataToTree(BehaviorTreeData.SetNewDestAllowed, false);
                                return NodeStatus.Success;
                            }
                        }
                    }
                }
            }
        }
        //If there is no available place with the desired topics, return failure
        return NodeStatus.Failure;
    }
}
