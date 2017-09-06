using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Loops through all desires in the Personality and sorts them from most wanted, to least wanted and adds them to the tree data.
/// </summary>
public class SetAndSortDesires : LeafNode {

    private Personality person;
    
    public SetAndSortDesires(BehaviorTree tree, BehaviorTreeNode parent, string tag) : base(tree, parent, tag) {
        person = (Personality) tree.treeData[BehaviorTreeData.Personality];
    }

    public override NodeStatus Tick()
    {
        // Loops through all desires and sets a list of the persons desires in order of need
        // TODO change distance parameter
        List<KeyValuePair<Topic,float>> desireList = person.GetDesireKeyValuePairs();

        //Uses insertion sort to sort the list of desires.
        for(int i = 1; i<desireList.Count; i++)
        {
            int j = i;
            while(j>0 && desireList[j-1].Value < desireList[j].Value)
            {
                var temp = desireList[j];
                desireList[j] = desireList[j - 1];
                desireList[j - 1] = temp;
                j--;
            }
        }

        tree.AddDataToTree(BehaviorTreeData.SortedDesires, desireList );
        return NodeStatus.Success;
    }
}
