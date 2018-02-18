using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class Program
    {
        public static Random Rand = new Random();

        public const int Space_Size = 50;//エリア一つ当たりの大きさ

        public const int Space_X = 10;//エリアの横分割数
        public const int Space_Y = 10;//エリアの縦分割数

        public const int World_X = Space_Size * Space_X;//ワールド座標の横最大値
        public const int World_Y = Space_Size * Space_Y;//ワールド座標の縦最大値

        public const int Window_X = 640;//ウィンドウの横幅
        public const int Window_Y = 480;//ウィンドウの縦幅


        public const int MaxCreature = 10;//オブジェクトの最大数(←予定　現在は初期オブジェクト数)


        [STAThread]
        static void Main()
        {
            // ウィンドウモードに切り替え 
            DX.ChangeWindowMode(DX.TRUE);
            // フルスクリーン時の画面拡大モードを設定
            DX.SetFullScreenScalingMode(DX.DX_FSSCALINGMODE_NEAREST);
            // ウィンドウに表示する名前を設定
            DX.SetMainWindowText("LifeGame");
            // ウィンドウのアイコンを設定
            //DX.SetWindowIconID(101);
            DX.SetDoubleStartValidFlag(DX.TRUE);
            DX.SetGraphMode(Window_X, Window_Y, 32);

            // DXライブラリの初期化 
            if (DX.DxLib_Init() == -1)
            {
                // エラーが発生したら終了 
                return;
            }
            // ウィンドウが非アクティブでも処理を続ける設定
            DX.SetAlwaysRunFlag(DX.TRUE);

            DX.SetWindowSizeChangeEnableFlag(DX.TRUE);
            // 裏画面を描画画面に設定
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);

            // 各クラスの初期化処理
            Drawer Drawer = new Drawer();
            Land Land = new Land();
            CreatureMgr CreatureMgr = new CreatureMgr();
            God God = new God();

            Drawer.Initialize();
            Land.Initialize(Drawer);
            CreatureMgr.Initialize(Land, Drawer);
            God.Initialize(Land, Drawer);

            while (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 0 && DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0)
            {//画面更新 & メッセージ処理 & 画面消去

                Drawer.Update();

                CreatureMgr.Update();
                God.Update();
                Land.Update();

                Land.Draw();
                CreatureMgr.Draw();
                God.Draw();
            }

            // DXライブラリの終了
            DX.DxLib_End();

            return;
        }
    }
}
