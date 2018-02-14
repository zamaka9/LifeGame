using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    /// <summary>
    /// 地面の栄養を管理するクラス
    /// </summary>
    class Land
    {
        public Land()
        {
            //Spaceを初期化
            Space = new List<Creature>[Program.Space_X, Program.Space_Y];
            LandNutrition = new Nutrition[Program.Space_X, Program.Space_Y];
            for (int i = 0; i < Program.Space_X; i++)
            {
                for (int j = 0; j < Program.Space_Y; j++)
                {
                    Space[i, j] = new List<Creature>();
                    LandNutrition[i, j] = new Nutrition();
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
