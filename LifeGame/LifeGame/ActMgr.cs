using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeGame.Acts;

namespace LifeGame
{
    class ActMgr
    {
        public void Initialize(Creature owner, List<Creature> targetList, List<int> actions)
        {
            ActList = new List<Act>();
            foreach(int act in actions)
            {
                switch (act)
                {
                   
                    case 0:
                        //汚いので直したい ownerをActクラスのほうで持ってほしい
                        ActList.Add(new Move(owner));//仮;
                        break;
                    /*
               case 1:
                   ActList.Add(new Attack(itself, TargetList));//仮
                   break;
                   */
                    case 2:
                        //汚いので直したい ownerをActクラスのほうで持ってほしい
                        ActList.Add(new Divide(owner));//仮
                        break;
                        /*
                    case 3:
                        ActList.Add(new Prey(itself, TargetList));//仮
                        break;
                    case 4:
                        ActList.Add(new Recover(itself, TargetList));//仮
                        break;
                    case 5:
                        ActList.Add(new Age(itself, TargetList));//仮
                        break;
                    case 6:
                        ActList.Add(new Produce(itself, TargetList));//仮
                        break;*/
                }
            }

            //Actの初期化
            foreach (Act act in ActList)
            {
                act.Initialize();
            }
        }

        public void Update()
        {
            foreach (Act act in ActList)
            {
                act.Update();
            }
        }

        List<Act> ActList= new List<Act>();
    }
}
