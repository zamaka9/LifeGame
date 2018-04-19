using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Landforms
{
    abstract class LandFormBase
    {
        public int posX, posY;
        public Land mgr;
        public int id;

        /// <summary>
        /// その地形ができたときに呼び出されます
        /// </summary>
        /// <param name="land"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void Initialize(Land mgr, int x, int y)
        {
            this.mgr = mgr;
            posX = x;
            posY = y;
        }
        /// <summary>
        /// Updateされるときに呼び出されます。タイミングはCreatureの直前です。
        /// 地形が生物に何か影響を及ぼすときは、基本的にActに記述してください。
        /// こちらのUpdateは、地形がなくなるとか、近くの地形に影響するなどの変化だけを記述することを
        /// 推奨します
        /// </summary>
        public abstract void Update();
    }
}
