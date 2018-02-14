using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    class Divide:Act
    {
        
        public Divide(Creature owner)
        {
            this.owner = owner;
            this.mgr = owner.mgr;
        } 
        public override void Initialize()
        {
            cost = owner.Size * 2 ;
        }
        
        public override void Update()
        {
            //if (GetRand(Cost) == 0)
            {
                if (owner.Nutrition.Sum > cost)
                {
                    owner.Nutrition = (owner.Nutrition / 2);
                    mgr.CreateCreature(owner);
                }
            }
        }
        CreatureMgr mgr;
        Creature owner;
        int cost;
    }
}
