using System;
using Core;
using Core.Helpers;

namespace Core
{
    //
    // Summary:
    //     Representation of 3D vectors and points using integers.
    public struct Int3 : IEquatable<Int3>
    {
        public int x { get; set; }

        public int y { get; set; }

        public int z { get; set; }

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
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Int3 index addressed: {index}!");
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
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Int3 index addressed: {index}!");
                }
            }
        }

        public float magnitude => MathCore.Sqrt(x * x + y * y + z * z);

        public int sqrMagnitude => x * x + y * y + z * z;

        public static Int3 zero { get; } = new Int3(0, 0, 0);

        public static Int3 one { get; } = new Int3(1, 1, 1);

        public static Int3 up { get; } = new Int3(0, 1, 0);

        public static Int3 down { get; } = new Int3(0, -1, 0);

        public static Int3 left { get; } = new Int3(-1, 0, 0);

        public static Int3 right { get; } = new Int3(1, 0, 0);

        public Int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static float Distance(Int3 a, Int3 b)
        {
            return (a - b).magnitude;
        }

        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        public static Int3 Min(Int3 a, Int3 b)
        {
            return new Int3(MathCore.Min(a.x, b.x), MathCore.Min(a.y, b.y), MathCore.Min(a.z, b.z));
        }

        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        public static Int3 Max(Int3 a, Int3 b)
        {
            return new Int3(MathCore.Max(a.x, b.x), MathCore.Max(a.y, b.y), MathCore.Max(a.z, b.z));
        }

        /// <summary>
        /// Multiplies two vectors component-wise.
        /// </summary>
        public static Int3 Scale(Int3 a, Int3 b)
        {
            return new Int3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Multiplies every component of this vector by the same component of scale.
        /// </summary>
        public void Scale(Int3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        /// <summary>
        /// Clamps the Int3 to the bounds given by min and max.
        /// </summary>
        public void Clamp(Int3 min, Int3 max)
        {
            x = MathCore.Max(min.x, x);
            x = MathCore.Min(max.x, x);
            y = MathCore.Max(min.y, y);
            y = MathCore.Min(max.y, y);
            z = MathCore.Max(min.z, z);
            z = MathCore.Min(max.z, z);
        }

        public static implicit operator Float3(Int3 v)
        {
            return new Float3(v.x, v.y, v.z);
        }

        public static explicit operator Int2(Int3 v)
        {
            return new Int2(v.x, v.y);
        }

        /// <summary>
        /// Converts a Vector3 to a Int3 by doing a Floor to each value.
        /// </summary>
        public static Int3 FloorToInt(Float3 v)
        {
            return new Int3(MathCore.FloorToInt(v.x), MathCore.FloorToInt(v.y), MathCore.FloorToInt(v.z));
        }

        /// <summary>
        /// Converts a Vector3 to a Int3 by doing a Ceiling to each value.
        /// </summary>
        public static Int3 CeilToInt(Float3 v)
        {
            return new Int3(MathCore.CeilToInt(v.x), MathCore.CeilToInt(v.y), MathCore.CeilToInt(v.z));
        }

        /// <summary>
        /// Converts a Vector3 to a Int3 by doing a Round to each value.
        /// </summary>
        public static Int3 RoundToInt(Float3 v)
        {
            return new Int3(MathCore.RoundToInt(v.x), MathCore.RoundToInt(v.y), MathCore.RoundToInt(v.z));
        }

        public static Int3 operator +(Int3 a, Int3 b)
        {
            return new Int3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Int3 operator -(Int3 a, Int3 b)
        {
            return new Int3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Int3 operator *(Int3 a, Int3 b)
        {
            return new Int3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Int3 operator -(Int3 a)
        {
            return new Int3(-a.x, -a.y, -a.z);
        }

        public static Int3 operator *(Int3 a, int b)
        {
            return new Int3(a.x * b, a.y * b, a.z * b);
        }

        public static Int3 operator *(int a, Int3 b)
        {
            return new Int3(a * b.x, a * b.y, a * b.z);
        }

        public static Int3 operator /(Int3 a, int b)
        {
            return new Int3(a.x / b, a.y / b, a.z / b);
        }

        public static bool operator ==(Int3 lhs, Int3 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }

        public static bool operator !=(Int3 lhs, Int3 rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object other)
        {
            if (!(other is Int3))
            {
                return false;
            }

            return Equals((Int3)other);
        }

        public bool Equals(Int3 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            int hashCode = y.GetHashCode();
            int hashCode2 = z.GetHashCode();
            return x.GetHashCode() ^ (hashCode << 4) ^ (hashCode >> 28) ^ (hashCode2 >> 4) ^ (hashCode2 << 28);
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public string ToString(string format)
        {
            return $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)})";
        }
    }
}