using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装
    class Attack : Act
    {
        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            this.strength = owner.Size*level;
            costbase = new Nutrition(strength, 100, 0);
            requirementbase = new Nutrition(0, owner.Size * 1000, owner.Size * 1000);
            //basicCostbase = new Nutrition(100, 1000, 100);
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
