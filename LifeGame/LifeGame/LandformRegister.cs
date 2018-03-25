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
            LandformsManager.RegisterAct(0, typeof(LandformEmpty));
            LandformsManager.RegisterAct(1, typeof(HydrothermalVent));
        }
    }
}
