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
            GraphicHandle = DX.LoadGraph("Data/土地スキャンアイコン.png");
        }

        public override void Update()
        {
            if (ClickFlag == true)
            {
                targetPos = clickPosition;
                targetNutrition = Land.GetLandNutrition(targetPos);

                ClickFlag = false;
            }
        }

        public override void Draw()
        {
            if (targetNutrition != null)
            {
                // 選択中のタイルを赤枠で表示(仮)
                Vector2D f = (Vector2D)targetPos.Clone();
                f /= Program.Space_Size;
                Vector2D vec1 = new Vector2D(f.iX, f.iY) * Program.Space_Size;
                Vector2D vec2 = new Vector2D(f.iX + 1, f.iY) * Program.Space_Size;
                Vector2D vec3 = new Vector2D(f.iX, f.iY + 1) * Program.Space_Size;
                Vector2D vec4 = new Vector2D(f.iX + 1, f.iY + 1) * Program.Space_Size;
                vec1 = Drawer.ChangeWtoL(vec1);
                vec2 = Drawer.ChangeWtoL(vec2);
                vec3 = Drawer.ChangeWtoL(vec3);
                vec4 = Drawer.ChangeWtoL(vec4);
                DX.DrawLine(vec1.iX, vec1.iY, vec2.iX, vec2.iY, DX.GetColor(255, 100, 100));
                DX.DrawLine(vec2.iX, vec2.iY, vec4.iX, vec4.iY, DX.GetColor(255, 100, 100));
                DX.DrawLine(vec4.iX, vec4.iY, vec3.iX, vec3.iY, DX.GetColor(255, 100, 100));
                DX.DrawLine(vec3.iX, vec3.iY, vec1.iX, vec1.iY, DX.GetColor(255, 100, 100));

                // 栄養を表示
                DX.DrawString(Program.Window_X - 128, 76, targetPos.X.ToString() + " " + targetPos.Y.ToString(), DX.GetColor(255, 255, 255));
                DX.DrawString(Program.Window_X - 128, 96, targetNutrition.Red.ToString() + ":" + (targetNutrition.Red >> 16).ToString(), DX.GetColor(255, 100, 100));
                DX.DrawString(Program.Window_X - 128, 112, targetNutrition.Green.ToString() + ":" + (targetNutrition.Green >> 16).ToString(), DX.GetColor(100, 255, 100));
                DX.DrawString(Program.Window_X - 128, 128, targetNutrition.Blue.ToString() + ":" + (targetNutrition.Blue >> 16).ToString(), DX.GetColor(100, 100, 255));
            }
        }

        // ターゲット中の土地の栄養
        Nutrition targetNutrition;
        // ターゲット中のワールド座標
        Vector2D targetPos;
    }
}
