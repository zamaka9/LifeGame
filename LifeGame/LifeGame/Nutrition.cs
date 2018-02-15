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

        public static Nutrition operator +(Nutrition n1, Nutrition n2)
        {
            return new Nutrition(n1.Red + n2.Red, n1.Blue + n2.Blue, n1.Green + n2.Green);
        }
        public static Nutrition operator +(Nutrition n, int i)
        {
            return new Nutrition(n.Red + i, n.Blue + i, n.Green + i);
        }

        public static Nutrition operator -(Nutrition n1, Nutrition n2)
        {
            return new Nutrition(n1.Red - n2.Red, n1.Blue - n2.Blue, n1.Green - n2.Green);
        }
        public static Nutrition operator -(Nutrition n, int i)
        {
            return new Nutrition(n.Red - i, n.Blue - i, n.Green - i);
        }

        public static Nutrition operator *(Nutrition n, int i)
        {
            return new Nutrition(n.Red * i, n.Blue * i, n.Green * i);
        }

        public static Nutrition operator /(Nutrition n, int i)
        {
            return new Nutrition(n.Red / i, n.Blue / i, n.Green / i);
        }

        public static bool operator ==(Nutrition n1, Nutrition n2)
        {
            return n1.Red == n2.Red && n1.Blue == n2.Blue && n1.Green == n2.Green;
        }
        public static bool operator !=(Nutrition n1, Nutrition n2)
        {
            return n1.Red != n2.Red || n1.Blue != n2.Blue || n1.Green != n2.Green;
        }

        public static bool operator <(Nutrition n1, Nutrition n2)
        {
            return n1.Red < n2.Red && n1.Blue < n2.Blue && n1.Green < n2.Green;
        }
        public static bool operator <(Nutrition n, int i)
        {
            return n.Red < i && n.Blue < i && n.Green < i;
        }

        public static bool operator >(Nutrition n1, Nutrition n2) {
            return n2 < n1;
        }
        public static bool operator >(Nutrition n, int i)
        {
            return n.Red > i && n.Blue > i && n.Green > i;
        }

        //valueの値を栄養の割合にして返す
        public Nutrition Percent(int value)
        {
            if (Sum > 0)
            {
                return new Nutrition(Red * value / Sum, Green * value / Sum, Blue * value / Sum);
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

        //栄養量の総和を返す
        public int Sum => (Red + Green + Blue);

        public int Red { get; private set; } = 0;
        public int Green { get; private set; } = 0;
        public int Blue { get; private set; } = 0;
    }
}
