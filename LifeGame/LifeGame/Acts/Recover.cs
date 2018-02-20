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
            this.requirement = new Nutrition(Program.Rand.Next(16), Program.Rand.Next(16), Program.Rand.Next(16));
        }

        public override bool Update()
        {
            if(owner.HP<owner.MaxHP && requirement < owner.Nutrition)
            {
                owner.Nutrition -= requirement;
                owner.HP += requirement.Sum;
                return true;
            }
            return false;
        }
        public Nutrition requirement;
    }
}
