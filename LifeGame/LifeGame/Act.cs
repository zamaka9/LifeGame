using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    abstract class Act
    {
        public static CreatureMgr CreatureMgr;
        public static Land Land;
        public Creature owner;

        public virtual void Initialize(Creature owner) {
            this.owner = owner;
        }

        public abstract void Update();

        /// <summary>
        /// 毎フレームUpdateを呼ぶ必要があるなら、trueを返す
        /// </summary>
        /// <returns></returns>
        public virtual bool ShouldBeUpdatedEveryFrame() { return false; }

    }
}
