using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeGame.Acts;

namespace LifeGame
{
    /// <summary>
    /// Actを登録するためだけに存在するクラス
    /// </summary>
    class ActRegister
    {
        public static void Register()
        {
            ActMgr.RegisterAct(0, typeof(Divide));
            ActMgr.RegisterAct(1, typeof(Attack));
            ActMgr.RegisterAct(2, typeof(Move));
            ActMgr.RegisterAct(4, typeof(Recover));
            ActMgr.RegisterAct(5, typeof(Age));
            ActMgr.RegisterAct(6, typeof(Produce));
            ActMgr.RegisterAct(7, typeof(GetNutFromLand));
            ActMgr.RegisterAct(8,typeof(Scavenge));
            ActMgr.RegisterAct(9, typeof(Photosynthesis));
            ActMgr.RegisterAct(10, typeof(NitrogenFixation));
        }
    }
}
