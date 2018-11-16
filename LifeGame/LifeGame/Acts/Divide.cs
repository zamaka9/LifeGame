using System;

namespace LifeGame.Acts
{
    class Divide : Act
    {

        
        public override void Initialize(Creature owner,int level)
        {
            base.Initialize(owner,level);
            costbase = new Nutrition(owner.Size * 1200, owner.Size * 1200, 1000);
            requirement = costbase * 3;
            Left = new Formula(0, 0, 0, 12);
            Right = new Formula(0, 0, 0, 0);
        }

        public override double Update(double coeff)
        {
            //if (GetRand(Cost) == 0)

            //Console.WriteLine(owner.Nutrition.ToString()+","+cost.ToString());
            if (owner.Nutrition > requirement)
            {
                owner.Nutrition = (owner.Nutrition / 2);
                owner.Energy = owner.Energy / 2;
                CreatureMgr.CreateCreature(owner);
                return coeff;
            }
            else
            {
                return 0;
            }

        }
        public Nutrition requirement;
    }
}
