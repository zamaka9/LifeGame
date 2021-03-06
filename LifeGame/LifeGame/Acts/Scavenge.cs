﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LifeGame.Acts
{
    //仮実装
    class Scavenge : Act
    {
        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            nutToScavenge = owner.Size * 200*level;
            costbase = new Nutrition(owner.Size, owner.Size, owner.Size) * level;
        }

        public override bool Update()
        {
            var target = owner.TargetList;
            if (target != null)
            {
                foreach (var t in target)
                {
                    if (t != owner)
                    {
                        if (t.HP <= 0 )
                        {
                            //栄養を死体から取得する
                            Nutrition predation = t.Nutrition.PercentNonNegative(nutToScavenge);
                            //うち半分は自分が得る
                            owner.Nutrition += predation * efficiency;
                            //残り半分は地面ばらまく。この割合は将来的にはパーツ形状などによって変化
                            Land land = owner.mgr.Land;
                            Nutrition nut = predation * (1 - efficiency) / 9;
                            int i = 0;
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {

                                    //Console.WriteLine(X + x*50 + "," + Y + y * 50);
                                    Vector2D vec = new Vector2D(x * Program.Space_X, y * Program.Space_Y)+owner.Position;
                                    land.SetLandNutrition(vec,land.GetLandNutrition(vec) + nut);
                                    i++;
                                }
                            }
                            t.Nutrition -= predation;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public float efficiency=0.5f;
        public int nutToScavenge;
    }


}
