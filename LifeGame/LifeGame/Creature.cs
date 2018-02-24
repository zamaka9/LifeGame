using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace LifeGame
{
    class Creature
    {
        public static void LoadGraphic(Drawer drawer)
        {
            DX.LoadDivGraph("Data/原初生物Anim.png", 8, 4, 2, 32, 32, GH);
            Creature.drawer = drawer;
        }

        public Creature(CreatureMgr mgr,Creature parent) : this(mgr, new Gene(parent.Gene), parent.Nutrition.Copy())
        {

            Size = Gene.Size;
            MaxHP = Gene.HP;
            HP =MaxHP;
            MaxNutrition = Gene.Nutrition;
            
            Nutrition = parent.Nutrition.Copy();

            X = parent.X + Program.Rand.Next(-20, 20);
            Y = parent.Y + Program.Rand.Next(-20, 20);

        }
        public Creature(CreatureMgr mgr):this(mgr, new Gene())
        {
            X = (float)Program.Rand.Next(Program.World_X - 1);
            Y = (float)Program.Rand.Next(Program.World_Y - 1);

        }

        public Creature(CreatureMgr mgr, Gene gene):this(mgr,gene,gene.Nutrition)
        {
        }

        public Creature(CreatureMgr mgr,Gene gene,Nutrition nutrition)
        {
            this.mgr = mgr;
           
            Size = gene.Size;
            MaxHP = gene.HP;
            HP = Program.Rand.Next(MaxHP);
            MaxNutrition = gene.Nutrition;
            Nutrition = nutrition;
            
            ActMgr.Initialize(this, TargetList, gene.ActList);

            Time = 0;

            Existence = true;
            Alive = true;

            VelocityX = 0;
            VelocityY = 0;
            LastVelocityMultiplier = 60f/(float)mgr.TimerMax;
            VelocityXcache = 0;
            VelocityYcache = 0;
        }

        public void Update()
        {
            if (Alive == true)
            {//生存なう
                MaxHP-=agePerTick;
                HP-=agePerTick;
                ActMgr.Update();
                X += VelocityX;
                Y += VelocityY;
                //何だか生物が変なところ行くので仮の処置
                if (X < 0)
                {
                    X+= Program.World_X;
                }else if(X> Program.World_X)
                {
                    X -= Program.World_X;
                }
                if (Y < 0)
                {
                    Y += Program.World_Y;
                }
                else if (Y > Program.World_Y)
                {
                    Y -= Program.World_Y;
                }

                if (HP <= 0)
                {
                    Alive = false;
                    deadTime = Time;
                    //栄養ばらまく。後々は、まず死体になってそれが分解されていく感じにしたい
                    OnDied();
                }

            }
            else
            {
                
                if (Nutrition.Sum<=0)
                {
                    Existence = false;
                }
                
            }

            Time++;
        }
        public void Draw(int animTime,int speed)
        {
            if (Alive == true)
            {
                drawer.AddDrawList(X, Y, 0, Size / 10, GH[(animTime / speed) % 8]);
            }
            else
            {
                drawer.AddDrawList(X, Y, 0, Size / 10, GH[(deadAnimTime / speed) % 8]);
            }
        }

        //velocitycacheにもとづき、座標を更新する。Update頻度に関係なく、毎フレーム呼ばれる
        public void Move(){
            if (Alive == true){
            if(mgr.hasTimerUpdated){
                LastVelocityMultiplier=60f/(float)mgr.TimerMax;
            }
             X += VelocityXcache;
                Y += VelocityYcache;
            
                //何だか生物が変なところ行くので仮の処置
                if (X < 0)
                {
                    X+= Program.World_X;
                }else if(X> Program.World_X)
                {
                    X -= Program.World_X;
                }
                if (Y < 0)
                {
                    Y += Program.World_Y;
                }
                else if (Y > Program.World_Y)
                {
                    Y -= Program.World_Y;
                }
            }
        }
        //
        //メソッドとして独立
        public void OnDied()
        {
            /*栄養プレゼントしてた時代
            Creature nearest = this.GetNearestCreature();
            nearest.Nutrition = nearest.Nutrition + this.Nutrition;
            */
            //Landをどこから参照するか迷ったけど、Creaturemgrからにした
            //周囲9マスに平等に栄養をばらまく
            /*
            Land land = mgr.Land;
            Nutrition nut = Nutrition / 9;
            int i = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {

                    //Console.WriteLine(X + x*50 + "," + Y + y * 50);
                    Vector2D landPosition = Position + new Vector2D(x * 50, y * 50);
                    land.SetLandNutrition(landPosition, land.GetLandNutrition(landPosition) + Nutrition.Divide(9,i));
                    i++;
                }
            }
           
        }

        //引数で与えられた生物との距離の2乗を返す。GetNearestCreatureで使うための存在
        public double DistanceSquareFrom(Creature c)
        {
            return (this.X - c.X) * (this.X - c.X) + (this.Y - c.Y) * (this.Y - c.Y);
        }

        //最寄りの生物を取得。あまりきれいな形じゃないので後々Actあたりを使って綺麗にしたい
        public Creature GetNearestCreature()
        {
            Creature nearest=null;
            double distance=Double.MaxValue;
            foreach(Creature c in this.mgr.CreatureList)
            {
                if (c == this) continue;
                double d = (c.Position - this.Position).SquareLength;
                if ( d < distance) {
                    nearest = c;
                    distance = d;
                }
            }
            return nearest;
        }


        //仮 グラフィックハンドル
        static int[] GH = new int[8];
        static Drawer drawer;


        public bool Existence { get; private set; } = true;//存在フラグ（TRUEで存在　FALSEで消滅） 死体や種の状態でもOK
        public bool Alive { get; private set; } = true;//生存フラグ（TRUEで生　FALSEで死）

        public int MaxHP { get; private set; } = 0;//最大体力
        public Nutrition MaxNutrition { get; private set; } = new Nutrition();//栄養最大値
        public int Size { get; private set; } = 0;//大きさ（体格）

        public int HP { get; set; } = 0;//現在体力
        public Nutrition Nutrition { get; set; } = new Nutrition();//現在栄養
        // 座標
        public Vector2D Position { get; set; } = new Vector2D();
        public int Time { get; private set; } = 0;//時間
        int deadTime = 0;//死亡時刻

        Gene Gene { get; set; } = new Gene();//遺伝子クラス

        ActMgr ActMgr = new ActMgr();//行動パターン管理クラス

        public List<Creature> TargetList { get; set; } = new List<Creature>();//同エリア内のCreatureを持つリストを指すポインタ

        public CreatureMgr mgr;//CreatureMgrを格納する。コンストラクタで登録。staticにするか迷ったけど特に必要性がないので普通で

        private float velocityX;
        public float VelocityX
        {
            
            set
            {
                VelocityXcache = value * LastVelocityMultiplier;
                velocityX = value;
            }
            get
            {
                return this.velocityX;
            }
        }
        private float velocityY;
        public float VelocityY
        {
            
            set
            {
                VelocityYcache = value * LastVelocityMultiplier;
                velocityY = value;
            }
            get
            {
                return this.velocityY;
            }
        }//移動速度

        float VelocityXcache  = 0;
        float VelocityYcache = 0;//1ティックごとの移動距離を保存
        private float lastVelocityMultiplier;
        public float LastVelocityMultiplier
        {
            
            set
            {
                VelocityXcache = value * VelocityX;
                VelocityYcache = value * VelocityY;
                lastVelocityMultiplier = value;
            }
            get
            {
                return this.lastVelocityMultiplier;
            }
        }//VelocityXにこの値をかけた数字が実際にX,Yに加算される

        int agePerTick=200;
        int deadAnimTime;
    }
}

