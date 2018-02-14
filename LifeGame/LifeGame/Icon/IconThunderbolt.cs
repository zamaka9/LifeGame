using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class IconThunderbolt : Icon
    {
        public override void Initialize()
        {
            GH = new int[AnimeNumber];
            DX.LoadDivGraph("Data/Thunderbolt.png", AnimeNumber, 4, 3, 64, 64, GH);
        }

        public override void Update()
        {
            if (ClickFlag == true)
            {
                Thunderbolt_X = ;
                List<Creature> CList = Land.GetCList(clickX, clickY);
                foreach (Creature creature in CList)
                {
                    creature.HP = 0;
                }
                ClickFlag = false;
            }
        }

        public override void Draw()
        {
            if (ClickFlag == true)
            {
                DX.DrawBillboard3D(VGet(Thunderbolt_X, 0.0f, Thunderbolt_Y), 0.5f, 0.0f, 100.0f, 0.0f, GH[(TimeCount / AnimeTime)], TRUE);
            }
        }

        const int AnimeNumber = 12;//アニメ枚数
        const int AnimeTime = 1;//雷アニメ切り替えフレーム
        int[] GH;//雷画像グラフィックハンドル
        float Thunderbolt_X;
        float Thunderbolt_Y;
        int TimeCount;//雷を表示してからの経過フレーム
    }
}
