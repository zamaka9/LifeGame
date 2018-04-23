using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using LifeGame.Landforms;

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
            Sections = new Section[Program.Space_X, Program.Space_Y];

            for (int x = 0; x < Program.Space_X; x++)
            {
                for (int y = 0; y < Program.Space_Y; y++)
                {
                    Sections[x, y] = new Section();
                    Sections[x, y].CList = new List<Creature>();
                    Sections[x, y].Nut = new Nutrition();
                    Sections[x, y].Nut.Rand(Nutrition.MaxValue / 12, Nutrition.MaxValue / 6);
                    if (Program.Rand.Next(10) == 0)
                    {
                        Sections[x, y].Landform = CreateLandform(1);
                    }
                    else
                    {
                        Sections[x, y].Landform = CreateLandform(0);
                    }
                    Sections[x, y].Initialize(this, x, y);
                }
            }
            
        }

        public void Update()
        {
            foreach(Section sec in Sections)
            {
                sec.Update();
            }
        }

        public void Draw()
        {
            for(int i = 0; i < Program.Space_X; i++){
                for(int j = 0; j < Program.Space_Y; j++){
                    Vector2D vec = (new Vector2D(i, j) + new Vector2D(0.5f, 0.5f)) * Program.Space_Size;
                    DX.SetDrawBright(Sections[i, j].Nut.Red >> nbit, Sections[i, j].Nut.Green >> nbit, Sections[i, j].Nut.Blue >> nbit);
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
            Sections[X,Y].CList.Add(pointer);
            pointer.TargetList = (Sections[X, Y].CList);
        }

	    public List<Creature> GetCList(Vector2D position)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
            return Sections[X, Y].CList;
        }

    	public Nutrition GetLandNutrition(Vector2D position)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
            return Sections[X, Y].Nut;
        }

    	public void SetLandNutrition(Vector2D position, Nutrition Nut)
        {
            int X = ReturnX(position.X);
            int Y = ReturnY(position.Y);
            Sections[X, Y].Nut = Nut;
        }

        public LandFormBase GetLandformAt(int x,int y)
        {
            return Sections[x, y].Landform;
        }

        public LandFormBase GetLandformAt(Vector2D vector)
        {
            return GetLandformAt(ReturnX(vector.X), ReturnY(vector.Y));
        }


        /// <summary>
        ///LandFormBaseRegisterのRegister()の中で登録してください
        /// </summary>
        /// <param name="id"> LandFormBase固有のidです</param>
        /// <param name="landformType">Landformのクラスを表すTypeです</param>
        /// <returns>成功したらtrue,失敗したらfalseを返します</returns>
        public static bool RegisterLandform(int id, Type landformType)
        {
            if (!landformType.IsSubclassOf(typeof(LandFormBase)))
            {
                Console.WriteLine(landformType.ToString() + "はLandFormBaseクラスを継承していないため登録できません");
                return false;
            }
            if (landformClassMap.ContainsKey(id))
            {
                Console.WriteLine(landformType.ToString() + "と" + landformClassMap[id].ToString() + "のIDが両方とも" + id + "のため登録できません");
                return false;
            }
            //Console.WriteLine(actType.ToString()+ "," + id);
            landformClassMap.Add(id, landformType);
            landformIdList.Add(id);
            return true;
        }

        public LandFormBase CreateLandform(int key)
        {
            var args = new Object[] { };
            LandFormBase actInstance = (LandFormBase)(Activator.CreateInstance(landformClassMap[key], args));
            actInstance.id = key;
            return actInstance;
        }

        public static IDictionary<int, Type> landformClassMap = new Dictionary<int, Type>();
        public static List<int> landformIdList = new List<int>();
        Drawer Drawer;
        public Section[,] Sections;

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
