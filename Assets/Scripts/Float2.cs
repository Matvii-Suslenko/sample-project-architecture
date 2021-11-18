using System;
using Core.Helpers;

namespace Core
{
    /// <summary>
    ///   <para>Representation of 2D vectors and points.</para>
    /// </summary>

    [Serializable]
    public struct Float2
    {
        public const float kEpsilon = 1E-05f;
        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public float x;
        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public float y;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Float2 normalized
        {
            get
            {
                Float2 vector2 = new Float2(this.x, this.y);
                vector2.Normalize();
                return vector2;
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return MathCore.Sqrt((float)((double)this.x * (double)this.x + (double)this.y * (double)this.y));
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return (float)((double)this.x * (double)this.x + (double)this.y * (double)this.y);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, 0).</para>
        /// </summary>
        public static Float2 zero
        {
            get
            {
                return new Float2(0.0f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(1, 1).</para>
        /// </summary>
        public static Float2 one
        {
            get
            {
                return new Float2(1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, 1).</para>
        /// </summary>
        public static Float2 up
        {
            get
            {
                return new Float2(0.0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, -1).</para>
        /// </summary>
        public static Float2 down
        {
            get
            {
                return new Float2(0.0f, -1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(-1, 0).</para>
        /// </summary>
        public static Float2 left
        {
            get
            {
                return new Float2(-1f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(1, 0).</para>
        /// </summary>
        public static Float2 right
        {
            get
            {
                return new Float2(1f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Constructs a new vector with given x, y components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Float2(Float3 v)
        {
            return new Float2(v.x, v.y);
        }

        public static implicit operator Float3(Float2 v)
        {
            return new Float3(v.x, v.y, 0.0f);
        }

        public static Float2 operator +(Float2 a, Float2 b)
        {
            return new Float2(a.x + b.x, a.y + b.y);
        }

        public static Float2 operator -(Float2 a, Float2 b)
        {
            return new Float2(a.x - b.x, a.y - b.y);
        }

        public static Float2 operator -(Float2 a)
        {
            return new Float2(-a.x, -a.y);
        }

        public static Float2 operator *(Float2 a, float d)
        {
            return new Float2(a.x * d, a.y * d);
        }

        public static Float2 operator *(float d, Float2 a)
        {
            return new Float2(a.x * d, a.y * d);
        }

        public static Float2 operator /(Float2 a, float d)
        {
            return new Float2(a.x / d, a.y / d);
        }

        public static bool operator ==(Float2 lhs, Float2 rhs)
        {
            return (double)Float2.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Float2 lhs, Float2 rhs)
        {
            return (double)Float2.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        /// <summary>
        ///   <para>Set x and y components of an existing Vector2.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        public void Set(float new_x, float new_y)
        {
            this.x = new_x;
            this.y = new_y;
        }

        /// <summary>
        ///   <para>Linearly interpolates between vectors a and b by t.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Float2 Lerp(Float2 a, Float2 b, float t)
        {
            t = MathCore.Clamp01(t);
            return new Float2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between vectors a and b by t.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Float2 LerpUnclamped(Float2 a, Float2 b, float t)
        {
            return new Float2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        ///   <para>Moves a point current towards target.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Float2 MoveTowards(Float2 current, Float2 target, float maxDistanceDelta)
        {
            Float2 vector2 = target - current;
            float magnitude = vector2.magnitude;
            if ((double)magnitude <= (double)maxDistanceDelta || (double)magnitude == 0.0)
                return target;
            return current + vector2 / magnitude * maxDistanceDelta;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Float2 Scale(Float2 a, Float2 b)
        {
            return new Float2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Float2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float magnitude = this.magnitude;
            if ((double)magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Float2.zero;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return $"({this.x}, {this.y})";
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return $"({this.x.ToString(format)}, {this.y.ToString(format)})";
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Float2))
                return false;
            Float2 vector2 = (Float2)other;
            if (this.x.Equals(vector2.x))
                return this.y.Equals(vector2.y);
            return false;
        }

        /// <summary>
        ///   <para>Reflects a vector off the vector defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Float2 Reflect(Float2 inDirection, Float2 inNormal)
        {
            return -2f * Float2.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Float2 lhs, Float2 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static float Angle(Float2 from, Float2 to)
        {
            return MathCore.Acos(MathCore.Clamp(Float2.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Float2 a, Float2 b)
        {
            return (a - b).magnitude;
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Float2 ClampMagnitude(Float2 vector, float maxLength)
        {
            if ((double)vector.sqrMagnitude > (double)maxLength * (double)maxLength)
                return vector.normalized * maxLength;
            return vector;
        }

        /// <summary>
        /// Clamps the Float2 to the bounds given by min and max.
        /// </summary>
        public void Clamp(Float2 min, Float2 max)
        {
            x = Math.Max(min.x, x);
            x = Math.Min(max.x, x);
            y = Math.Max(min.y, y);
            y = Math.Min(max.y, y);
        }

        public static float SqrMagnitude(Float2 a)
        {
            return (float)((double)a.x * (double)a.x + (double)a.y * (double)a.y);
        }

        public float SqrMagnitude()
        {
            return (float)((double)this.x * (double)this.x + (double)this.y * (double)this.y);
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Float2 Min(Float2 lhs, Float2 rhs)
        {
            return new Float2(MathCore.Min(lhs.x, rhs.x), MathCore.Min(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Float2 Max(Float2 lhs, Float2 rhs)
        {
            return new Float2(MathCore.Max(lhs.x, rhs.x), MathCore.Max(lhs.y, rhs.y));
        }

        public static Float2 SmoothDamp(Float2 current, Float2 target, ref Float2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = MathCore.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Float2 vector = current - target;
            Float2 vector2_1 = target;
            float maxLength = maxSpeed * smoothTime;
            Float2 vector2_2 = Float2.ClampMagnitude(vector, maxLength);
            target = current - vector2_2;
            Float2 vector2_3 = (currentVelocity + num1 * vector2_2) * deltaTime;
            currentVelocity = (currentVelocity - num1 * vector2_3) * num3;
            Float2 vector2_4 = target + (vector2_2 + vector2_3) * num3;
            if ((double)Float2.Dot(vector2_1 - current, vector2_4 - vector2_1) > 0.0)
            {
                vector2_4 = vector2_1;
                currentVelocity = (vector2_4 - vector2_1) / deltaTime;
            }
            return vector2_4;
        }
    }
}