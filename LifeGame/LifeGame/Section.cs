using LifeGame.Landforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    /// <summary>
    /// 地面の一つ一つのマスを表します
    /// </summary>
    class Section
    {
        public int X, Y;
        public Land Land;
        public Nutrition Nut;
        public List<Creature> CList;
        public LandFormBase Landform;

        public void Initialize(Land land,int x,int y)
        {
            Land = land;
            X = x;
            Y = y;
            Landform.Initialize(land,x,y);

        }

        public void Update()
        {
            Landform.Update();
            CList.Clear();
        }
    }
}
