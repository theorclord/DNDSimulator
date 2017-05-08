using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDSimulator
{
  public class Shortspear : IWeapon
  {
    public int DamageDie
    { get; private set; }

    public bool TwoHanded
    { get; private set; }
    public Shortspear()
    {
      DamageDie = 6;
      TwoHanded = false;
    }
  }
}
