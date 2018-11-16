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
            Left = new Formula(0, 0, 0, 1);
            Right = new Formula(0, 0, 0, 0);
        }

        public override double Update(double coeff)
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
                            t.HP -= (int)coeff;
                            return coeff;
                        }
                    }
                }
            }
            return 0;
        }
    }

    
}
