﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    class Move:Act
    {
        public override void Initialize()
        {
            SpeedLevel = Program.Rand.Next(9);
            MaxSpeed = (Program.Rand.Next(50) + 1) / 10.0f;
            //Speed = rand_normal(1.0f,1.9f);
            Direction = (float)((Program.Rand.Next(359) / 180.0f) * Math.PI);
            Speed = MaxSpeed * SpeedLevel / 10;
        }

        public override void Update()
        {
            //確率で向きと移動速度の変更
            if (Program.Rand.Next(100) == 0)
            {
                if (Program.Rand.Next(1) == 0)
                {
                    SpeedLevel++;
                    if (SpeedLevel > 9)
                    {
                        SpeedLevel = 9;
                    }
                }
                else
                {
                    SpeedLevel--;
                    if (SpeedLevel < 0)
                    {
                        SpeedLevel = 0;
                    }
                }
                Direction = (float)((Program.Rand.Next(359) / 180.0f) * Math.PI);
                Speed = MaxSpeed * SpeedLevel / 10;
            }
            //栄養が足りていれば
            /*if (Itself->GetNut().GetNutSum() > Speed)
            {
                Itself->SetNut(Itself->GetNut() - Itself->GetNut().Percent((unsigned int)Speed));
                //座標の更新
                Itself->SetX(Itself->GetX() + cos(Direction) * Speed);
                Itself->SetY(Itself->GetY() + sin(Direction) * Speed);
            }*/
        }

        float MaxSpeed;//最高移動速度
        float Speed;//現在移動速度
        int SpeedLevel;//移動速度の段階（10段階で0で止まる）
        float Direction;//向き
    }
}