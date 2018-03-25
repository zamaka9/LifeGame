using LifeGame.Landforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    class LandformsManager
    {
        public LandFormBase[,] landFormBase;
        public static IDictionary<int, Type> landformClassMap = new Dictionary<int, Type>();
        public static List<int> landformIdList = new List<int>();

        public void Initialize (Land land)
        {
            landFormBase = new LandFormBase[Program.Space_X, Program.Space_Y];
            for (int x = 0; x < Program.Space_X; x++)
            {
                for(int y = 0; y < Program.Space_Y; y++)
                {
                    if (Program.Rand.Next(10) == 0)
                    {
                        SetLandform(CreateLandform(1), x, y);
                    }
                    else
                    {
                        SetLandform(CreateLandform(0), x, y);
                    }
                    landFormBase[x, y].Initialize(this,x,y);
                }
            }
        }

        public void Update()
        {
            for (int x = 0; x < Program.Space_X; x++)
            {
                for (int y = 0; y < Program.Space_Y; y++)
                {
                    landFormBase[x, y].Update();
                }
            }
        }

        /// <summary>
        ///LandFormBaseRegisterのRegister()の中で登録してください
        /// </summary>
        /// <param name="id"> LandFormBase固有のidです</param>
        /// <param name="landformType">Landformのクラスを表すTypeです</param>
        /// <returns>成功したらtrue,失敗したらfalseを返します</returns>
        public static bool RegisterAct(int id, Type landformType)
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

        public void SetLandform(LandFormBase landForm, int x, int y)
        {
            landFormBase[x, y] = landForm;
        }

        public void RemoveLandform(int x, int y)
        {
            landFormBase[x, y] = CreateLandform(0);
        }

        public LandFormBase CreateLandform(int key)
        {
            var args = new Object[] { };
            LandFormBase actInstance = (LandFormBase)(Activator.CreateInstance(landformClassMap[key], args));
            actInstance.id = key;
            return actInstance;
        }
    }
}

