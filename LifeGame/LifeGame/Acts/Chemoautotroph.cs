using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装
    class Chemoautotroph : Act
    {

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            costbase = new Nutrition(0,owner.Size/2, 0) *level;
        }
        public override bool Update()
        {
            if (owner.mgr.Land.GetLandformAt(owner.Position).id == 1)
            {
                Nutrition newNut = new Nutrition(owner.Size * 8, owner.Size, owner.Size*2) * level;
                owner.Nutrition += newNut;
                Console.WriteLine(newNut.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    
}
