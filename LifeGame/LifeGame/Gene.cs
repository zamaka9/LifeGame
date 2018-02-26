﻿using System;
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
            Size = parentGene.Size + Program.Rand.Next(-5, 5);
            if (Size < 10)
            {
                Size = 10;
            }
            HP = parentGene.HP + Program.Rand.Next(-50, 50);
            if (HP < 1)
            {
                HP = 1;
            }
            Nutrition = parentGene.Nutrition + new Nutrition().Rand(5);
            ActList = new List<int>(parentGene.ActList); // 値渡し
            if (Program.Rand.Next(20) == 0)
            {
                ActList.Add(ActMgr.GetRandomActId());
            }
            if (Program.Rand.Next(20) == 0)
            {
                if (ActList.Count > 1)
                {
                    ActList.RemoveAt(Program.Rand.Next(ActList.Count-1));
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
            Size = Program.Rand.Next(254) + 10;
            HP = 1+ Program.Rand.Next(255)*64;
            Nutrition.Rand(Size*30000);
            /*
            ActList.Add(Program.Rand.Next(6));
            ActList.Add(Program.Rand.Next(6));
            ActList.Add(Program.Rand.Next(6));
            */
            ActList.Add(0);
            //ActList.Add(1);
            ActList.Add(2);
            ActList.Add(7);
            //ActList.Add(8);
        }

        public int HP { get; private set; } = 0;//最大体力
        public Nutrition Nutrition { get; private set; } = new Nutrition();//栄養最大値
        public int Size { get; private set; } = 0;//大きさ（体格）
        public List<int> ActList { get; private set; } = new List<int>();//行動リスト

    }
}
