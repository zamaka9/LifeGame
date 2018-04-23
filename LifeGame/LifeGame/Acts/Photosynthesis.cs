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
            int height = Land.GetHeightAt(owner.Position);
            int value = height * 3 - 2;
            Nutrition newNut = new Nutrition(value, 0, 0)*level*owner.Size;
            owner.Nutrition += newNut;

            return true;
        }
        public int requirement;
    }

    
}
