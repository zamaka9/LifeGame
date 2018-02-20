using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装。登録されないので今のところ無意味
    class Produce : Act
    {

        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            this.requirement = owner.Size / 2;
        }
        public override bool Update()
        {
            Nutrition landnut = Land.GetLandNutrition(owner.X, owner.Y);
            Nutrition predation = landnut.Percent(requirement);
            Land.SetLandNutrition(owner.X, owner.Y, landnut - predation);
            owner.Nutrition += predation;
            return true;
        }
        public int requirement;
    }

    
}
