using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    class Divide:Act
    {
        
        public override void Initialize(Creature owner)
        {
            cost = owner.Size * 2 ;
            this.owner = owner;
        }
        
        public override void Update()
        {
            //if (GetRand(Cost) == 0)
            {
                if (owner.Nutrition.Sum > cost)
                {
                    owner.Nutrition = (owner.Nutrition / 2);
                    CreatureMgr.CreateCreature(owner);
                }
            }
        }
        Creature owner;
        int cost;
    }
}
