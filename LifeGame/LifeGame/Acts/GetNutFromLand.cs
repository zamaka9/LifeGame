using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装。すこしずつ真下の地面から栄養を吸収します
    class GetNutFromLand : Act
    {

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            nutToGetRatio = owner.Size *level*0.000005;
            costbase = new Nutrition(owner.Size*2, owner.Size*2, owner.Size)*level;
        }
        public override bool Update()
        {
            Nutrition landnut = Land.GetLandNutrition(owner.Position);
                Nutrition predation = landnut.PercentNonNegative((int)(landnut.Sum*nutToGetRatio));

                Land.SetLandNutrition(owner.Position, landnut - predation);
                owner.Nutrition += predation;
                return true;
            
        }
        //タイマーはActMgrの方で管理

        public double nutToGetRatio;
    }

    
}
