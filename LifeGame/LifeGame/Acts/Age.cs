﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Acts
{
    //仮実装。登録されないので今のところ無意味
    class Age : Act
    {
        public override void Initialize(Creature owner, int level)
        {
            base.Initialize(owner, level);
        }

        public override bool Update()
        {
            owner.HP -= 10;
            return true;
        }
    }
}
