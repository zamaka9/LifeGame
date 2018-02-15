namespace LifeGame.Acts
{
    class Divide:Act
    {
        
        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            cost = owner.Size * 2 ;
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
        int cost;
    }
}
