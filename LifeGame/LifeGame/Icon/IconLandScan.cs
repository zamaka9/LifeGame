using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class IconLandScan : Icon
    {
        public override void Initialize()
        {
            targetNutrition = null;
            GraphicHandle = DX.LoadGraph("Data/スキャンアイコン.png");
        }

        public override void Update()
        {
            if (ClickFlag == true)
            {
                int targetX = clickX;
                int targetY = clickY;
                Drawer.ChangeLtoW(ref targetX, ref targetY);
                TargetLand = Land.GetLandNutrition(targetX, targetY);

                ClickFlag = false;
            }
        }

        public override void Draw()
        {
            if (targetNutrition != null)
            {
                // 選択中のタイルを赤枠で表示(仮)
                int fX = targetX;
                int fY = targetY;
                fX = (fX / Program.Space_Size);
                fY = (fY / Program.Space_Size);
                int x1 = fX * Program.Space_Size;
                int y1 = fY * Program.Space_Size;
                int x2 = (fX + 1) * Program.Space_Size;
                int y2 = fY * Program.Space_Size;
                int x3 = fX * Program.Space_Size;
                int y3 = (fY + 1) * Program.Space_Size;
                int x4 = (fX + 1) * Program.Space_Size;
                int y4 = (fY + 1) * Program.Space_Size;
                Drawer.ChangeWtoL(ref x1, ref y1);
                Drawer.ChangeWtoL(ref x2, ref y2);
                Drawer.ChangeWtoL(ref x3, ref y3);
                Drawer.ChangeWtoL(ref x4, ref y4);
                DX.DrawLine(x1, y1, x2, y2, DX.GetColor(255, 100, 100));
                DX.DrawLine(x2, y2, x4, y4, DX.GetColor(255, 100, 100));
                DX.DrawLine(x4, y4, x3, y3, DX.GetColor(255, 100, 100));
                DX.DrawLine(x3, y3, x1, y1, DX.GetColor(255, 100, 100));

                // 栄養を表示
                DX.DrawString(Program.Window_X - 128, 96, Object.Nutrition.Red.ToString(), DX.GetColor(255, 100, 100));
                DX.DrawString(Program.Window_X - 128, 102, Object.Nutrition.Green.ToString(), DX.GetColor(100, 255, 100));
                DX.DrawString(Program.Window_X - 128, 118, Object.Nutrition.Blue.ToString(), DX.GetColor(100, 100, 255));
            }
        }

        // ターゲット中の土地の栄養
        Nutrition targetNutrition;
        // ターゲット中のワールド座標
        int targetX;
        int targetY;
    }
}
