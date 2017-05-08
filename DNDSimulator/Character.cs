using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public class Character
  {
    public string Name { get; set; }
    public int Level { get; set; }
    // Statistics
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }

    // class
    public IClass CharacterClass { get; set; }
    // Calculated fields
    public int MaximumHitpoints { get; private set; }
    public int CurrentHitpoints { get; set; }
    public int BaseAttackBonus
    {
      get
      {
        switch (CharacterClass.BaseAttack)
        {
          case BaseAttackType.Low:
            return Level / 2;
          case BaseAttackType.High:
            return Level;
          case BaseAttackType.Medium:
            return (Level * 3) / 4;
        }
        return -1;
      }
    }
    public int ArmorClass { get { return 10 + (Dexterity / 2 - 5); } }
    public IWeapon Weapon { get; set; }
    public bool IsAlive { get { return CurrentHitpoints > 0; } }

    public Character(int[] stats, IClass chaClass, int level)
    {
      if(stats.Length == 6)
      {
        Strength = stats[0];
        Dexterity = stats[1];
        Constitution = stats[2];
        Intelligence = stats[3];
        Wisdom = stats[4];
        Charisma = stats[5];
      }
      CharacterClass = chaClass;
      Level = level;

      // Calc fields
      MaximumHitpoints = CharacterClass.HitDie + (Constitution / 2 - 5);
      for (int i = 0; i < Level; i++)
      {
        MaximumHitpoints += GameController.Instance.RandomGenerator.Next(CharacterClass.HitDie) + 1 +(Constitution / 2 - 5);
      }
      CurrentHitpoints = MaximumHitpoints;
    }

    /// <summary>
    /// Simple attack function. Assumes close combat and strength based weapon.
    /// Should be moved
    /// </summary>
    /// <param name="cha"></param>
    public void AttackCharacter(Character cha)
    {
      int toHit = GameController.Instance.RandomGenerator.Next(20) + 1 + BaseAttackBonus + (Strength / 2 - 5);
      if(toHit >= cha.ArmorClass)
      {
        int damageDealt = GameController.Instance.RandomGenerator.Next(Weapon.DamageDie) + 1 + (Strength / 2 - 5);
        Console.WriteLine(Name + ", dealt damage to " + cha.Name + ", " +damageDealt);
        cha.CurrentHitpoints -= damageDealt;
      }
    }
  }
}
