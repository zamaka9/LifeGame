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
            Left = new Formula(0, 0, 0, 0);
            Right = new Formula(0, 0, 0, 24);
        }
    }
        public override bool Update(double coeff)
        {
            return true;
        }
    }

    
}
