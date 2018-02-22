using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class IconScan : Icon
    {
        public override void Initialize()
        {
            Object = null;
            TriCorGH = DX.LoadGraph("Data/三角カーソル.png");// 画像データ（作って）あるけど（リポジトリ内に）ない
            GraphicHandle = DX.LoadGraph("Data/スキャンアイコン.png");
        }

        public override void Update()
        {
            if (ClickFlag == true)
            {
                int temDistance = 10000;//同エリア内でのカーソルと生物との暫定最短距離
                List<Creature> CList = Land.GetCList(clickX, clickY);
                foreach(Creature creature in CList)
                {
                    int X = clickX - (int)creature.X;
                    int Y = clickY - (int)creature.Y;
                    if (temDistance > X * X + Y * Y)
                    {
                        temDistance = X * X + Y * Y;
                        Object = creature;
                    }
                }
                ClickFlag = false;
            }
        }

        public override void Draw()
        {
            if (Object != null)
            {
                if (Object.Existence == true)
                {
                    DX.DrawString(Program.Window_X - 128, 16, Object.HP.ToString() + '/' + Object.MaxHP.ToString(), DX.GetColor(255, 255, 255));
                    DX.DrawString(Program.Window_X - 128, 32, Object.Size.ToString(), DX.GetColor(255, 255, 255));
                    DX.DrawString(Program.Window_X - 128, 48, Object.Nutrition.Sum.ToString(), DX.GetColor(255, 255, 255));

                    // ▽カーソルを表示
                    int x = (int)Object.X;
                    int y = (int)Object.Y;
                    Drawer.ChangeWtoL(ref x, ref y);
                    DX.DrawGraph(x - 8, y - Object.Size / 10 - 10, TriCorGH, DX.TRUE);
                }
                else
                {
                    Object = null;
                }
            }
        }

        // スキャン対象の生物
        Creature Object;

        // ▽カーソルのグラフィックハンドル
        int TriCorGH;
    }
}
