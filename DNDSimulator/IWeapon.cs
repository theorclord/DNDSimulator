using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public interface IWeapon
  {
    int DamageDie { get; }
    bool TwoHanded { get; }
    //int MainCharateristic { get; }
  }
}
