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
                        ActList.Add(new Move());//仮;
                        break;

                    case 1:
                        ActList.Add(new Attack());//仮
                        break;

                    case 2:
                        ActList.Add(new Divide());//仮
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
                    case 7:
                        ActList.Add(new GetNutFromLand());
                        break;
                }
            }

            //Actの初期化
            foreach (Act act in ActList)
            {
                act.Initialize(owner);
            }
        }

        public void Update()
        {
            bool isTimeToUpdate = ++timer >= timerMax;
            if (isTimeToUpdate)
            {
                timer = 0;
            }
            foreach (Act act in ActList)
            {
                
                if (isTimeToUpdate || act.ShouldBeUpdatedEveryFrame())
                {
                    act.Update();
                }
                
            }
        }

        List<Act> ActList= new List<Act>();
        public int timer;//毎フレームUpdateしていると重いし、新しいActの方でも不都合があるので60Fに一度にします
        public int timerMax = 60;

    }
}
