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

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            costbase = new Nutrition(owner.Size*10, owner.Size, owner.Size)*level;
            requirementbase = new Nutrition(0, owner.Size * 2000, owner.Size * 10000) * level;
        }
        public override bool Update()
        {
            Nutrition newNut = new Nutrition(0, owner.Size * 7, 0)*level;
            owner.Nutrition += newNut;

            return true;
        }
        public int requirement;
    }

    
}
