using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class God
    {
        public void Initialize(Land land, Drawer drawer)
        {
            icons = new Icon[3];
            Drawer = drawer;
            Icon.Drawer = drawer;
            Icon.Land = land;

            //各アイコンクラスの初期化
            icons[0] = new IconScan();
            icons[1] = new IconThunderbolt();
            icons[2] = new IconLandScan();
            
            foreach(Icon icon in icons){
                icon.Initialize();
            }

            //仮　アイコン用グラフィックの読み込み
            angelGH = DX.LoadGraph("Data/テンシ大.png");

            //初期選択アイコンを設定
            leftClick = 0;
            rightClick = 1;
        }

        public void Update()
        {
            int mouseX;
            int mouseY;
            //マウスのいずれかのクリックがされたら
            if (DX.GetMouseInputLog(out Mouse_Button, out mouseX, out mouseY, DX.TRUE) == 0)
            {
                mouse = new Vector2D(mouseX, mouseY);
                //アイコンの変更の判定
                if (mouse.iY >= Program.Window_Y - 64 && mouse.iY < Program.Window_Y&&
                    mouse.iX >= Program.Window_X - 128 - 64 * icons.Count()&&mouse.iX <= Program.Window_X - 128)
                {
                    int n = (mouse.iX - (Program.Window_X - 128 - 64 * icons.Count())) / 64;
                    if (Mouse_Button == DX.MOUSE_INPUT_LEFT)
                    {
                        leftClick = n;
                    }
                    if (Mouse_Button == DX.MOUSE_INPUT_RIGHT)
                    {
                        rightClick = n;
                    }
                }

                //各ボタン対応アイコンクラスに座標とクリックされたという情報をセット
                mouse = Drawer.ChangeLtoW(mouse);
                if (Mouse_Button == DX.MOUSE_INPUT_LEFT)
                {
                    icons[leftClick].React(mouse);
                }
                if (Mouse_Button == DX.MOUSE_INPUT_RIGHT)
                {
                    icons[rightClick].React(mouse);
                }
            }

            //各アイコンクラスの更新
            foreach(Icon icon in icons)
            {
                icon.Update();
            }
        }

        public void Draw()
        {
            // 選択中のタイルを色付きで表示(仮)
            DX.GetMousePoint(out int fX, out int fY);
            Vector2D f = new Vector2D(fX, fY);
            f = Drawer.ChangeLtoW(f);
            f /= Program.Space_Size;
            Vector2D vec1 = new Vector2D(f.iX, f.iY) * Program.Space_Size;
            Vector2D vec2 = new Vector2D(f.iX + 1, f.iY) * Program.Space_Size;
            Vector2D vec3 = new Vector2D(f.iX, f.iY + 1) * Program.Space_Size;
            Vector2D vec4 = new Vector2D(f.iX + 1, f.iY + 1) * Program.Space_Size;
            vec1 = Drawer.ChangeWtoL(vec1);
            vec2 = Drawer.ChangeWtoL(vec2);
            vec3 = Drawer.ChangeWtoL(vec3);
            vec4 = Drawer.ChangeWtoL(vec4);
            DX.DrawLine(vec1.iX, vec1.iY, vec2.iX, vec2.iY, DX.GetColor(255, 255, 255));
            DX.DrawLine(vec2.iX, vec2.iY, vec4.iX, vec4.iY, DX.GetColor(255, 255, 255));
            DX.DrawLine(vec4.iX, vec4.iY, vec3.iX, vec3.iY, DX.GetColor(255, 255, 255));
            DX.DrawLine(vec3.iX, vec3.iY, vec1.iX, vec1.iY, DX.GetColor(255, 255, 255));


	
            //各アイコンクラスの描画
            foreach(Icon icon in icons)
            {
                icon.Draw();
            }

            //アイコンのグラフィックの表示
            foreach(var icon in icons.Select((v, i) => new{ v, i }))
            {
                DX.DrawGraph(Program.Window_X - 128 - (Icon.IconSize * (icons.Count() - icon.i)), Program.Window_Y - Icon.IconSize, icon.v.GraphicHandle, DX.TRUE);
            }
            DX.DrawExtendGraph(Program.Window_X - 128, Program.Window_Y - 128, Program.Window_X, Program.Window_Y, angelGH, DX.TRUE);

            DX.DrawString(Program.Window_X - 128 - (Icon.IconSize * (icons.Count() - leftClick)), Program.Window_Y - Icon.IconSize, "L", DX.GetColor(255, 255, 255));
            DX.DrawString(Program.Window_X - 128 - (Icon.IconSize * (icons.Count() - rightClick)), Program.Window_Y - Icon.IconSize, "R", DX.GetColor(255, 255, 255));
        }

        enum IconIndexT
        {
            Scan,
            Thunder,
            Save
        }

        const int iconSize = 64;

        int leftClick;
        int rightClick;

        Drawer Drawer;

        Vector2D mouse = new Vector2D();
        int Mouse_Button;

        Icon[] icons;  
        
        int angelGH;
    }
}
