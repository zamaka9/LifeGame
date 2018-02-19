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
            icons = new Icon[2];
            Drawer = drawer;
            Icon.Drawer = drawer;
            Icon.Land = land;

            //各アイコンクラスの初期化
            icons[0] = new IconScan();
            icons[1] = new IconThunderbolt();
            
            foreach(Icon icon in icons){
                icon.Initialize();
            }

            //仮　アイコン用グラフィックの読み込み
            angelGH = DX.LoadGraph("Data/テンシ大.png");

            //初期選択アイコンを設定
            LeftClick = icons[0];
            RightClick = icons[1];
        }

        public void Update()
        {
            //マウスのいずれかのクリックがされたら
            if (DX.GetMouseInputLog(out Mouse_Button, out Mouse_X, out Mouse_Y, DX.TRUE) == 0)
            {
                //アイコンの変更の判定
                if (Mouse_Y >= Program.Window_Y - 64 && Mouse_Y < Program.Window_Y&&
                    Mouse_X >= Window_X - 128 - 64 * icons.Count()&&Mouse_X <= Window_X - 128)
                {
                    int n = (int)(Mouse_X - (Program.Window_X - 128 - 64 * icons.Count())) / 64;
                    if (Mouse_Button == DX.MOUSE_INPUT_LEFT)
                    {
                        LeftClick = icons[n];
                    }
                    if (Mouse_Button == DX.MOUSE_INPUT_RIGHT)
                    {
                        RightClick = icons[n];
                    }
                }

                //各ボタン対応アイコンクラスに座標とクリックされたという情報をセット
                Drawer.ChangeLtoW(ref Mouse_X, ref Mouse_Y);
                if (Mouse_Button == DX.MOUSE_INPUT_LEFT)
                {
                    LeftClick.React(Mouse_X, Mouse_Y);
                }
                if (Mouse_Button == DX.MOUSE_INPUT_RIGHT)
                {
                    RightClick.React(Mouse_X, Mouse_Y);
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
            int fX = Mouse_X;
            int fY = Mouse_Y;
            Drawer.ChangeLtoW(out fX, out fY);
        	fX = (fX / Space_Size) * Space_Size;
	        fY = (fY / Space_Size) * Space_Size;
            int x1 = i * Program.Space_Size;
            int y1 = j * Program.Space_Size;
            int x2 = (i + 1) * Program.Space_Size;
            int y2 = j * Program.Space_Size;
            int x3 = i * Program.Space_Size;
            int y3 = (j + 1) * Program.Space_Size;
            int x4 = (i + 1) * Program.Space_Size;
            int y4 = (j + 1) * Program.Space_Size;
            Drawer.ChangeWtoL(ref x1, ref y1);
            Drawer.ChangeWtoL(ref x2, ref y2);
            Drawer.ChangeWtoL(ref x3, ref y3);
            Drawer.ChangeWtoL(ref x4, ref y4);
            DX.DrawTriangle(x1, y1,
                            x2, y2,
                            x3, y3,
                            DX.GetColor(LandNutrition[i,j].Red, LandNutrition[i,j].Green, LandNutrition[i,j].Blue), DX.TRUE
            );
            DX.DrawTriangle(x2, y2,
                            x3, y3,
                            x4, y4,
                            DX.GetColor(LandNutrition[i,j].Red, LandNutrition[i,j].Green, LandNutrition[i,j].Blue), DX.TRUE
            );



	
            //各アイコンクラスの描画
            foreach(Icon icon in icons)
            {
                icon.Draw();
            }

            //アイコンのグラフィックの表示
            foreach(Icon icon in icons.Select((v, i) => ( v, i )))
            {
                DX.DrawGraph(Program.Window_X - 128 - (Icon.iconSize * (icons.Count() - i)), Program.Window_Y - Icon.iconSize, icon.GraphicHandle, DX.TRUE);
            }
            DX.DrawExtendGraph(Program.Window_X - 128, Program.Window_Y - 128, Program.Window_X, Program.Window_Y, angelGH, DX.TRUE);
        }

        enum IconIndexT
        {
            Scan,
            Thunder,
            Save
        }

        const int iconSize = 64;

        Icon LeftClick;
        Icon RightClick;

        Drawer Drawer;

        int Mouse_X;
        int Mouse_Y;
        int Mouse_Button;

        Icon[] icons;  
        
        int angelGH;
    }
}
