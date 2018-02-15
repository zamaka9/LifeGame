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
            this.strength = owner.Size;
        }

        public override void Update()
        {
            var target = owner.TargetList;
            if (target != null)
            {
                foreach (var t in target)
                {
                    if (t != owner)
                    {
                        if (strength - t.Size>10)
                        {
                            t.HP -= strength;
                        }
                    }
                }
            }
        }
        public int strength;
    }

    
}
