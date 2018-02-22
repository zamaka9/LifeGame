namespace LifeGame.Acts
{
    class Divide:Act
    {
        
        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            cost = owner.Size * 130000 ;
        }
        
        public override bool Update()
        {
            //if (GetRand(Cost) == 0)
            {
                if (owner.Nutrition.Sum > cost)
                {
                    owner.Nutrition = (owner.Nutrition / 2);
                    CreatureMgr.CreateCreature(owner);
                    return true;
                }
            }
            return false;
        }
        int cost;
    }
}
