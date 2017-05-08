using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public class GameController
  {
    private Random ran = new Random();
    public Random RandomGenerator { get { return ran; } }

    private static GameController instance;

    private GameController() { }

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
  }
}
