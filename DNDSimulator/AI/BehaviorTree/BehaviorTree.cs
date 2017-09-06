using System.Collections;
using System.Collections.Generic;
namespace DNDSimulator.AI.BehaviorTree
{
  public enum BehaviorTreeData
  {
    Actor,
    DialogBehavior,
    Personality,
    SortedDesires,
    DesireDestination,
    SetNewDestAllowed,
    DesireTimeStart,
    DesireStimulated,
    NearbyActors

  }

  public class BehaviorTree
  {

    public bool debug = false;

    public Character owner;
    public Dictionary<BehaviorTreeData, object> treeData { get; private set; }
    public BehaviorTreeNode root { get; private set; }

    public BehaviorTree(GameObject owner)
    {
      root = new SelectorNode(this, null, "rootSelector");
      this.owner = owner;
      this.treeData = new Dictionary<BehaviorTreeData, object>();
      this.AddDataToTree(BehaviorTreeData.NearbyActors, new List<AiActor>());
      this.AddDataToTree(BehaviorTreeData.DesireStimulated, false);
    }

    public void AddDataToTree(BehaviorTreeData dataType, object data)
    {
      //Checks if data is present
      if (treeData.ContainsKey(dataType))
      {
        treeData[dataType] = data;
      }
      else
      {
        treeData.Add(dataType, data);
      }
    }

    public void AddBehaviors(List<AiBehavior> list)
    {
      // TODO if over all types?????? fuck me but thats the only way i guess
      foreach (AiBehavior behavior in list)
      {
        if (behavior is DialogBehavior)
        {
          AddDataToTree(BehaviorTreeData.DialogBehavior, behavior);
        }
      }
    }

    // run the tree
    public void Tick()
    {
#if BT_DEBUG
        Debug.Log("-------------BehaviorTree Tick-------------");
#endif

      root.Tick();
    }
  }
}
