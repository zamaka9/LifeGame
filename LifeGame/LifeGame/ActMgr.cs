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
            Act.ActMgr = this;
            
            ActList = new List<Act>();
            foreach(int act in actions)
            {
                var args= new Object[] { };
                if (!ActClassMap.ContainsKey(act)) continue;
                Act actInstance = (Act)(Activator.CreateInstance(ActClassMap[act], args));
                actInstance.id = act;
                ActList.Add(actInstance);
                /*
                switch (act)
                {

                    case 0:
                        ActList.Add(new Divide());//仮;
                        break;

                    case 1:
                        ActList.Add(new Attack());//仮
                        break;

                    case 2:
                        ActList.Add(new Move());//仮
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
                    break;
                    case 7:
                        ActList.Add(new GetNutFromLand());
                        break;
                }
                */
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
        /// <summary>
        /// Actを作るたびに上のswitch文に書き込んでいくのはなんというかスマートじゃないので、
        ///これを使って登録していく方式にします
        ///ActRegisterのRegister()の中で登録してください
        /// </summary>
        /// <param name="id"> Act固有のidです</param>
        /// <param name="actType">Actのクラスを表すTypeです</param>
        /// <returns>成功したらtrue,失敗したらfalseを返します</returns>
        public static bool RegisterAct(int id,Type actType)
        {
            if (!actType.IsSubclassOf(typeof(Act)))
            {
                Console.WriteLine(actType.ToString()+"はActクラスを継承していないため登録できません");
                return false;
            }
            if (ActClassMap.ContainsKey(id))
            {
                Console.WriteLine(actType.ToString() + "と"+ActClassMap[id].ToString()+"のIDが両方とも"+id+"のため登録できません");
                return false;
            }
            //Console.WriteLine(actType.ToString()+ "," + id);
            ActClassMap.Add(id, actType);
            actIdList.Add(id);
            return true;
        }

        /// <summary>
        /// 存在するActIdから、ランダムなものを返します
        /// </summary>
        /// <returns></returns>
        public static int GetRandomActId()
        {
            return actIdList[Program.Rand.Next(actIdList.Count)];
        }

        List<Act> ActList= new List<Act>();
        public int timer;//毎フレームUpdateしていると重いし、新しいActの方でも不都合があるので60Fに一度にします
        public int timerMax = 60;
        
        //ゲーム内で登場しうるActをidと一緒に保存
        public static IDictionary<int,Type> ActClassMap = new Dictionary<int,Type>();
        public static List<int> actIdList = new List<int>();
    }
}
