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

        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
        }
        public override bool Update()
        {
            Nutrition landnut = Land.GetLandNutrition(owner.X, owner.Y);
            //呼び出されるたび、合計で15の栄養を吸収
            int nutToGet = 100000;
            Nutrition predation = landnut.PercentNonNegative(nutToGet);
            Land.SetLandNutrition(owner.X, owner.Y, landnut - predation);
            owner.Nutrition += predation;
            return true;
        }
        //タイマーはActMgrの方で管理
    }

    
}
