using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class Gene
    {

        public Gene(Gene parentGene)
        {
            Size = parentGene.Size;
            HP = parentGene.HP + Program.Rand.Next(-50, 50);
            Nutrition = parentGene.Nutrition + new Nutrition().Rand(5);
            ActList = new List<int>(parentGene.ActList); // 値渡し
            if (Program.Rand.Next(5) == 0)
            {
                ActList.Add(Program.Rand.Next(6));
            }
            if (Program.Rand.Next(40) == 0)
            {
                if (ActList.Count > 1)
                {
                    ActList.RemoveAt(ActList.Count - 1);
                }
            }
        }

        public Gene(int hp, Nutrition nutrition, int size, List<int> Actions)
        {
            HP = hp;
            Nutrition = nutrition;
            Size = size;
            ActList = Actions;
        }

        public Gene()
        {
            Size = Program.Rand.Next(254) + 1;
            HP = Size * 64 + Program.Rand.Next(255);
            Nutrition.Rand(255);
            /*
            ActList.Add(Program.Rand.Next(6));
            ActList.Add(Program.Rand.Next(6));
            ActList.Add(Program.Rand.Next(6));
            */
            ActList.Add(0);
            ActList.Add(2);
        }

        public int HP { get; private set; } = 0;//最大体力
        public Nutrition Nutrition { get; private set; } = new Nutrition();//栄養最大値
        public int Size { get; private set; } = 0;//大きさ（体格）
        public List<int> ActList { get; private set; } = new List<int>();//行動リスト

    }
}
