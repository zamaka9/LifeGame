using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    class Vector2D:ICloneable
    {
        public Vector2D() : this(0.0f, 0.0f)
        {
        }
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public object Clone()
        {
            return new Vector2D(X, Y);
        }

        public static Vector2D operator +(Vector2D vec1, Vector2D vec2)
        {
            return new Vector2D(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        public static Vector2D operator -(Vector2D vec1, Vector2D vec2)
        {
            return new Vector2D(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static Vector2D operator *(Vector2D vec1, Vector2D vec2)
        {
            return new Vector2D(vec1.X * vec2.X, vec1.Y * vec2.Y);
        }
        public static Vector2D operator *(Vector2D vec, float val)
        {
            return new Vector2D(vec.X * val, vec.Y * val);
        }

        public static Vector2D operator /(Vector2D vec1, Vector2D vec2)
        {
            return new Vector2D(vec1.X / vec2.X, vec1.Y / vec2.Y);
        }
        public static Vector2D operator /(Vector2D vec, float val)
        {
            return new Vector2D(vec.X / val, vec.Y / val);
        }

        public static bool operator ==(Vector2D vec1, Vector2D vec2)
        {
            if (Object.ReferenceEquals(vec1, vec2))
            {
                return true;
            }
            if ((Object)vec1 == null || (Object)vec2 == null)
            {
                return false;
            }
            return (vec1.X == vec2.X) && (vec1.Y == vec2.Y);
        }
        public static bool operator !=(Vector2D vec1, Vector2D vec2)
        {
            return !(vec1 == vec2);
        }

        /// <summary>
        /// 内積を返す
        /// </summary>
        public static float Dot(Vector2D vec1, Vector2D vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }

        /// <summary>
        /// ベクトルの大きさを返す
        /// </summary>
        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// ベクトルの大きさの二乗を返す
        /// </summary>
        /// <returns></returns>
        public float SquareLength => X * X + Y * Y;

        /// <summary>
        /// 正規化したベクトルを返す
        /// </summary>
        public Vector2D Normal => new Vector2D(X / Length, Y / Length);

        /// <summary>
        /// 指定角度分回転したベクトルを返す
        /// </summary>
        /// <param name="angle">回転角度(ラジアン)</param>
        public Vector2D Rotate(float angle)
        {
            return new Vector2D((float)(X * Math.Cos(angle) - Y * Math.Sin(angle)), (float)(X * Math.Sin(angle) + Y * Math.Cos(angle)));
        }

        public float X { get; set; }
        public float Y { get; set; }
        public int iX => (int)X;
        public int iY => (int)Y;
    }
}
