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
        //public Nutrition basicCost = new Nutrition();//Actが実行され、成功失敗にかかわらず消費する栄養
        public Nutrition requirement = new Nutrition();//Actが実行されるのに最低限必要な栄養

        public Nutrition costbase = new Nutrition();//レベルに応じたcost算出の元となる値
       // public Nutrition basicCostbase = new Nutrition();//レベルに応じたbasiccost算出の元となる値
        public Nutrition requirementbase = new Nutrition();//レベルに応じたrequirement算出の元となる値

        public int level;

        public virtual void Initialize(Creature owner,int level) {
            this.owner = owner;
            this.level = level;
           
        }

        public virtual void SetCostFromCostBase()
        {
            cost = GetCostFromCostBase(level, costbase);
            //basicCost = GetCostFromCostBase(level, basicCostbase);
            requirement = GetCostFromCostBase(level, requirementbase);
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
            if (owner.Nutrition > this.cost + this.requirement)
            {
                if (Update())
                {
                    owner.Nutrition -= this.cost;
                }
                //owner.Nutrition -= this.basicCost;
            }
            else
            {
                OnNutritionNotEnough();
            }
        }
    
        /// <summary>
        /// 栄養が十分でなく、Update()が呼ばれなかったときにこちらが呼ばれる
        /// </summary>
        public virtual void OnNutritionNotEnough() { }

        public virtual Nutrition GetCostFromCostBase(int level,Nutrition nutbase)
        {
            float var1=1;
            for(int i = 0; i < level; i++)
            {
                var1 *= 0.95f;
            }
            return nutbase  * var1;
        }

    }
}
