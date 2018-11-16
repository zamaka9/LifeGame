using LifeGame.Landforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装
    class Chemoautotroph : Act
    {

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            costbase = new Nutrition(owner.Size, owner.Size, owner.Size) *level;
        }
        public override double Update(double coeff)
        {
            LandFormBase landform = owner.mgr.Land.GetLandformAt(owner.Position);
            if (landform.id == 1)
            {
                //Console.WriteLine(((HydrothermalVent)landform).LastCreaturesGave + "," + ((HydrothermalVent)landform).NutRatio);
                Nutrition newNut = new Nutrition(2,1,1)*( 20f * (float)owner.Size * (float)level * ((HydrothermalVent)landform).NutRatio);
                ((HydrothermalVent)landform).LastCreaturesGave+=level;
                owner.Nutrition += newNut;
                //Console.WriteLine(newNut.ToString());
                return coeff;
            }
            else
            {
                return 0;
            }
        }
    }

    
}
