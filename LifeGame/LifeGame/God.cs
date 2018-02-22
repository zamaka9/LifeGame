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
            //マウスのいずれかのクリックがされたら
            if (DX.GetMouseInputLog(out Mouse_Button, out Mouse_X, out Mouse_Y, DX.TRUE) == 0)
            {
                //アイコンの変更の判定
                if (Mouse_Y >= Program.Window_Y - 64 && Mouse_Y < Program.Window_Y&&
                    Mouse_X >= Program.Window_X - 128 - 64 * icons.Count()&&Mouse_X <= Program.Window_X - 128)
                {
                    int n = (int)(Mouse_X - (Program.Window_X - 128 - 64 * icons.Count())) / 64;
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
                Drawer.ChangeLtoW(ref Mouse_X, ref Mouse_Y);
                if (Mouse_Button == DX.MOUSE_INPUT_LEFT)
                {
                    icons[leftClick].React(Mouse_X, Mouse_Y);
                }
                if (Mouse_Button == DX.MOUSE_INPUT_RIGHT)
                {
                    icons[rightClick].React(Mouse_X, Mouse_Y);
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
            int fX = 0;
            int fY = 0;
            DX.GetMousePoint(out fX, out fY);
            Drawer.ChangeLtoW(ref fX, ref fY);
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
            DX.DrawLine(x1,y1, x2, y2, DX.GetColor(255, 255, 255));
            DX.DrawLine(x2,y2, x4, y4, DX.GetColor(255, 255, 255));
            DX.DrawLine(x4,y4, x3, y3, DX.GetColor(255, 255, 255));
            DX.DrawLine(x3,y3, x1, y1, DX.GetColor(255, 255, 255));


	
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

        int Mouse_X;
        int Mouse_Y;
        int Mouse_Button;

        Icon[] icons;  
        
        int angelGH;
    }
}
