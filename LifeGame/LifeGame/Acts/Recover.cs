using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装
    class Recover : Act
    {
        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            costbase = new Nutrition(Program.Rand.Next(16)*100, Program.Rand.Next(16)*100, Program.Rand.Next(16)*100)*level;
        }

        public override bool Update()
        {
            if(owner.HP<owner.MaxHP)
            {
                owner.HP += cost.Sum/40;
                return true;
            }
            return false;
        }
    }
}
