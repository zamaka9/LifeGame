using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class CreatureMgr
    {
        public void Initialize(Land ptrLand, Drawer ptrDrawer)
        {
            Land = ptrLand;

            //ActクラスにCreateCreature用インターフェースポインタを登録
            Act.CreatureMgr = this;
            Act.Land = Land;

            //仮　Creatureのグラフィックを読み込み
            Creature.LoadGraphic(ptrDrawer);


            //Creatureの登録
            for (int i = 0; i < Program.MaxCreature; i++)
            {
                CreatureList.Add(new Creature(this));
            }
        }

        public void Update()
        {
            foreach (Creature newParent in ParentList)
            {
                CreatureList.Add(new Creature(this, newParent));
            }
            ParentList.Clear();

            // 当たり判定用にどこにいるかを登録
            foreach (Creature creature in CreatureList.Where(x => x.Existence == true))
            {
                Land.AddCList(creature.X, creature.Y, creature);
            }

            // Creatureの更新
            bool isTimeToUpdate = (++timer % timerMax) == 0;
            if (isTimeToUpdate)
            {

                foreach (Creature creature in CreatureList.Where(x => x.Existence == true))
                {
                    creature.Update();

                }

            }
            foreach (Creature creature in CreatureList.Where(x => x.Existence == true))
            {
                creature.Move();
            }
            hasTimerUpdated = false;

            // 消去フラグの立った生物は削除
            CreatureList.RemoveAll(x => x.Existence == false);

            TimeCount++;
        }

        public void Draw()
        {
            foreach (Creature creature in CreatureList.Where(x => x.Existence == true))
            {
                creature.Draw(timer,1+TimerMax/4);
            }
            DX.SetDrawBright(255, 255, 255);

            //生物数を表示
            DX.DrawString(4, Program.Window_Y - 20, CreatureList.Count.ToString(), DX.GetColor(255, 255, 255));

            DX.DrawString(0, 0, (TimeCount / 3600 + 1).ToString() + "年目", DX.GetColor(255, 255, 255));
        }

        public void CreateCreature(Creature Parent)
        {
            ParentList.Add(Parent);
        }

        //LandやDrawerがCreatureから参照できないのが不便だったからpublicにしちゃった
        public Land Land;
        public Drawer Drawer;

        public List<Creature> CreatureList = new List<Creature>();

        List<Creature> ParentList = new List<Creature>();//CreateCreature用 親リスト

        int TimeCount = 0;

        int timer;//CreatureMgrの方で管理します
        int timerMax = 40;//書き換え非推奨　TimerMaxを使ってください
        public bool hasTimerUpdated;//直前のフレームにTimerMaxが書き換えられたときにtrue

        public int TimerMax
        {//何フレームに一度アップデートするかを指定
            get
            {
                return timerMax;
            }
            set
            {
                timerMax = value;
                hasTimerUpdated = true;
            }
        }

    }

}
