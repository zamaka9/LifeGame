using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装
    class NitrogenFixation : Act
    {

        public override void Initialize(Creature owner)
        {
            base.Initialize(owner);
            basicCost = new Nutrition(1000,100,500);//雑。将来的には主にサイズによってこの値を決めたい
        }
        public override bool Update()
        {
            Nutrition newNut = new Nutrition(0, 5000, 0);
            owner.Nutrition += newNut;

            return true;
        }
        public int requirement;
    }

    
}
