using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public class CharacterController
  {
    private static CharacterController instance;

    private CharacterController() { }

    public static CharacterController Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new CharacterController();
        }
        return instance;
      }
    }

    public int[] GenerateStatRow()
    {
      int[] stats = new int[6];
      for (int i = 0; i < stats.Length; i++)
      {
        int[] diceRolls = new int[4];
        int min = 10;
        for (int j = 0; j < diceRolls.Length; j++)
        {
          diceRolls[j] = GameController.Instance.RandomGenerator.Next(6) + 1;
          if (diceRolls[j] < min)
          {
            min = diceRolls[j];
          }
        }
        stats[i] = diceRolls.Sum() - min;
      }
      stats = stats.OrderByDescending(v => v).ToArray();
      return stats;
    }
  }
}
