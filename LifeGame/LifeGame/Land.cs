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
        int nessuihunsyutukouGraphicHandle;

        public Land()
        {

        }

        public void Initialize(Drawer drawer){
            Drawer = drawer;

            nessuihunsyutukouGraphicHandle = DX.LoadGraph("Data/nessuihunsyutukou.png");

            //Spaceを初期化
            Space = new List<Creature>[Program.Space_X, Program.Space_Y];
            LandNutrition = new Nutrition[Program.Space_X, Program.Space_Y];
            for (int i = 0; i < Program.Space_X; i++)
            {
                for (int j = 0; j < Program.Space_Y; j++)
                {
                    Space[i, j] = new List<Creature>();
                    LandNutrition[i, j] = new Nutrition();
                    LandNutrition[i, j].Rand(Nutrition.MaxValue/4, Nutrition.MaxValue / 2);
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
                    Vector2D vec = (new Vector2D(i, j) + new Vector2D(0.5f, 0.5f)) * Program.Space_Size;
                    DX.SetDrawBright(LandNutrition[i, j].Red >> nbit, LandNutrition[i, j].Green >> nbit, LandNutrition[i, j].Blue >> nbit);
                    Drawer.AddDrawList(vec, 0, 36, nessuihunsyutukouGraphicHandle);
                    /*
                    Vector2D vec1 = new Vector2D(i, j) * Program.Space_Size;
                    Vector2D vec2 = new Vector2D(i + 1, j) * Program.Space_Size;
                    Vector2D vec3 = new Vector2D(i, j + 1) * Program.Space_Size;
                    Vector2D vec4 = new Vector2D(i + 1, j + 1) * Program.Space_Size;
                    vec1 = Drawer.ChangeWtoL(vec1);
                    vec2 = Drawer.ChangeWtoL(vec2);
                    vec3 = Drawer.ChangeWtoL(vec3);
                    vec4 = Drawer.ChangeWtoL(vec4);
                    DX.DrawTriangle(vec1.iX, vec1.iY, vec2.iX, vec2.iY, vec3.iX, vec3.iY,
                                    DX.GetColor(LandNutrition[i,j].Red >> nbit, LandNutrition[i,j].Green >> nbit, LandNutrition[i,j].Blue >> nbit), DX.TRUE);
                    DX.DrawTriangle(vec2.iX, vec2.iY, vec3.iX, vec3.iY, vec4.iX, vec4.iY,
                                    DX.GetColor(LandNutrition[i,j].Red >> nbit, LandNutrition[i,j].Green>> nbit, LandNutrition[i,j].Blue >> nbit), DX.TRUE);*/
                }
            }
            DX.SetDrawBright(255, 255, 255);
        }

        public void AddCList(Vector2D position, Creature pointer)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
            Space[X,Y].Add(pointer);
            pointer.TargetList = (Space[X,Y]);
        }

	    public List<Creature> GetCList(Vector2D position)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
            return Space[X,Y];
        }

    	public Nutrition GetLandNutrition(Vector2D position)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
            return LandNutrition[X,Y];
        }

    	public void SetLandNutrition(Vector2D position, Nutrition Nut)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
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
        public static int nbit = 16;//栄養値を16ビット右にシフトする(=65536で割る)と最大255になる
    }
}
