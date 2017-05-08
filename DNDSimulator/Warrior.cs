using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public class Warrior : IClass
  {
    public BaseAttackType BaseAttack
    { get; private set; }
    public int HitDie
    { get; private set; }
    public Warrior()
    {
      BaseAttack = BaseAttackType.High;
      HitDie = 8;
    }
  }
}
