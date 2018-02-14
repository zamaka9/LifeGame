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
            Drawer = drawer;
            Icon.Drawer = drawer;
            Icon.Land = land;

            //各アイコンクラスの初期化
            Scan = new IconScan();
            Thunderbolt = new IconThunderbolt();
            Scan.Initialize();
            Thunderbolt.Initialize();

            //仮　アイコン用グラフィックの読み込み
            GH[0] = DX.LoadGraph("Data/スキャンアイコン.png");
            GH[1] = DX.LoadGraph("Data/雷アイコンカラー.png");
            GH[2] = DX.LoadGraph("Data/図鑑アイコンカラー.png");
            GH[3] = DX.LoadGraph("Data/テンシ大.png");

            //初期選択アイコンを設定
            LeftClick = Scan;
            RightClick = Thunderbolt;
        }

        public void Update()
        {
            //マウスのいずれかのクリックがされたら
            if (DX.GetMouseInputLog(out Mouse_Button, out Mouse_X, out Mouse_Y, DX.TRUE) == 0)
            {
                //アイコンの変更の判定
                if (Mouse_Y >= Program.Window_Y - 64 && Mouse_Y < Program.Window_Y)
                {
                    if (Mouse_X >= ThunderboltIconX && Mouse_X <= ThunderboltIconX + 64)
                    {
                        if (Mouse_Button == DX.MOUSE_INPUT_LEFT)
                        {
                            LeftClick = Thunderbolt;
                        }
                        if (Mouse_Button == DX.MOUSE_INPUT_RIGHT)
                        {
                            RightClick = Thunderbolt;
                        }
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
            Scan.Update();
            Thunderbolt.Update();
        }

        public void Draw()
        {
            //各アイコンクラスの描画
            Scan.Draw();
            Thunderbolt.Draw();

            //アイコンのグラフィックの表示
            DX.DrawGraph(ScanIconX, Program.Window_Y - 64, GH[0], DX.TRUE);
            DX.DrawGraph(ThunderboltIconX, Program.Window_Y - 64, GH[1], DX.TRUE);
            DX.DrawGraph(SaveIconX, Program.Window_Y - 64, GH[2], DX.TRUE);
            DX.DrawExtendGraph(Program.Window_X - 128, Program.Window_Y - 128, Program.Window_X, Program.Window_Y, GH[3], DX.TRUE);
        }

        enum IconIndexT
        {
            Scan,
            Thunder,
            Save
        }

        const int iconSize = 64;

        const int ScanIconX = Program.Window_X - 320;
        const int ThunderboltIconX = Program.Window_X - 256;
        const int SaveIconX = Program.Window_X - 192;

        Icon LeftClick;
        Icon RightClick;

        Drawer Drawer;

        IconScan Scan;
        IconThunderbolt Thunderbolt;

        int Mouse_X;
        int Mouse_Y;
        int Mouse_Button;

        int[] GH = new int[4];
        
    }
}
