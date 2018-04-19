using LifeGame.Landforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    class LandformRegister
    {
        public static void Register()
        {
            Land.RegisterLandform(0, typeof(LandformEmpty));
            Land.RegisterLandform(1, typeof(HydrothermalVent));
        }
    }
}
