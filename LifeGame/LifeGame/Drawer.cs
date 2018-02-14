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
                    DX.GetMousePoint(out oldMouseX, out oldMouseY);
                }
                else
                {
                    int Mouse_X = 0;
                    int Mouse_Y = 0;
                    DX.GetMousePoint(out Mouse_X, out Mouse_Y);
                    cameraX += (oldMouseX - Mouse_X) / zoom;
                    cameraY += (oldMouseY - Mouse_Y) / zoom;
                    oldMouseX = Mouse_X;
                    oldMouseY = Mouse_Y;
                }
                mouseRightCount++;
            }
            else
            {
                mouseRightCount = 0;
            }

            DX.DrawString(Program.Window_X - 96, 0, zoom.ToString(), DX.GetColor(255, 255, 255));

            //エリア分割線の描画
            int temX1, temY1, temX2, temY2;
            for (int i = 0; i <= Program.Space_X; i++)
            {
                temX1 = i * (Program.World_X / Program.Space_X);
                temY1 = 0;
                temX2 = temX1;
                temY2 = Program.World_Y;
                ChangeWtoL(ref temX1, ref temY1);
                ChangeWtoL(ref temX2, ref temY2);
                DX.DrawLine(temX1, temY1, temX2, temY2, DX.GetColor(100, 100, 100));
            }
            for (int i = 0; i <= Program.Space_Y; i++)
            {
                temX1 = 0;
                temY1 = i * (Program.World_Y / Program.Space_Y);
                temX2 = Program.World_X;
                temY2 = temY1;
                ChangeWtoL(ref temX1, ref temY1);
                ChangeWtoL(ref temX2, ref temY2);
                DX.DrawLine(temX1, temY1, temX2, temY2, DX.GetColor(100, 100, 100));
            }
        }

        public void AddDrawList(float x, float y, int z, int size, int graphicHandle)
        {
            int Local_X = (int)(zoom * ((x - y) / 1.414f - cameraX) + Program.Window_X / 2);
            int Local_Y = (int)(zoom * ((x + y) / 1.414f / 2 - cameraY) + Program.Window_Y / 2);
            int SZ = (int)(size * zoom);
            DX.DrawExtendGraph(Local_X - SZ, Local_Y - SZ, Local_X + SZ, Local_Y + SZ, graphicHandle, DX.TRUE);
        }

        public void ChangeLtoW(ref int x, ref int y)
        {
            float a = ((x - Program.Window_X / 2) / zoom + cameraX) * 1.414f;
            float b = ((y - Program.Window_Y / 2) / zoom + cameraY) * 1.414f * 2;
            x = (int)(a + b) / 2;
            y = (int)b - x;
        }

        public void ChangeWtoL(ref int x, ref int y)
        {
            float tem1 = x - y;
            float tem2 = x + y;
            x = (int)(zoom * (tem1 / 1.414f - cameraX) + Program.Window_X / 2);
            y = (int)(zoom * (tem2 / 1.414f / 2 - cameraY) + Program.Window_Y / 2);
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

        float cameraX = 0.0f;
        float cameraY = Program.Window_Y / 2;
        int zoomLevel = 0;
        float zoom = 0.0f;
        int oldMouseX;
        int oldMouseY;
        int mouseRightCount;
    }
}
