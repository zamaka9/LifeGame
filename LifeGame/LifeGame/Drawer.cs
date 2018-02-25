using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class Drawer
    {
        public void Initialize()
        {

        }

        public void Update()
        {
            //ズーム倍率の更新
            zoomLevel += DX.GetMouseWheelRotVol();
            if (zoomLevel > 0)
            {
                zoom = (10.0f + zoomLevel) / 10.0f;
            }
            else if (zoomLevel < 0)
            {
                zoom = 10.0f / (10.0f - zoomLevel);
            }
            else
            {
                zoom = 1.0f;
            }
            //カメラ位置の更新
            if (DX.GetMouseInput() == DX.MOUSE_INPUT_MIDDLE)
            {
                if (mouseRightCount == 0)
                {
                    DX.GetMousePoint(out int x, out int y);
                    oldMouse = new Vector2D(x, y);
                }
                else
                {
                    DX.GetMousePoint(out int x, out int y);
                    Vector2D mouse = new Vector2D(x, y);
                    camera += ((oldMouse - mouse) / zoom);
                    oldMouse = mouse;
                }
                mouseRightCount++;
            }
            else
            {
                mouseRightCount = 0;
            }

            DX.DrawString(Program.Window_X - 96, 0, zoom.ToString(), DX.GetColor(255, 255, 255));

            //エリア分割線の描画
            for (int i = 0; i <= Program.Space_X; i++)
            {
                Vector2D tem1 = new Vector2D(i * (Program.World_X / Program.Space_X), 0);
                Vector2D tem2 = new Vector2D(tem1.X, Program.World_Y);
                tem1 = ChangeWtoL(tem1);
                tem2 = ChangeWtoL(tem2);
                DX.DrawLine(tem1.iX, tem1.iY, tem2.iX, tem2.iY, DX.GetColor(100, 100, 100));
            }
            for (int i = 0; i <= Program.Space_Y; i++)
            {
                Vector2D tem1 = new Vector2D(0, i * (Program.World_Y / Program.Space_Y));
                Vector2D tem2 = new Vector2D(Program.World_X, tem1.Y);
                tem1 = ChangeWtoL(tem1);
                tem2 = ChangeWtoL(tem2);
                DX.DrawLine(tem1.iX, tem1.iY, tem2.iX, tem2.iY, DX.GetColor(100, 100, 100));
            }
        }

        public void AddDrawList(Vector2D position, int z, int size, int graphicHandle)
        {
            Vector2D local = new Vector2D(
            (int)(zoom * ((position.X - position.Y) / 1.414f - camera.X) + Program.Window_X / 2),
            (int)(zoom * ((position.X + position.Y) / 1.414f / 2 - camera.Y) + Program.Window_Y / 2));
            int SZ = (int)(size * zoom);
            DX.DrawExtendGraph(local.iX - SZ, local.iY - SZ, local.iX + SZ, local.iY + SZ, graphicHandle, DX.TRUE);
        }

        public Vector2D ChangeLtoW(Vector2D vector)
        {
            float a = ((vector.X - Program.Window_X / 2) / zoom + camera.X) * 1.414f;
            float b = ((vector.Y - Program.Window_Y / 2) / zoom + camera.Y) * 1.414f * 2;
            float x = (a + b) / 2;
            return new Vector2D(x, b - x);
        }

        public Vector2D ChangeWtoL(Vector2D vector)
        {
            return new Vector2D((zoom * ((vector.X - vector.Y) / 1.414f - camera.X) + Program.Window_X / 2),
                                (zoom * ((vector.X + vector.Y) / 1.414f / 2 - camera.Y) + Program.Window_Y / 2));
        }

        struct DrawObject
        {
            int x;
            int y;
            int z;
            int size;
            int gh;
        }
        List<DrawObject> DrawList;

        Vector2D camera = new Vector2D(0.0f, Program.Window_Y / 2);
        int zoomLevel = 0;
        public float zoom = 0.0f;
        Vector2D oldMouse = new Vector2D(0, 0);
        int mouseRightCount;
    }
}
