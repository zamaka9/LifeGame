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
        public static ActMgr ActMgr;
        public static Land Land;
        public Creature owner;
        public int id;//Actごとに固有のID。例えばどの生物のどのパーツでも、Moveのidは1である

        public virtual void Initialize(Creature owner) {
            this.owner = owner;
        }

        public abstract void Update();

        /// <summary>
        /// 毎フレームUpdateを呼ぶ必要があるなら、trueを返す。使用非推奨
        /// </summary>
        /// <returns></returns>
        public virtual bool ShouldBeUpdatedEveryFrame() { return false; }
    }
}
