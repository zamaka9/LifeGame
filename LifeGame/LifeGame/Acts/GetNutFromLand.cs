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
            nutToGet = owner.Size * 100*level;
            costbase = new Nutrition(owner.Size*10, owner.Size*10, owner.Size)*level;
        }
        public override bool Update()
        {
            Nutrition landnut = Land.GetLandNutrition(owner.Position);
            if (landnut.Sum > nutToGet*2)
            {
                Nutrition predation = landnut.PercentNonNegative(nutToGet);

                Land.SetLandNutrition(owner.Position, landnut - predation);
                owner.Nutrition += predation;
                return true;
            }
            return false;
        }
        //タイマーはActMgrの方で管理

        public int nutToGet;
    }

    
}
