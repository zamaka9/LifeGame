using System;

namespace LifeGame.Acts
{
    class Divide : Act
    {

        
        public override void Initialize(Creature owner,int level)
        {
            base.Initialize(owner,level);
            costbase = new Nutrition(owner.Size * 300, owner.Size * 300, 1000);
            requirementbase = costbase * 3;
        }

        public override bool Update()
        {
            //if (GetRand(Cost) == 0)
            
            //Console.WriteLine(owner.Nutrition.ToString()+","+cost.ToString());
            owner.Nutrition = (owner.Nutrition / 2);
            CreatureMgr.CreateCreature(owner);
            return true;

        }

    }
}
