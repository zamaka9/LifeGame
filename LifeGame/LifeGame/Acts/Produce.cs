using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //GetNutFromLandに役割が移ったので使われていません
    class Produce : Act
    {

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            this.requirement = owner.Size / 2;
        }
        public override bool Update()
        {
            Nutrition landnut = Land.GetLandNutrition(owner.Position);
            Nutrition predation = landnut.Percent(requirement);
            Land.SetLandNutrition(owner.Position, landnut - predation);
            owner.Nutrition += predation;
            return true;
        }
        public int requirement;
    }

    
}
