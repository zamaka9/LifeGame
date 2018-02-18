using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    /// <summary>
    /// 地面の栄養を管理するクラス
    /// </summary>
    class Land
    {
        public Land()
        {

        }

        public void Initialize(Drawer drawer){
            Drawer = drawer;

            //Spaceを初期化
            Space = new List<Creature>[Program.Space_X, Program.Space_Y];
            LandNutrition = new Nutrition[Program.Space_X, Program.Space_Y];
            for (int i = 0; i < Program.Space_X; i++)
            {
                for (int j = 0; j < Program.Space_Y; j++)
                {
                    Space[i, j] = new List<Creature>();
                    LandNutrition[i, j] = new Nutrition();
                    LandNutrition[i, j].Rand(90, 110);
                }
            }
        }

        public void Update()
        {
            foreach(List<Creature> space in Space)
            {
                space.Clear();
            }
        }

        public void Draw()
        {
            for(int i = 0; i < Program.Space_X; i++){
                for(int j = 0; j < Program.Space_Y; j++){
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
                    //Console.WriteLine("test");
                }
            }
        }

        public void AddCList(float x, float y, Creature pointer)
        {
            int X = ReturnX(x);
            int Y = ReturnY(y);
            Space[X,Y].Add(pointer);
            pointer.TargetList = (Space[X,Y]);
        }

	    public List<Creature> GetCList(float x, float y)
        {
            int X = ReturnX(x);
            int Y = ReturnY(y);
            return Space[X,Y];
        }

    	public Nutrition GetLandNutrition(float x, float y)
        {
            int X = ReturnX(x);
            int Y = ReturnY(y);
            return LandNutrition[X,Y];
        }

    	public void SetLandNutrition(float x, float y, Nutrition Nut)
        {
            int X = ReturnX(x);
            int Y = ReturnY(y);
            LandNutrition[X,Y] = Nut;
        }

        List<Creature>[,] Space;
        Nutrition[,] LandNutrition;
        Drawer Drawer;

        int ReturnX(float x)
        {
            int X = (int)(x * Program.Space_X / Program.World_X);
            if (X < 0)
            {
                X = 0;
            }
            else if (X >= Program.Space_X)
            {
                X = Program.Space_X - 1;
            }
            return X;
        }

        int ReturnY(float y)
        {
            int Y = (int)(y * Program.Space_Y / Program.World_Y);
            if (Y < 0)
            {
                Y = 0;
            }
            else if (Y >= Program.Space_Y)
            {
                Y = Program.Space_Y - 1;
            }
            return Y;
        }
    }
}
