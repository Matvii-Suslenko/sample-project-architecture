using System;
using Core.Helpers;

namespace Core
{
    /// <summary>
    /// Representation of 2D vectors and points using integers.
    /// </summary>
    [Serializable]
    public struct Int2 : IEquatable<Int2>
    {
        public int x;
        public int y;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Vector2Int index addressed: {index}!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Vector2Int index addressed: {index}!");
                }
            }
        }

        public float magnitude => MathCore.Sqrt(x * x + y * y);

        public int sqrMagnitude => x * x + y * y;

        public static Int2 zero { get; } = new Int2(0, 0);

        public static Int2 one { get; } = new Int2(1, 1);

        public static Int2 up { get; } = new Int2(0, 1);

        public static Int2 down { get; } = new Int2(0, -1);

        public static Int2 left { get; } = new Int2(-1, 0);

        public static Int2 right { get; } = new Int2(1, 0);

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Int2(float x, float y)
        {
            this.x = (int)Math.Round(x, MidpointRounding.AwayFromZero);
            this.y = (int)Math.Round(y, MidpointRounding.AwayFromZero);
        }

        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static float Distance(Int2 a, Int2 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            return (float)Math.Sqrt(num * num + num2 * num2);
        }

        public static float SqrDistance(Int2 a, Int2 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            return num * num + num2 * num2;
        }

        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        public static Int2 Min(Int2 a, Int2 b)
        {
            return new Int2(MathCore.Min(a.x, b.x), MathCore.Min(a.y, b.y));
        }

        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        public static Int2 Max(Int2 a, Int2 b)
        {
            return new Int2(MathCore.Max(a.x, b.x), MathCore.Max(a.y, b.y));
        }

        public static Int2 Clamp(Int2 a, Int2 min, Int2 max)
        {
            return new Int2(MathCore.Clamp(a.x, min.x, max.x), MathCore.Clamp(a.y, min.y, max.y));
        }

        /// <summary>
        /// Multiplies two vectors component-wise.
        /// </summary>
        public static Int2 Scale(Int2 a, Int2 b)
        {
            return new Int2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// Multiplies every component of this vector by the same component of scale.
        /// </summary>
        public void Scale(Int2 scale)
        {
            x *= scale.x;
            y *= scale.y;
        }

        /// <summary>
        /// Clamps the Int2 to the bounds given by min and max.
        /// </summary>
        public void Clamp(Int2 min, Int2 max)
        {
            x = Math.Max(min.x, x);
            x = Math.Min(max.x, x);
            y = Math.Max(min.y, y);
            y = Math.Min(max.y, y);
        }

        public static implicit operator Float2(Int2 v)
        {
            return new Float2(v.x, v.y);
        }

        public static explicit operator Int3(Int2 v)
        {
            return new Int3(v.x, v.y, 0);
        }

        /// <summary>
        /// Converts a Vector2 to a Vector2Int by doing a Floor to each value.
        /// </summary>
        public static Int2 FloorToInt(Float2 v)
        {
            return new Int2(MathCore.FloorToInt(v.x), MathCore.FloorToInt(v.y));
        }

        /// <summary>
        /// Converts a Vector2 to a Vector2Int by doing a Ceiling to each value.
        /// </summary>
        public static Int2 CeilToInt(Float2 v)
        {
            return new Int2(MathCore.CeilToInt(v.x), MathCore.CeilToInt(v.y));
        }

        /// <summary>
        /// Converts a Vector2 to a Vector2Int by doing a Round to each value.
        /// </summary>
        public static Int2 RoundToInt(Float2 v)
        {
            return new Int2(MathCore.RoundToInt(v.x), MathCore.RoundToInt(v.y));
        }

        public static Int2 operator -(Int2 v)
        {
            return new Int2(-v.x, -v.y);
        }

        public static Int2 operator +(Int2 a, Int2 b)
        {
            return new Int2(a.x + b.x, a.y + b.y);
        }

        public static Int2 operator -(Int2 a, Int2 b)
        {
            return new Int2(a.x - b.x, a.y - b.y);
        }

        public static Int2 operator *(Int2 a, Int2 b)
        {
            return new Int2(a.x * b.x, a.y * b.y);
        }

        public static Int2 operator *(int a, Int2 b)
        {
            return new Int2(a * b.x, a * b.y);
        }

        public static Int2 operator *(Int2 a, int b)
        {
            return new Int2(a.x * b, a.y * b);
        }

        public static Int2 operator /(Int2 a, int b)
        {
            return new Int2(a.x / b, a.y / b);
        }

        public static bool operator ==(Int2 a, Int2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Int2 a, Int2 b)
        {
            return !(a == b);
        }

        public override bool Equals(object other)
        {
            if (!(other is Int2))
            {
                return false;
            }

            return Equals((Int2)other);
        }

        public bool Equals(Int2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}