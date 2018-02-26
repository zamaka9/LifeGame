using System;

namespace LifeGame.Acts
{
    class Divide : Act
    {

        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            cost = new Nutrition(owner.Size * 1200, owner.Size * 2000, 1000);
        }

        public override bool Update()
        {
            //if (GetRand(Cost) == 0)
            if (!(owner.Nutrition > (cost * 3)))
            {
                return false;
            }
            //Console.WriteLine(owner.Nutrition.ToString()+","+cost.ToString());
            owner.Nutrition = (owner.Nutrition / 2);
            CreatureMgr.CreateCreature(owner);
            return true;

        }

    }
}
