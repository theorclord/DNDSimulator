using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Reduces the desire of the actor based on location.
/// TODO Should perhaps be more general and based on set topic, rather than a destination
/// </summary>
public class ReduceDesireLocation : LeafNode {

    private int testcount;

    public ReduceDesireLocation(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {

    }

    public override NodeStatus Tick() {
        Personality personal = tree.treeData[BehaviorTreeData.Personality] as Personality;
        LocationBehaviour target = tree.treeData[BehaviorTreeData.DesireDestination] as LocationBehaviour;
        // Temporary boolean needed to ensure the time is not reset
        bool temp = true;
        // Checks if there already is a timer in the dictionary and if a desire currently is being stimulated
        if (tree.treeData.ContainsKey(BehaviorTreeData.DesireTimeStart) && (bool)tree.treeData[BehaviorTreeData.DesireStimulated]) {

            // Calculates the time difference to check if 5 seconds have passed
            //TODO set the time constraint somewhere managable
            float timediff = Time.time - (float)tree.treeData[BehaviorTreeData.DesireTimeStart];

            if (timediff > 5f) {
                //Allows the AI to set a new destination
                tree.treeData[BehaviorTreeData.SetNewDestAllowed] = true;
                //Finds the current desire which is being stimulated and sets it to false
                Topic tempKey = null;
                foreach (KeyValuePair<Topic, bool> pair in personal.StimulatedDesires) {
                    if (!personal.IsDesireStimulated(pair.Key)) continue;

                    if (pair.Key.Equals(target.DesireAffordance)) {
                        tempKey = pair.Key;
                    }
                    //Change inventory according to desire stimulated
                    AiActor actor = tree.treeData[BehaviorTreeData.Actor] as AiActor;
                    if (tempKey.Equals(Database.Instance.GetMoneyTopic()) && actor.Inventory.GetAvailableItems().Count >0 )
                    {
                        actor.Inventory.RemoveItem(actor.Inventory.GetAvailableItems()[Random.Range(0, actor.Inventory.GetAvailableItems().Count)]);
                    }
                    actor.Inventory.AddItem(tempKey);
                    GUIManager.DisplayInfoWithLockDelay(actor, GUIInfoBox.InfoType.Trade, -1, tempKey, 2.5f);

                    if(tempKey.value > 0)
                    {
                        actor.Inventory.RemoveItem(Database.Instance.GetMoneyTopic(),tempKey.value);
                    }
                    
                    tree.AddDataToTree(BehaviorTreeData.DesireStimulated, false);
                    personal.SetStimulateDesire(tempKey, false);
                    //Reset the tree timer
                    tree.AddDataToTree(BehaviorTreeData.DesireTimeStart, Time.time);
                    return NodeStatus.Success;
                }
            } else {
                // Sets the temp value to false, such that the timer may increase
                temp = false;
            }
        } 
        //Goes through the desires and sets the stimulated desire
        foreach (Topic desire in personal.GetDesiredTopics()) {

            if (desire.Equals(target.DesireAffordance)) {

                personal.SetStimulateDesire(desire, true);
                if (temp) {
                    tree.AddDataToTree(BehaviorTreeData.DesireTimeStart, Time.time);
                }

                tree.AddDataToTree(BehaviorTreeData.DesireStimulated, true);

                return NodeStatus.Success;
            }
        }
        return NodeStatus.Running;
    }
}
