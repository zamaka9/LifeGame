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
                float temDistance = 10000;//同エリア内でのカーソルと生物との暫定最短距離
                List<Creature> CList = Land.GetCList(clickPosition);
                foreach(Creature creature in CList.Where(x=>x.Alive))
                {
                    Vector2D creaturePos = clickPosition - creature.Position; 
                    if (temDistance > creaturePos.SquareLength)
                    {
                        temDistance = creaturePos.SquareLength;
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
                    DX.DrawString(Program.Window_X - 180, 48, Object.Nutrition.ToString(), DX.GetColor(255, 255, 255));
                    //所持している機能を数字で表示
                    for(int i = 0; i < Object.ActMgr.ActList.Count; i++)
                    {
                        DX.DrawString(Program.Window_X - 16-(i+1)*16, 60, Object.ActMgr.ActList[i].id.ToString(), DX.GetColor(255, 255, 255));
                    }
                    
                    // ▽カーソルを表示
                    Vector2D position = Drawer.ChangeWtoL(Object.Position);
                    DX.DrawGraph(position.iX - 8, position.iY - Object.Size / 10 - 10, TriCorGH, DX.TRUE);
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
