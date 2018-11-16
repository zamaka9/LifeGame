using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    class Stabilize : Act
    {
        //自身を固定します
        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
        }

        public override bool Update()
        {
            int l= lastNutSum;
            lastNutSum = owner.Nutrition.Sum;
            if (owner.Nutrition.Sum > l)
            {
                owner.isStable = true;
                //owner.VelocityStream = new Vector2D();
                return true;
            }
            owner.isStable = false;
            return false;
        }
        int lastNutSum=0;
    }
}
