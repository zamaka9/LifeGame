using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    class Nutrition
    {
        public Nutrition(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        public Nutrition() : this(0, 0, 0) { }

        public Nutrition Rand(int randMax)
        {
            Red = Program.Rand.Next(randMax);
            Green = Program.Rand.Next(randMax);
            Blue = Program.Rand.Next(randMax);
            return this;
        }

        public Nutrition Rand(int randMin, int randMax)
        {
            Red = Program.Rand.Next(randMin, randMax);
            Green = Program.Rand.Next(randMin, randMax);
            Blue = Program.Rand.Next(randMin, randMax);
            return this;
        }

        internal Nutrition PercentNonNegative(double v)
        {
            throw new NotImplementedException();
        }

        public static Nutrition operator +(Nutrition n1, Nutrition n2)
        {
            return new Nutrition(n1.Red + n2.Red, n1.Green + n2.Green,n1.Blue + n2.Blue);
        }
        public static Nutrition operator +(Nutrition n, int i)
        {
            return new Nutrition(n.Red + i, n.Green + i,n.Blue + i);
        }

        public static Nutrition operator -(Nutrition n1, Nutrition n2)
        {
            return new Nutrition(n1.Red - n2.Red, n1.Green - n2.Green,n1.Blue - n2.Blue);
        }
        public static Nutrition operator -(Nutrition n, int i)
        {
            return new Nutrition(n.Red - i, n.Green - i, n.Blue - i);
        }

        public static Nutrition operator *(Nutrition n, int i)
        {
            return new Nutrition(n.Red * i, n.Green * i, n.Blue * i);
        }

        public static Nutrition operator *(Nutrition n, float i)
        {
            return new Nutrition((int)(n.Red * i), (int)(n.Green * i), (int)(n.Blue * i));
        }

        public static Nutrition operator /(Nutrition n, int i)
        {
            return new Nutrition(n.Red / i, n.Green / i, n.Blue / i);
        }

        public static Nutrition operator /(Nutrition n, float i)
        {
            return new Nutrition((int)(n.Red / i), (int)(n.Green / i), (int)(n.Blue / i));
        }

        public static bool operator ==(Nutrition n1, Nutrition n2)
        {
            if(Object.ReferenceEquals(n1, n2))
            {
                return true;
            }
            if((Object)n1 == null||(Object)n2 == null)
            {
                return false;
            }
            return n1.Red == n2.Red  && n1.Green == n2.Green&& n1.Blue == n2.Blue;
        }
        public static bool operator !=(Nutrition n1, Nutrition n2)
        {
            return !(n1 == n2);
        }

        public static bool operator <(Nutrition n1, Nutrition n2)
        {
            return n1.Red < n2.Red && n1.Green < n2.Green&& n1.Blue < n2.Blue ;
        }
        public static bool operator <(Nutrition n, int i)
        {
            return n.Red < i && n.Green < i&& n.Blue < i ;
        }

        public static bool operator >(Nutrition n1, Nutrition n2) {
            return n2 < n1;
        }
        public static bool operator >(Nutrition n, int i)
        {
            return n.Red > i && n.Green > i&& n.Blue > i ;
        }

        //valueの値を栄養の割合にして返す
        public Nutrition Percent(int value)
        {
            if (Sum > 0)
            {
                return new Nutrition((int)(Red * ((double)value / (double)Sum)),(int)( Green * ((double)value / (double)Sum)),(int)( Blue * ((double)value / (double)Sum)));
            }
            else
            {
                return new Nutrition();
            }
        }

        //valueの値を栄養の割合にして返すが、負の値になりそうになったら調整する
        public Nutrition PercentNonNegative(int value)
        {
            if(value>Sum){
                value=Sum;

            }
            if (Sum > 0)
            {
                return new Nutrition((int)(Red * ((double)value / (double)Sum)),(int)( Green * ((double)value / (double)Sum)),(int)( Blue * ((double)value / (double)Sum)));
            }
            else
            {
                return new Nutrition();
            }
        }

        //コピーする。主にMaxNutritionからNutritionに移すために存在。ただしMaxNutritionのシステムは再考の余地あり
        public Nutrition Copy()
        {
            return new Nutrition(Red, Green, Blue);
        }

        ///<summary>
        ///名前はDivideだけど分裂とは無関係なので注意
        ///ちょっと説明が難しい関数なんだけど、基本的には今の栄養値をbyで割ったNutritionを返す
        ///でも、余りを考えてくれるというか、例えば元がR=10,G=11,B=7だったら
        ///by=9,of=1だと(2,2,1)を返し、by=9,of=2だと(1,2,1)、by=9,of=8だと(1,1,0)を返す
        ///というか、周囲に栄養ばらまいたときに、端数が丸められて栄養が目減りするのを防ぐための関数
        ///</summary>
        public Nutrition Divide(int by,int of)
        {
            return new Nutrition(Red % by < of ? 0 : 1, Green % by < of ? 0 : 1, Blue % by < of ? 0 : 1) + this / by;
        }

        public override string ToString()
        {
            return Red + "," + Green + "," + Blue;
        }


        /// <summary>
        /// byで与えられた栄養値を上限として、現在栄養値を丸めた値を返す
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public Nutrition clamp(Nutrition by)
        {
            Nutrition nut = this.Copy();
            if (nut.Red > by.Red)
            {
                nut.Red = by.Red;
            }
            if (nut.Green > by.Green)
            {
                nut.Green = by.Green;
            }
            if (nut.Blue > by.Blue)
            {
                nut.Blue = by.Blue;
            }
            return nut;
        }

        /// <summary>
        /// byで与えられた栄養値を下限として、現在栄養値を丸めた値を返す
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public Nutrition clamp_floor(Nutrition by)
        {
            Nutrition nut = this.Copy();
            if (nut.Red < by.Red)
            {
                nut.Red = by.Red;
            }
            if (nut.Green <by.Green)
            {
                nut.Green = by.Green;
            }
            if (nut.Blue < by.Blue)
            {
                nut.Blue = by.Blue;
            }
            return nut;
        }

        //栄養量の総和を返す
        public int Sum => (Red + Green + Blue);

        public int Red { get; private set; } = 0;
        public int Green { get; private set; } = 0;
        public int Blue { get; private set; } = 0;

        public static int MaxValue = 0xffffff;//=16,777,215 栄養の最大値

        public static Nutrition ZERO = new Nutrition();
    }
}
