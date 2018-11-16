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
            //costbase = new Nutrition(owner.Size*5, 0, 0)*level;
            Left = new Formula(0, 0, 0, 16);
            Right = new Formula(0, 2, 0, 0);
        }
        public override bool Update(double coeff)
        {
            Nutrition newNut = new Nutrition(0, owner.Size * 3, 0)*level;
            owner.Nutrition += newNut;

            return true;
        }
    }

    
}
