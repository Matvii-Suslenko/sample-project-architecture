using System;
using System.Runtime.CompilerServices;
using Core.Helpers;

namespace Core
{
    /// <summary>
    ///   <para>Representation of 3D vectors and points.</para>
    /// </summary>
    public struct Float3
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
        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public float z;

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
                    case 2:
                        return this.z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Float3 normalized
        {
            get
            {
                return Float3.Normalize(this);
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return MathCore.Sqrt((float)((double)this.x * (double)this.x + (double)this.y * (double)this.y + (double)this.z * (double)this.z));
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return (float)((double)this.x * (double)this.x + (double)this.y * (double)this.y + (double)this.z * (double)this.z);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 0, 0).</para>
        /// </summary>
        public static Float3 zero
        {
            get
            {
                return new Float3(0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(1, 1, 1).</para>
        /// </summary>
        public static Float3 one
        {
            get
            {
                return new Float3(1f, 1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 0, 1).</para>
        /// </summary>
        public static Float3 forward
        {
            get
            {
                return new Float3(0.0f, 0.0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 0, -1).</para>
        /// </summary>
        public static Float3 back
        {
            get
            {
                return new Float3(0.0f, 0.0f, -1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, 1, 0).</para>
        /// </summary>
        public static Float3 up
        {
            get
            {
                return new Float3(0.0f, 1f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(0, -1, 0).</para>
        /// </summary>
        public static Float3 down
        {
            get
            {
                return new Float3(0.0f, -1f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(-1, 0, 0).</para>
        /// </summary>
        public static Float3 left
        {
            get
            {
                return new Float3(-1f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3(1, 0, 0).</para>
        /// </summary>
        public static Float3 right
        {
            get
            {
                return new Float3(1f, 0.0f, 0.0f);
            }
        }

        [Obsolete("Use Vector3.forward instead.")]
        public static Float3 fwd
        {
            get
            {
                return new Float3(0.0f, 0.0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y components and sets z to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Float3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public static Float3 operator +(Float3 a, Float3 b)
        {
            return new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Float3 operator -(Float3 a, Float3 b)
        {
            return new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Float3 operator -(Float3 a)
        {
            return new Float3(-a.x, -a.y, -a.z);
        }

        public static Float3 operator *(Float3 a, float d)
        {
            return new Float3(a.x * d, a.y * d, a.z * d);
        }

        public static Float3 operator *(float d, Float3 a)
        {
            return new Float3(a.x * d, a.y * d, a.z * d);
        }

        public static Float3 operator /(Float3 a, float d)
        {
            return new Float3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Float3 lhs, Float3 rhs)
        {
            return (double)Float3.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Float3 lhs, Float3 rhs)
        {
            return (double)Float3.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Float3 Lerp(Float3 a, Float3 b, float t)
        {
            t = MathCore.Clamp01(t);
            return new Float3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Float3 LerpUnclamped(Float3 a, Float3 b, float t)
        {
            return new Float3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Moves a point current in a straight line towards a target point.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Float3 MoveTowards(Float3 current, Float3 target, float maxDistanceDelta)
        {
            Float3 vector3 = target - current;
            float magnitude = vector3.magnitude;
            if ((double)magnitude <= (double)maxDistanceDelta || (double)magnitude == 0.0)
                return target;
            return current + vector3 / magnitude * maxDistanceDelta;
        }

        public static Float3 SmoothDamp(Float3 current, Float3 target, ref Float3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = MathCore.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Float3 vector = current - target;
            Float3 vector3_1 = target;
            float maxLength = maxSpeed * smoothTime;
            Float3 vector3_2 = Float3.ClampMagnitude(vector, maxLength);
            target = current - vector3_2;
            Float3 vector3_3 = (currentVelocity + num1 * vector3_2) * deltaTime;
            currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
            Float3 vector3_4 = target + (vector3_2 + vector3_3) * num3;
            if ((double)Float3.Dot(vector3_1 - current, vector3_4 - vector3_1) > 0.0)
            {
                vector3_4 = vector3_1;
                currentVelocity = (vector3_4 - vector3_1) / deltaTime;
            }
            return vector3_4;
        }

        /// <summary>
        ///   <para>Set x, y and z components of an existing Vector3.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        public void Set(float new_x, float new_y, float new_z)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Float3 Scale(Float3 a, Float3 b)
        {
            return new Float3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Float3 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Float3 Cross(Float3 lhs, Float3 rhs)
        {
            return new Float3((float)((double)lhs.y * (double)rhs.z - (double)lhs.z * (double)rhs.y), (float)((double)lhs.z * (double)rhs.x - (double)lhs.x * (double)rhs.z), (float)((double)lhs.x * (double)rhs.y - (double)lhs.y * (double)rhs.x));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Float3))
                return false;
            Float3 vector3 = (Float3)other;
            if (this.x.Equals(vector3.x) && this.y.Equals(vector3.y))
                return this.z.Equals(vector3.z);
            return false;
        }

        /// <summary>
        ///   <para>Reflects a vector off the plane defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Float3 Reflect(Float3 inDirection, Float3 inNormal)
        {
            return -2f * Float3.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="value"></param>
        public static Float3 Normalize(Float3 value)
        {
            float num = Float3.Magnitude(value);
            if ((double)num > 9.99999974737875E-06)
                return value / num;
            return Float3.zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float num = Float3.Magnitude(this);
            if ((double)num > 9.99999974737875E-06)
                this = this / num;
            else
                this = Float3.zero;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return $"({this.x}, {this.y}, {this.z})";
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return $"({this.x.ToString(format)}, {this.y.ToString(format)}, {this.z.ToString(format)})";
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Float3 lhs, Float3 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y + (double)lhs.z * (double)rhs.z);
        }

        /// <summary>
        ///   <para>Projects a vector onto another vector.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        public static Float3 Project(Float3 vector, Float3 onNormal)
        {
            float num = Float3.Dot(onNormal, onNormal);
            if ((double)num < (double)MathCore.Epsilon)
                return Float3.zero;
            return onNormal * Float3.Dot(vector, onNormal) / num;
        }

        /// <summary>
        ///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        public static Float3 ProjectOnPlane(Float3 vector, Float3 planeNormal)
        {
            return vector - Float3.Project(vector, planeNormal);
        }

        [Obsolete("Use Vector3.ProjectOnPlane instead.")]
        public static Float3 Exclude(Float3 excludeThis, Float3 fromThat)
        {
            return fromThat - Float3.Project(fromThat, excludeThis);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The angle extends round from this vector.</param>
        /// <param name="to">The angle extends round to this vector.</param>
        public static float Angle(Float3 from, Float3 to)
        {
            return MathCore.Acos(MathCore.Clamp(Float3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Float3 a, Float3 b)
        {
            Float3 vector3 = new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
            return MathCore.Sqrt((float)((double)vector3.x * (double)vector3.x + (double)vector3.y * (double)vector3.y + (double)vector3.z * (double)vector3.z));
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Float3 ClampMagnitude(Float3 vector, float maxLength)
        {
            if ((double)vector.sqrMagnitude > (double)maxLength * (double)maxLength)
                return vector.normalized * maxLength;
            return vector;
        }

        public static float Magnitude(Float3 a)
        {
            return MathCore.Sqrt((float)((double)a.x * (double)a.x + (double)a.y * (double)a.y + (double)a.z * (double)a.z));
        }

        public static float SqrMagnitude(Float3 a)
        {
            return (float)((double)a.x * (double)a.x + (double)a.y * (double)a.y + (double)a.z * (double)a.z);
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Float3 Min(Float3 lhs, Float3 rhs)
        {
            return new Float3(MathCore.Min(lhs.x, rhs.x), MathCore.Min(lhs.y, rhs.y), MathCore.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Float3 Max(Float3 lhs, Float3 rhs)
        {
            return new Float3(MathCore.Max(lhs.x, rhs.x), MathCore.Max(lhs.y, rhs.y), MathCore.Max(lhs.z, rhs.z));
        }

        [Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
        public static float AngleBetween(Float3 from, Float3 to)
        {
            return MathCore.Acos(MathCore.Clamp(Float3.Dot(from.normalized, to.normalized), -1f, 1f));
        }
    }
}