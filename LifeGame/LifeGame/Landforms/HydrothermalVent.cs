using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Landforms
{
    class HydrothermalVent : LandFormBase
    {
        public int LastCreaturesGave;
        public float NutRatio;
        public override void Update()
        {
            NutRatio = 1/(1 + 0.1f *LastCreaturesGave);
            //Console.WriteLine(LastCreaturesGave + "," + NutRatio+":"+posX+","+posY);
            LastCreaturesGave = 0;
            
        }
    }
}
