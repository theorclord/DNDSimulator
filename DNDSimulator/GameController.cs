using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNDSimulator.Utility;

namespace DNDSimulator
{
  public class GameController
  {
    private static GameController instance;
    private Random ran = new Random();

    private bool initiaveRolled;

    public Random RandomGenerator { get { return ran; } }
    public Dictionary<string, List<Character>> BattleGroups { get; set; }
    public Character[,] BattleGrid { get; set; }
    
    public PriorityQueueMin<Character> InitiativeQue { get; set; }

    private GameController()
    {
      BattleGroups = new Dictionary<string, List<Character>>();
      InitiativeQue = new PriorityQueueMin<Character>(2);
    }

    public static GameController Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new GameController();
        }
        return instance;
      }
    }

    public bool SetBattleFieldSize(int size)
    {
      if(BattleGrid != null)
      {
        BattleGrid = new Character[size, size];
        return true;
      }
      return false;
    }

    public void AddGroup(string groupName, List<Character> group)
    {
      BattleGroups.Add(groupName, group);
    }

    public void SimulateTurn()
    {
      if (!initiaveRolled)
      {
        foreach(KeyValuePair<string,List<Character>> pair in BattleGroups)
        {
          foreach(Character charac in pair.Value)
          {
            charac.Initiative = ran.Next(20) + 1+ charac.Dexterity / 2 - 5;
          }
          InitiativeQue.insertRange(pair.Value);
        }
        initiaveRolled = true;
      }
      foreach(Character charac in InitiativeQue)
      {
        Console.WriteLine("Character initiative " + charac.Initiative);
        // DO AI LOGIC
      }
    }
  }
}
