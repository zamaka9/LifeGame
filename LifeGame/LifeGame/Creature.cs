﻿using System;
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
            
            Position = parent.Position + new Vector2D(Program.Rand.Next(-20, 20), Program.Rand.Next(-20, 20));

        }
        public Creature(CreatureMgr mgr):this(mgr, new Gene())
        {
            Position=new Vector2D(Program.Rand.Next(Program.World_X-1), Program.Rand.Next(Program.World_Y - 1));

        }

        public Creature(CreatureMgr mgr, Gene gene):this(mgr,gene,gene.Nutrition)
        {
        }

        public Creature(CreatureMgr mgr,Gene gene,Nutrition nutrition)
        {
            this.mgr = mgr;

            Gene = gene;
            Size = gene.Size;
            MaxHP = gene.HP;
            HP = Program.Rand.Next(MaxHP);
            MaxNutrition = gene.Nutrition;
            Nutrition = nutrition;
            
            ActMgr.Initialize(this, TargetList, gene.ActList);

            Time = 0;

            Existence = true;
            Alive = true;

            Velocity=new Vector2D();
            LastVelocityMultiplier = 60f/(float)mgr.TimerMax;
            Velocitycache = new Vector2D();
        }

        public void Update()
        {
            if (Alive == true)
            {//生存なう
                
                MaxHP-=agePerTick;
                HP-=agePerTick;
                if (!isStable)
                {
                    int freq = 10;
                    int SpeedLevel = 5;
                    float MaxSpeed = 0.5F;
                    if (Program.Rand.Next(freq) == 0)
                    {

                        float Direction = (float)((Program.Rand.Next(359) / 180.0f) * Math.PI);
                        float Speed = MaxSpeed * SpeedLevel / 10;
                        //速度はCreatureの方で持つようにしました
                        Velocity = new Vector2D((float)Math.Cos(Direction), (float)Math.Sin(Direction)) * Speed;
                    }
                }
                ActMgr.Update();
                Nutrition -= new Nutrition(Size*5, Size, Size);
                Nutrition=Nutrition.clamp_floor(Nutrition.ZERO);

                if (!(this.Nutrition > Nutrition.ZERO))
                {
                    this.HP = 0;
                }
                if (!(this.Nutrition < this.MaxNutrition))
                {
                    Nutrition lastNut = this.Nutrition.Copy();
                    this.Nutrition = this.Nutrition.clamp(this.MaxNutrition);
                    mgr.Land.SetLandNutrition(Position, mgr.Land.GetLandNutrition(Position)+lastNut - Nutrition);
                }

                    //突然の死
                    if (Program.Rand.Next(1000)==0)
                {
                    this.HP = 0;
                }
                
                //Nutrition.clamp(MaxNutrition);
                
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
            if (Size * drawer.zoom > 20)
            {
                if (Alive == true)
                {
                    drawer.AddDrawList(Position, 0, Size / 10, GH[(animTime / speed) % 8]);
                }
                else
                {
                    drawer.AddDrawList(Position, 0, Size / 10, GH[(deadAnimTime / speed) % 8]);
                }
            }
        }

        //velocitycacheにもとづき、座標を更新する。Update頻度に関係なく、毎フレーム呼ばれる
        public void Move(){
            if (Alive == true){
            if(mgr.hasTimerUpdated){
                LastVelocityMultiplier=60f/(float)mgr.TimerMax;
            }
                Position += Velocitycache;
            
                //何だか生物が変なところ行くので仮の処置
                if (Position.X < 0)
                {
                    Position.X += Program.World_X;
                }else if(Position.X > Program.World_X)
                {
                    Position.X -= Program.World_X;
                }
                if (Position.Y < 0)
                {
                    Position.Y += Program.World_Y;
                }
                else if (Position.Y > Program.World_Y)
                {
                    Position.Y -= Program.World_Y;
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
            }*/
           
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

        Gene Gene { get; set; }//遺伝子クラス

        public ActMgr ActMgr = new ActMgr();//行動パターン管理クラス

        public List<Creature> TargetList { get; set; } = new List<Creature>();//同エリア内のCreatureを持つリストを指すポインタ

        public CreatureMgr mgr;//CreatureMgrを格納する。コンストラクタで登録。staticにするか迷ったけど特に必要性がないので普通で

        private Vector2D velocity;
        public Vector2D Velocity
        {
            
            set
            {
                Velocitycache = value * LastVelocityMultiplier;
                velocity = value;
            }
            get
            {
                return this.velocity;
            }
        }
       //移動速度

        Vector2D Velocitycache ;//1ティックごとの移動距離を保存
        private float lastVelocityMultiplier;
        public float LastVelocityMultiplier
        {
            
            set
            {
                Velocitycache = Velocity*value;
                lastVelocityMultiplier = value;
            }
            get
            {
                return this.lastVelocityMultiplier;
            }
        }//VelocityXにこの値をかけた数字が実際にX,Yに加算される

        int agePerTick=1;
        int deadAnimTime;

        //falseのときランダム移動し、trueのとき任意移動する
        public bool isStable;
    }
}

