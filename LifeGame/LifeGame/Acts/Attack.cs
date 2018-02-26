using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装。登録されないので今のところ無意味
    class Attack : Act
    {
        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            this.strength = owner.Size*1;
            cost = new Nutrition(strength, 100, 0);
            basicCost = new Nutrition(100, 1000, 100);
        }

        public override bool Update()
        {
            var target = owner.TargetList;
            if (target != null)
            {
                foreach (var t in target)
                {
                    if (t != owner && t.Alive)
                    {
                        if (owner.Size - t.Size>10)
                        {
                            t.HP -= strength;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public int strength;
    }

    
}
