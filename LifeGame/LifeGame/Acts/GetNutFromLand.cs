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
            Nutrition landnut = Land.GetLandNutrition(owner.Position);
            //呼び出されるたび、合計で15の栄養を吸収
            int nutToGet = 900000;
            if (landnut.Sum < 900000)
            {//Sumが15未満の時は、負の値になるのを避けるためSumの値だけ吸収
                nutToGet = landnut.Sum;
            }
            Nutrition predation = landnut.Percent(nutToGet);
            Land.SetLandNutrition(owner.Position, landnut - predation);
            owner.Nutrition += predation;
            return true;
        }
        //タイマーはActMgrの方で管理
    }

    
}
