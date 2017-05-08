using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public enum BaseAttackType { Low, Medium, High }
  public interface IClass
  {
    int HitDie { get; }
    BaseAttackType BaseAttack { get; }

  }
}
