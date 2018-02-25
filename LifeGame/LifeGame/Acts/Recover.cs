using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装。登録されないので今のところ無意味
    class Recover : Act
    {
        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            cost = new Nutrition(Program.Rand.Next(16)*100, Program.Rand.Next(16)*100, Program.Rand.Next(16)*100);
        }

        public override bool Update()
        {
            if(owner.HP<owner.MaxHP)
            {
                owner.HP += cost.Sum/100;
                return true;
            }
            return false;
        }
    }
}
