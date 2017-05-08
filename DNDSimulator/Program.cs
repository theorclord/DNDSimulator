using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Character> goodGuys = new List<Character>();
      List<Character> badGuys = new List<Character>();
      for(int i = 0; i < 3; i++)
      {
        goodGuys.Add(new Character(CharacterController.Instance.GenerateStatRow(), new Warrior(), 1) { Weapon = new Shortspear(), Name = "Good" + i });
        badGuys.Add(new Character(CharacterController.Instance.GenerateStatRow(), new Warrior(), 1) { Weapon = new Shortspear(), Name = "Bad" + i });
      }

      List<Tuple<int,Character>> initiativeList = new List<Tuple<int, Character>>();
      for(int i = 0; i < badGuys.Count; i++)
      {
        initiativeList.Add(new Tuple<int, Character>(GameController.Instance.RandomGenerator.Next(20) + 1 + badGuys[i].Dexterity / 2 - 5, badGuys[i]));
      }
      for (int i = 0; i < goodGuys.Count; i++)
      {
        initiativeList.Add(new Tuple<int, Character>(GameController.Instance.RandomGenerator.Next(20) + 1 + goodGuys[i].Dexterity / 2 - 5, goodGuys[i]));
      }
      initiativeList = initiativeList.OrderByDescending(s => s.Item1).ToList();

      for(int i = 0; i < initiativeList.Count; i++)
      {
        Console.WriteLine(initiativeList[i].Item1 + " " + initiativeList[i].Item2.Name);
      }

      //int war1Ini = GameController.Instance.RandomGenerator.Next(20) + 1 + war1.Dexterity / 2 - 5;
      //int war2Ini = GameController.Instance.RandomGenerator.Next(20) + 1 + war2.Dexterity / 2 - 5;
      //Character firstCha = war1Ini > war2Ini ? war1 : war2;
      //Character SecondCha = war1Ini > war2Ini ? war2 : war1;
      //Console.WriteLine("First Character " + firstCha.Name + ", Second Character "+ SecondCha.Name);
      while(badGuys.Select(S => S.IsAlive == true).ToList().Count > 0 && goodGuys.Select(S => S.IsAlive == true).ToList().Count > 0)
      {

        //Console.WriteLine(firstCha.Name + ": " + firstCha.CurrentHitpoints);
        //Console.WriteLine(SecondCha.Name + ": " + SecondCha.CurrentHitpoints);
        //firstCha.AttackCharacter(SecondCha);
        //if (SecondCha.IsAlive)
        //{
        //  SecondCha.AttackCharacter(firstCha);
        //}
      }
      Console.ReadKey();
    }
  }
}
