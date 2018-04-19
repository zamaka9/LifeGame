using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //糖新生。タンパク質からグルコースをつくります
    class Gluconeogenesis : Act
    {

        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
            costbase = new Nutrition(0, owner.Size*2, 0)*level;
        }
        public override bool Update()
        {
            Nutrition newNut = new Nutrition(owner.Size, 0,0)*level;
            owner.Nutrition += newNut;

            return true;
        }
    }

    
}
