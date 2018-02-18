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
        public override void Update()
        {
            ++timer;
            if (timer > 60)
            {
                Nutrition landnut = Land.GetLandNutrition(owner.X, owner.Y);
                //呼び出されるたび、合計で15の栄養を吸収
                int nutToGet = 15;
                if (landnut.Sum < 15)
                {//Sumが15未満の時は、負の値になるのを避けるためSumの値だけ吸収
                    nutToGet = landnut.Sum;
                }
                Nutrition predation = landnut.Percent(nutToGet);
                Land.SetLandNutrition(owner.X, owner.Y, landnut - predation);
                owner.Nutrition += predation;
                timer = 0;
            }
            
        }

        int timer;//毎フレーム吸収するとすごい勢いで栄養枯れるのでとりあえずタイマーで緩和
    }

    
}
