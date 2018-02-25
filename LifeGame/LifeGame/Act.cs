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
        public int id;//Actごとに固有のID。例えばどの生物のどのパーツでも、Moveのidは1である
        public Nutrition cost=new Nutrition();//Actが実行され、成功したときに消費する栄養
        public Nutrition basicCost = new Nutrition();//Actが実行され、成功失敗にかかわらず消費する栄養

        public virtual void Initialize(Creature owner) {
            this.owner = owner;
        }

        public abstract bool Update();

        /// <summary>
        /// 毎フレームUpdateを呼ぶ必要があるなら、trueを返す。使用非推奨
        /// </summary>
        /// <returns></returns>
        public virtual bool ShouldBeUpdatedEveryFrame() { return false; }

        /// <summary>
        /// Updateがtrueを返したとき、実行が成功したとみなし栄養を消費する
        /// </summary>
        public virtual void UpdateAndConsumeNut ()
        {
            if (owner.Nutrition > this.cost + this.basicCost)
            {
                if (Update())
                {
                    owner.Nutrition -= this.cost;
                }
                owner.Nutrition -= this.basicCost;
            }
            else
            {
                OnNutritionNotEnough();
            }
        }

        public virtual void OnNutritionNotEnough() { }

    }
}
