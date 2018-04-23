using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装
    class Photosynthesis : Act
    {

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            costbase = new Nutrition(owner.Size, owner.Size, owner.Size)*level;
            requirementbase = new Nutrition(0, owner.Size*1000, owner.Size*1000)*level;
        }
        public override bool Update()
        {
            Nutrition newNut = new Nutrition(owner.Size*4, 0, 0)*level;
            owner.Nutrition += newNut;

            return true;
        }
        public int requirement;
    }

    
}
